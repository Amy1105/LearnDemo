
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDemo
{
    public class Helper
    {
        public static SchoolContext GetContext()
        {
            SqliteConnection connection = new SqliteConnection("Data Source=School.db");
            connection.Open();
            var builder = new DbContextOptionsBuilder(new DbContextOptions<SchoolContext>());
            builder.UseSqlite(connection);
            return new SchoolContext(builder.Options as DbContextOptions<SchoolContext>);
        }
    }
}
