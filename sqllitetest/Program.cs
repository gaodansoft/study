using ApplicationCore.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace sqllitetest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //DbContextOptionsBuilder dob = new DbContextOptionsBuilder();

            //options.UseSqlite
            User u = new User();
           // u.ID = 1;
            u.Pwd = "aaaa";
            u.Remark = "";
            u.LastLoginTime = DateTime.Now;
            u.Status = 1;
            u.AccountNum = "bbb";
            MyContext mc = new MyContext();
            mc.User.Add(u);
            mc.SaveChanges();
          var t=   mc.User.FirstOrDefault();

        }
    }


    public class MyContext : DbContext
    {
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string cntString = "Data Source=maintenance.db";
            optionsBuilder.UseSqlite(cntString);
        }
    }
}
