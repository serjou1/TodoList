﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoList.DAL;

namespace TodoList.DAL.Migrations
{
    [DbContext(typeof(TodoListContext))]
    partial class TodoListContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("TodoList.DAL.Models.TaskDal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDone")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TaskDescription")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UserDalId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("UserDalId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TodoList.DAL.Models.UserDal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsAdmin = false,
                            UserName = "TestUser"
                        },
                        new
                        {
                            Id = 2,
                            IsAdmin = true,
                            UserName = "TestAdmin"
                        });
                });

            modelBuilder.Entity("TodoList.DAL.Models.TaskDal", b =>
                {
                    b.HasOne("TodoList.DAL.Models.UserDal", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.HasOne("TodoList.DAL.Models.UserDal", null)
                        .WithMany("Tasks")
                        .HasForeignKey("UserDalId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("TodoList.DAL.Models.UserDal", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
