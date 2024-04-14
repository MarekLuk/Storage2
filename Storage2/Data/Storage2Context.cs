using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Storage2.Models;

namespace Storage2.Data
{
    public class Storage2Context : DbContext
    {
        public Storage2Context (DbContextOptions<Storage2Context> options)
            : base(options)
        {
        }

        public DbSet<Storage2.Models.Product> Product { get; set; } = default!;
    }
}
