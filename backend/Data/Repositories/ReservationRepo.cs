using System;
using System.Collections.Generic;
using System.Text;
using Backend_Area42_3.Models;
using Backend_Area42_3.Enums;
using Npgsql;

namespace Backend_Area42_3.Repositories;

public class ReservationRepo(NpgsqlDataSource dataSource) : IReservationRepo
{
    private readonly NpgsqlDataSource dataSource = dataSource;

    public async Task<List<Reservation>> GetAll()
    {
        var result = new List<Reservation>();
        using var connection = await dataSource.OpenConnectionAsync();
        string query = "SELECT id, user_id, table_id, start_date, amount, restaurant::text, status::text FROM reservations";
        using var command = new NpgsqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            result.Add(new Reservation
            {
                Id = reader.GetInt32(0),
                UserId = reader.GetInt32(1),
                TableId = reader.GetInt32(2),
                StartDate = reader.GetDateTime(3),
                Amount = reader.GetInt32(4),
                Restaurant = Enum.Parse<Restaurant>(reader.GetString(5), true),
                Status = Enum.Parse<ReservationStatus>(reader.GetString(6), true)
            });
        }
        return result;
    }

    public async Task<Reservation?> GetById(int id)
    {
        using var connection = await dataSource.OpenConnectionAsync();
        string query = "SELECT id, user_id, table_id, start_date, amount, restaurant::text, status::text FROM reservations WHERE id = @id";
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);
        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Reservation
            {
                Id = reader.GetInt32(0),
                UserId = reader.GetInt32(1),
                TableId = reader.GetInt32(2),
                StartDate = reader.GetDateTime(3),
                Amount = reader.GetInt32(4),
                Restaurant = Enum.Parse<Restaurant>(reader.GetString(5), true),
                Status = Enum.Parse<ReservationStatus>(reader.GetString(6), true)
            };
        }
        return null;
    }

    public async Task Add(Reservation reservation)
    {
        using var connection = await dataSource.OpenConnectionAsync();
        string query = @"INSERT INTO reservations (user_id, table_id, start_date, amount, restaurant, status)
                         VALUES (@userId, @tableId, @startDate, @amount, @restaurant::restaurant, @status::reservation_status)";
        using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@userId", reservation.UserId);
        command.Parameters.AddWithValue("@tableId", reservation.TableId);
        command.Parameters.AddWithValue("@startDate", reservation.StartDate);
        command.Parameters.AddWithValue("@amount", reservation.Amount);
        command.Parameters.AddWithValue("@restaurant", reservation.Restaurant.ToString().ToLowerInvariant());
        command.Parameters.AddWithValue("@status", reservation.Status.ToString().ToLowerInvariant());
        await command.ExecuteNonQueryAsync();
    }
}