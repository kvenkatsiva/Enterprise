using Enterprise.Data;
using Enterprise.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Enterprise.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class ContactsController : Controller
    {
        private readonly ContactAPIDbContext dbContext;

        public ContactsController(ContactAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContact()
        {
            return Ok(await dbContext.Contact.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
         {
                var contact = await dbContext.Contact.FindAsync(id);

                if (contact == null)
                {
                     return NotFound();
                }
                     return Ok(contact);
         }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactReqeust)
        {
            var contact = new Contacts()
            {
                Id = Guid.NewGuid(),
                Address = addContactReqeust.Address,
                Email = addContactReqeust.Email,
                FullName = addContactReqeust.FullName,
                PhoneNo = addContactReqeust.PhoneNo,
            };
            await dbContext.Contact.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id,UpdateContactRequest updateContactRequest)
        {
            var contact =await dbContext.Contact.FindAsync(id);

            if(contact != null) 
            { 
                contact.FullName = updateContactRequest.FullName;
                contact.PhoneNo = updateContactRequest.PhoneNo;
                contact.Email = updateContactRequest.Email;
                contact.Address=updateContactRequest.Address;

                await dbContext.SaveChangesAsync();

                return Ok(contact);
            }

            return NotFound();

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contact.FindAsync(id);

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
