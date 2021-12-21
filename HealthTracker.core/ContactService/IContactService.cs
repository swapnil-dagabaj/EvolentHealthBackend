using HealthTracker.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthTracker.Core.ContactService
{
    public interface IContactService
    {
        Task<IList<Contact>> GetAllContact();

        Task<IList<Contact>> GetAllActiveContact();

        Task<Contact> GetContactById(int id);

        Task<int> AddNewContact(Contact contact);

        Task<object> UpdateContact(int id, Contact contact);

        Task<Contact> DeleteContact(int id);

        Task<Contact> SetContactAsInactive(int id);
    }
}
