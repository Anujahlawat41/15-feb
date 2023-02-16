using System;
using ContactsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Data
{
    public class ContactAPIDbContext : DbContext
    {
        public ContactAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> contacts { get; set; }
    }
}



 