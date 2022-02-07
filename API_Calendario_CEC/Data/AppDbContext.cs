using System;
using API_Calendario_CEC.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Calendario_CEC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts)
            :base(opts)
        {
        }

        public DbSet<Instrutor> Instrutores { get; set; }
    }
}
