using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Area52Horeca.Interfaces;
using Area52Horeca.Models;

namespace Area52Horeca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TijdslotController : ControllerBase
    {
        private readonly ITijdslotRepository _repository;

        public TijdslotController(ITijdslotRepository repository)
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

        [HttpGet("available")]
        public IActionResult GetAvailable(DateTime datum)
        {
            return Ok(_repository.GetAvailable(datum));
        }
    }
}