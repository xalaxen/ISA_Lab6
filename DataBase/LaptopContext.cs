using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    internal class LaptopContext : DbContext
    {
        public LaptopContext() : base("DbConnection") { }
        public DbSet<Laptop> Laptops { get; set; }
    }
}
