﻿// <auto-generated />
using System;
using EFCore.Console;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCore.Console.Migrations
{
    [DbContext(typeof(DomainDbContext))]
    partial class DomainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.CategoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool?>("Enabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool?>("IsLocked")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long?>("ParentCategory")
                        .HasColumnType("bigint");

                    b.Property<string>("Pinyin")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CategoryEntities");
                });

            modelBuilder.Entity("Domain.TopicEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("Commits")
                        .HasColumnType("int");

                    b.Property<bool?>("Creamed")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("FavouriteTimes")
                        .HasColumnType("int");

                    b.Property<bool?>("Hoted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool?>("IsLocked")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastReciveTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("LastReciveUserId")
                        .HasColumnType("int");

                    b.Property<string>("TopicContent")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TopicName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<int>("Views")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("TopicEntities");
                });

            modelBuilder.Entity("Domain.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("BlackTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("FollowTimes")
                        .HasColumnType("int");

                    b.Property<bool?>("IsBlack")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsPass")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("NickName")
                        .HasColumnType("longtext")
                        .HasColumnName("Name")
                        .HasComment("名字");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<int?>("Sex")
                        .HasColumnType("int");

                    b.Property<int?>("TopicTimes")
                        .HasColumnType("int");

                    b.Property<string>("UserAvatar")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("myuser", (string)null);
                });

            modelBuilder.Entity("Domain.TopicEntity", b =>
                {
                    b.HasOne("Domain.CategoryEntity", "Category")
                        .WithMany("Topics")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Domain.CategoryEntity", b =>
                {
                    b.Navigation("Topics");
                });
#pragma warning restore 612, 618
        }
    }
}
