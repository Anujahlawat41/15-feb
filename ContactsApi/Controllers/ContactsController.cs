using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsApi.Data;
using ContactsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactsApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactAPIDbContext dbContext;
        public ContactsController(ContactAPIDbContext dbContacts)
        {
            this.dbContext = dbContacts;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.contacts.ToListAsync());

        }
        [HttpGet]
        [Route(" {id:Guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await dbContext.contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contact()
            {
                ID = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                FullName = addContactRequest.FullName,
                Phone = addContactRequest.Phone
            };
            await dbContext.contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route(" {id:Guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = await dbContext.contacts.FindAsync(id);
            if (contact != null)
            {
                contact.FullName = updateContactRequest.FullName;
                contact.Address = updateContactRequest.Address;
                contact.Phone = updateContactRequest.Phone;
                contact.Email = updateContactRequest.Email;

                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }
        [HttpDelete]
        [Route(" {id:guid}")]
        
            public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        { 
            var contact = await dbContext.contacts.FindAsync(id);
            if (contact != null)
            {
                dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
                return Ok(contact);

            }
                     return NotFound();
         }

    }
}

