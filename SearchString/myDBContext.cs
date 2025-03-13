using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOL.Entity.DomainModels;

namespace SearchString
{
    public class myDBContext:DbContext
    {
        public myDBContext(DbContextOptions<myDBContext> options)
           : base(options)
        {
        }

        public DbSet<Sys_Text_Main> sys_Text_Mains { get; set; }
        public DbSet<Sys_Text> sys_Texts { get; set; }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sys_Text_Main>().ToTable("Sys_Text_Main");
            modelBuilder.Entity<Sys_Text>().ToTable("Sys_Text");
        }
    }
}
