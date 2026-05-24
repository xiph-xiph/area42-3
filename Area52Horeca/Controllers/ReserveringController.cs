using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Area52Horeca.Interfaces;
using Area52Horeca.Models;

namespace Area52Horeca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReserveringController : ControllerBase
    {
        private readonly IReserveringRepository _repository;

        public ReserveringController(IReserveringRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_repository.GetById(id));
        }

        [HttpPost]
        public IActionResult Create(Reservering reservering)
        {
            _repository.Add(reservering);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Cancel(int id)
        {
            _repository.Delete(id);
            return Ok();
        }
    }
}