using Microsoft.EntityFrameworkCore;
using Entities;
using System;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class ApplicationDbContextActivities: DbContext
    {
        public ApplicationDbContextActivities(DbContextOptions<ApplicationDbContextActivities> options): base(options)
        {
            
        }        

        public DbSet<Property> Property { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<Survey> Survey { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            

            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();                
                var connectionString = configuration.GetConnectionString("DefaultConneccion");             
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        
    }

   
    
}
