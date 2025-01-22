using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Prueba.Api.Models;

namespace Prueba.Api.DataAccess
{
    public class DbContexts(DbContextOptions options) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Tasks
            var taskModel = modelBuilder.Entity<TaskModel>();
            taskModel.ToTable("Tasks").HasKey(task => task.Id);
            #endregion

            #region State
            var stateModel = modelBuilder.Entity<StateModel>();
            stateModel.ToTable("States").HasKey(state => state.Id);
            stateModel.HasMany(state => state.Tasks).WithOne(task => task.State).HasForeignKey(fk => fk.StateId);
            #endregion

            base.OnModelCreating(modelBuilder);
        }

        #region DbSets
        public DbSet<TaskModel> Tasks { get; set; }

        public DbSet<StateModel> States { get; set; }
        #endregion
    }
}
