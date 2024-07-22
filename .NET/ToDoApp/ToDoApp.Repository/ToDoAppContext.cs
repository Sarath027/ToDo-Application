using Microsoft.EntityFrameworkCore;
using ToDoApp.Repository.Models;

namespace ToDoApp.Repository;

public partial class ToDoAppContext : DbContext
{
    public ToDoAppContext()
    {
    }

    public ToDoAppContext(DbContextOptions<ToDoAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<TaskInfo> TaskInfos { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserTask> UserTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Status__C8EE2063956ED592");

            entity.ToTable("Status");
        });

        modelBuilder.Entity<TaskInfo>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__TaskTabl__7C6949B1708A6B36");

            entity.ToTable("TaskInfo");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CD18FFE6E");
        });

        modelBuilder.Entity<UserTask>(entity =>
        {
            entity.HasKey(e => e.UserTaskId).HasName("PK__UserTask__4EF5961FC10484ED");

            entity.Property(e => e.CompletedOn).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Status).WithMany(p => p.UserTasks)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__UserTasks__Statu__3F466844");

            entity.HasOne(d => d.Task).WithMany(p => p.UserTasks)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__UserTasks__TaskI__3E52440B");

            entity.HasOne(d => d.User).WithMany(p => p.UserTasks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserTasks__UserI__3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
