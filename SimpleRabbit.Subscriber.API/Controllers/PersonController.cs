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

        // GET api/GetPersons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            var response=await _personService.GetPersons();

            return Ok(response);
        }
    }
}
