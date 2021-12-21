using HealthTracker.Data;
using HealthTracker.Data.Transfer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using HealthTracker.Core.Model;
using HealthTracker.Data.Entities;
namespace HealthTracker.Core.ContactService
{
    public class ContactService : IContactService
    {

        public readonly IContactTransfer _contactTransfer;

        public ContactService(IContactTransfer contactTransfer)
        {
            _contactTransfer = contactTransfer;
        }

        public async Task<IList<Contact>> GetAllContact()
        {
            var data = await _contactTransfer.GetAllContact();
            IList<Contact> list = data.Select(x => MapContact(x)).ToList();
            return list;
        }

        public async Task<IList<Contact>> GetAllActiveContact()
        {
            var data = await _contactTransfer.GetActiveContact();
            IList<Contact> list = data.Select(x => MapContact(x)).ToList();
            return list;
        }

        public async Task<Contact> GetContactById(int id)
        {
            var result = await _contactTransfer.GetContactById(id);
            return MapContact(result);
        }

        public async Task<int> AddNewContact(Contact contact)
        {
            int identityCount = await _contactTransfer.SaveContact(SetContactForDB(contact));
            if (identityCount > 0)
            {
                contact.Id = identityCount;
            }
            return identityCount;
        }

        public async Task<object> UpdateContact(int id, Contact contact)
        {
            return await _contactTransfer.UpdateContact(id, SetContactForDB(contact));
        }

        public async Task<Contact> DeleteContact(int id)
        {
            var data = await _contactTransfer.DeleteContact(id);
            return MapContact(data);
        }

        public async Task<Contact> SetContactAsInactive(int id)
        {
            var data = await _contactTransfer.SetContactAsInactive(id);
            return MapContact(data);
        }

        private static Contact MapContact(ContactTable contact)
        {
            return new Contact
            {
                Email = contact.Email,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                PhoneNo = contact.PhoneNo,
                Status = contact.Status,
                Id = contact.Id
            };
        }


        private static ContactTable SetContactForDB(Contact contact)
        {
            return new ContactTable
            {
                Email = contact.Email,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                PhoneNo = contact.PhoneNo,
                Status = contact.Status,
                Id = contact.Id
            };
        }


    }
}
