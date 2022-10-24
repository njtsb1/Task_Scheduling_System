using Microsoft.EntityFrameworkCore;
using TaskSchedulingSystem.Models;

namespace TaskSchedulingSystem.Context
{
    public class ContextOrganizer : DbContext
    {
        public ContextOrganizer(DbContextOptions<ContextOrganizer> options) : base(options)
        {
            
        }

        public DbSet<Task> Tasks { get; set; }
    }
}