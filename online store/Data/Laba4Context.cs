using Laba4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Laba4.Data
{
    public class Laba4Context : DbContext
    {
        public Laba4Context(DbContextOptions<Laba4Context> options) : base(options)
        {
        }
        public DbSet<Buyer> Buyers {get; set;}
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<DeliveryService> DeliveryServices { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
