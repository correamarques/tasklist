using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace App.Models
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext() : base("name=TaskDbContext")
        {
#if DEBUG
            // drop database and recreate always in debug mode, after seed database
            Database.SetInitializer<TaskDbContext>(new DropCreateDatabaseIfModelChanges<TaskDbContext>());
            Database.SetInitializer(new SeedDatabtase());
#endif
        }

        public class SeedDatabtase : DropCreateDatabaseAlways<TaskDbContext>
        {
            protected override void Seed(TaskDbContext context)
            {
                IList<Task> tasks = new List<Task>
                {
                    new Task { Title = "CRUD Task", Description = "A lot of things to do", CreatedAt = DateTime.Now.AddDays(-10), UpdatedAt = DateTime.Now },
                    new Task { Title = "Model task", Description = "model with attributes", Completed = true, CompletedAt = DateTime.Now, CreatedAt = DateTime.Now.AddDays(-8), UpdatedAt = DateTime.Now },
                    new Task { Title = "index return fake list of tasks", Completed = true, CompletedAt = DateTime.Now.AddHours(-4), CreatedAt = DateTime.Now.AddDays(-6), UpdatedAt = DateTime.Now.AddHours(-3) }
                };

                context.Tasks.AddRange(tasks);

                base.Seed(context);
            }
        }

        public DbSet<App.Models.Task> Tasks { get; set; }
    }
}
