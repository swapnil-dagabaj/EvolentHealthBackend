using HealthTracker.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthTracker.Data.Transfer
{
    public interface IContactTransfer
    {
        Task<ContactTable> DeleteContact(int id);
        Task<ContactTable> SetContactAsInactive(int id);
        Task<IList<ContactTable>> GetAllContact();
        Task<IList<ContactTable>> GetActiveContact();
        Task<ContactTable> GetContactById(int id);
        Task<int> SaveContact(ContactTable contact);
        Task<object> UpdateContact(int id, ContactTable contact);
    }
}