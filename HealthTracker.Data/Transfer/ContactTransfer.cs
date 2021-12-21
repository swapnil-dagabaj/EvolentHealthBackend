using HealthTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthTracker.Data.Transfer
{
    public class ContactTransfer : IContactTransfer
    {
        public readonly HealthContext _context;
        private readonly ILogger _logger;

        public ContactTransfer(HealthContext context, ILogger<ContactTransfer> logger)
        {
            _context = context;
            _context.Database.EnsureCreated();
            _logger = logger;
        }

        public async Task<IList<ContactTable>> GetAllContact()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<IList<ContactTable>> GetActiveContact()
        {
            try
            {
                return await _context.Contacts.Where(c => c.Status).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while getting contacts");
                throw ex;
            }
        }

        public async Task<ContactTable> GetContactById(int id)
        {
            try
            {
                return await _context.Contacts.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while getting contacts by Id");
                throw ex;
            }
        }

        public async Task<int> SaveContact(ContactTable contact)
        {
            try
            {
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while saving contact");
                throw ex;
            }
            return contact.Id;
        }

        public async Task<object> UpdateContact(int id, ContactTable contact)
        {
            ContactTable _contact = null;
            try
            {
                _contact = await _context.Contacts.FindAsync(id);
                if (_contact == null) return null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while getting contact by id");
                throw ex;
            }

            _contact.FirstName = contact.FirstName;
            _contact.LastName = contact.LastName;
            _contact.Email = contact.Email;
            _contact.PhoneNo = contact.PhoneNo;

            _context.Entry(_contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return null;
                }
                _logger.LogError("Error while updating contact due to concurrency issue");
                throw;
            }
            return true;
        }

        public async Task<ContactTable> DeleteContact(int id)
        {
            try
            {
                ContactTable contact = await _context.Contacts.FindAsync(id);
                if (contact == null) return null;
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
                return contact;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while deleting contact");
                throw ex;
            }
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(c => c.Id == id);
        }

        public async Task<ContactTable> SetContactAsInactive(int id)
        {
            try
            {
                ContactTable contact = await _context.Contacts.FindAsync(id);
                if (contact == null) return null;

                contact.Status = false;
                _context.Entry(contact).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return contact;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while setting contact status inactive");
                throw ex;
            }
        }
    }
}
