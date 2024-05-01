using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using task_tracker_group.Models;

namespace task_tracker_group.Services.Context
{
    public class DataContext : DbContext


    {
        public DbSet<UserModel> UserInfo { get; set; }
        public DbSet<TaskModel> TaskInfo { get; set; }
        public DbSet<CommentModel> CommentInfo { get; set; }

        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }



    }
}