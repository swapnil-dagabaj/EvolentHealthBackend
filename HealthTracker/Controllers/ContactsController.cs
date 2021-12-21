using HealthTracker.Core.ContactService;
using HealthTracker.Core.Model;
using HealthTracker.Data;
using HealthTrackerAPI;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthTrackerSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        public readonly HealthContext _context;
        private readonly ILogger _logger;


        private readonly IContactService _contactService;

        public ContactsController(ILogger<ContactsController> logger, IContactService contactService)
        {
            _logger = logger;
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActiveContact()
        {
            var contacts = await _contactService.GetAllActiveContact();
            return Ok(contacts);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetContact([FromRoute] int id)
        {
            Contact contact = await _contactService.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }


        [HttpPost]
        public async Task<IActionResult> PostContact([FromBody] Contact detail)
        {
            if (detail == null)
            {
                return BadRequest("Request is not in correct format");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int newContactId = await _contactService.AddNewContact(detail);
            return CreatedAtAction("GetContact", new { id = newContactId }, detail);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact([FromRoute] int id, [FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var data = await _contactService.UpdateContact(id, contact);
            if (data == null)
            {
                return NotFound();
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Contact>> DeleteContact([FromRoute] int id)
        {
            var data = await _contactService.SetContactAsInactive(id);
            return data;
        }
    }
}
