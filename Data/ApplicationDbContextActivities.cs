using Microsoft.EntityFrameworkCore;
using Entities;
using System;

namespace Data
{
    public class ApplicationDbContextActivities: DbContext
    {
        public ApplicationDbContextActivities(DbContextOptions<ApplicationDbContextActivities> options): base(options)
        {

        }

        public DbSet<Property> Property { get; set; }
    }
    
}
