﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoApp.Repository;

#nullable disable

namespace ToDoApp.Repository.Migrations
{
    [DbContext(typeof(ToDoAppContext))]
    partial class ToDoAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ToDoApp.Repository.Models.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatusId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusId")
                        .HasName("PK__Status__C8EE2063956ED592");

                    b.ToTable("Status", (string)null);
                });

            modelBuilder.Entity("ToDoApp.Repository.Models.TaskInfo", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaskId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaskName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TaskId")
                        .HasName("PK__TaskTabl__7C6949B1708A6B36");

                    b.ToTable("TaskInfo", (string)null);
                });

            modelBuilder.Entity("ToDoApp.Repository.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId")
                        .HasName("PK__Users__1788CC4CD18FFE6E");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ToDoApp.Repository.Models.UserTask", b =>
                {
                    b.Property<int>("UserTaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserTaskId"));

                    b.Property<DateTime?>("CompletedOn")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.Property<int?>("TaskId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserTaskId")
                        .HasName("PK__UserTask__4EF5961FC10484ED");

                    b.HasIndex("StatusId");

                    b.HasIndex("TaskId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTasks");
                });

            modelBuilder.Entity("ToDoApp.Repository.Models.UserTask", b =>
                {
                    b.HasOne("ToDoApp.Repository.Models.Status", "Status")
                        .WithMany("UserTasks")
                        .HasForeignKey("StatusId")
                        .HasConstraintName("FK__UserTasks__Statu__3F466844");

                    b.HasOne("ToDoApp.Repository.Models.TaskInfo", "Task")
                        .WithMany("UserTasks")
                        .HasForeignKey("TaskId")
                        .HasConstraintName("FK__UserTasks__TaskI__3E52440B");

                    b.HasOne("ToDoApp.Repository.Models.User", "User")
                        .WithMany("UserTasks")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__UserTasks__UserI__3D5E1FD2");

                    b.Navigation("Status");

                    b.Navigation("Task");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ToDoApp.Repository.Models.Status", b =>
                {
                    b.Navigation("UserTasks");
                });

            modelBuilder.Entity("ToDoApp.Repository.Models.TaskInfo", b =>
                {
                    b.Navigation("UserTasks");
                });

            modelBuilder.Entity("ToDoApp.Repository.Models.User", b =>
                {
                    b.Navigation("UserTasks");
                });
#pragma warning restore 612, 618
        }
    }
}
