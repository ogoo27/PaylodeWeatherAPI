using Microsoft.EntityFrameworkCore;
using PaylodeWeatherAPI.Credential_Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaylodeWeatherAPI.Infrastructure
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<UserModel> userModels { get; set; }
    }
}
