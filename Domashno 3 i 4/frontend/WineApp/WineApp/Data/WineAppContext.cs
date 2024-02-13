using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WineApp.Models;

namespace WineApp.Data
{
    public class WineAppContext : DbContext
    {
        public WineAppContext (DbContextOptions<WineAppContext> options)
            : base(options)
        {
        }

        public DbSet<WineApp.Models.Winery> Winery { get; set; } = default!;
    }
}
