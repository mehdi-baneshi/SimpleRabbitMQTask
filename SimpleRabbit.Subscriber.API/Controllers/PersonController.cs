using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRabbit.Subscriber.Domain.Interfaces.Services;
using SimpleRabbit.Subscriber.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SimpleRabbit.Subscriber.Domain.Dtos.Person;

namespace SimpleRabbit.Subscriber.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        // GET api/GetPersonsFromSqlDB
        [HttpGet("GetPersonsFromSqlDB")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersonsFromSqlDB()
        {
            var response=await _personService.GetPersonsFromSqlDB();

            return Ok(response);
        }

        // GET api/GetPersonsFromRedis
        [HttpGet("GetPersonsFromRedis")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersonsFromRedis()
        {
            var response = await _personService.GetPersonsFromRedis();

            return Ok(response);
        }
    }
}
