using HealthTracker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthTracker.Data.Migration
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactTable>().HasData(
                new ContactTable
                {
                    Id = 1,
                    FirstName = "Swapnil",
                    LastName = "Dagabaj",
                    Status = true,
                    PhoneNo = "9323893235",
                    Email = "swapnil.dagabaj@gmail.com"
                },
                new ContactTable
                {
                    Id = 2,
                    FirstName = "Pallavi",
                    LastName = "Shinde",
                    Status = true,
                    PhoneNo = "8329919272",
                    Email = "pallavi.shinde@gmail.com"
                },
                new ContactTable
                {
                    Id = 3,
                    FirstName = "Reyansh",
                    LastName = "Gaikwad",
                    Status = true,
                    PhoneNo = "9323893235",
                    Email = "Reyansh.Gaikwad@gmail.com"
                },
                new ContactTable
                {
                    Id = 4,
                    FirstName = "Monica",
                    LastName = "Krishan",
                    Status = true,
                    PhoneNo = "9323893235",
                    Email = "Monica.Krishan@gmail.com"
                },
                new ContactTable
                {
                    Id = 5,
                    FirstName = "Atul",
                    LastName = "Shinde",
                    Status = true,
                    PhoneNo = "8329919272",
                    Email = "Atul.shinde@gmail.com"
                });
        }
    }
}
