using Moq;
using Backend_Area42_3.Repositories;
using Backend_Area42_3.Models;
using Backend_Area42_3.Enums;
using Backend_Area42_3.Services;
using Backend_Area42_3.DTO.Input;

namespace Tests;

public class ReservationServiceTests
{
    private readonly Mock<IReservationRepo> _reservationRepoMock;
    private readonly Mock<ITableRepo> _tableRepoMock;
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly ReservationService _service;

    public ReservationServiceTests()
    {
        _reservationRepoMock = new Mock<IReservationRepo>();
        _tableRepoMock = new Mock<ITableRepo>();
        _userRepoMock = new Mock<IUserRepository>();

        _service = new ReservationService(
            _reservationRepoMock.Object,
            _tableRepoMock.Object,
            _userRepoMock.Object
        );
    }

    [Fact]
    public async Task CheckAvailability_TafelVrij_ReturnsTrue()
    {
        // Arrange
        _reservationRepoMock
            .Setup(r => r.GetAll())
            .ReturnsAsync(new List<Reservation>());

        // Act
        var result = await _service.CheckAvailability(1, new DateTime(2025, 6, 15, 19, 0, 0));

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CheckAvailability_TafelBezet_ReturnsFalse()
    {
        // Arrange
        var date = new DateTime(2025, 6, 15, 19, 0, 0);

        _reservationRepoMock
            .Setup(r => r.GetAll())
            .ReturnsAsync(new List<Reservation>
            {
                new Reservation
                {
                    TafelId = 1,
                    StartDate = date,
                    Status = ReservationStatus.Scheduled
                }
            });

        // Act
        var result = await _service.CheckAvailability(1, date);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task Create_UserBestaatNiet_ReturnsNull()
    {
        // Arrange
        _userRepoMock
            .Setup(r => r.GetUserById(99))
            .ReturnsAsync((User?)null);

        var dto = new CreateReservationDto
        {
            UserId = 99,
            TableId = 1,
            StartDate = new DateTime(2025, 6, 15, 19, 0, 0),
            Amount = 4
        };

        // Act
        var result = await _service.Create(dto);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Create_TafelTeKlein_ReturnsNull()
    {
        // Arrange
        _userRepoMock
            .Setup(r => r.GetUserById(1))
            .ReturnsAsync(new User { Id = 1, Name = "Ado", Email = "ado@test.nl", Role = UserRole.Customer, PasswordHash = "hash", Phone = "0612345678" });

        _tableRepoMock
            .Setup(r => r.GetById(1))
            .ReturnsAsync(new Table { Id = 1, MaxGuests = 2 });

        var dto = new CreateReservationDto
        {
            UserId = 1,
            TableId = 1,
            StartDate = new DateTime(2025, 6, 15, 19, 0, 0),
            Amount = 10
        };

        // Act
        var result = await _service.Create(dto);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Create_TafelBezet_ReturnsNull()
    {
        // Arrange
        var date = new DateTime(2025, 6, 15, 19, 0, 0);

        _userRepoMock
            .Setup(r => r.GetUserById(1))
            .ReturnsAsync(new User { Id = 1, Name = "Ado", Email = "ado@test.nl", Role = UserRole.Customer, PasswordHash = "hash", Phone = "0612345678" });

        _tableRepoMock
            .Setup(r => r.GetById(1))
            .ReturnsAsync(new Table { Id = 1, MaxGuests = 6 });

        _reservationRepoMock
            .Setup(r => r.GetAll())
            .ReturnsAsync(new List<Reservation>
            {
                new Reservation { TafelId = 1, StartDate = date, Status = ReservationStatus.Scheduled }
            });

        var dto = new CreateReservationDto
        {
            UserId = 1,
            TableId = 1,
            StartDate = date,
            Amount = 4
        };

        // Act
        var result = await _service.Create(dto);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Create_AllesGoed_ReturnsReservation()
    {
        // Arrange
        var date = new DateTime(2025, 6, 15, 19, 0, 0);

        _userRepoMock
            .Setup(r => r.GetUserById(1))
            .ReturnsAsync(new User { Id = 1, Name = "Ado", Email = "ado@test.nl", Role = UserRole.Customer, PasswordHash = "hash", Phone = "0612345678" });

        _tableRepoMock
            .Setup(r => r.GetById(1))
            .ReturnsAsync(new Table { Id = 1, MaxGuests = 6 });

        _reservationRepoMock
            .Setup(r => r.GetAll())
            .ReturnsAsync(new List<Reservation>());

        _reservationRepoMock
            .Setup(r => r.Add(It.IsAny<Reservation>()))
            .Returns(Task.CompletedTask);

        var dto = new CreateReservationDto
        {
            UserId = 1,
            TableId = 1,
            StartDate = date,
            Amount = 4
        };

        // Act
        var result = await _service.Create(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.UserId);
        Assert.Equal(date, result.StartDate);
    }
}