using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserSignup.Models.Entities;

namespace UserSignup.Repository
{
    public class UserManagementContext : DbContext
    {
        public UserManagementContext(DbContextOptions<UserManagementContext> options) :base(options)
        {

        }
        public DbSet<SignedupUser> SignedupUser { get; set; }

        public DbSet<UserProfile> UserProfile { get; set; }
    }
}
