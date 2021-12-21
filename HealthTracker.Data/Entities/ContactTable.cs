using System;
using System.Collections.Generic;
using System.Text;

namespace HealthTracker.Data.Entities
{
    public class ContactTable
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public bool Status { get; set; } = true;
    }
}
