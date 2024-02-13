using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WineryShopMkd.Models;

namespace WineryShopMkd.Data
{
    public class WineryShopMkdContext : DbContext
    {
        public WineryShopMkdContext (DbContextOptions<WineryShopMkdContext> options)
            : base(options)
        {
        }

        public DbSet<WineryShopMkd.Models.Winery> Winery { get; set; } = default!;
    }
}
