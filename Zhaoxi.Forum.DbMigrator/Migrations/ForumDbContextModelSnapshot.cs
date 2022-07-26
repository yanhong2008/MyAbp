﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Volo.Abp.EntityFrameworkCore;
using Zhaoxi.Forum.EntityFrameworkCore;

#nullable disable

namespace Zhaoxi.Forum.DbMigrator.Migrations
{
    [DbContext(typeof(ForumDbContext))]
    partial class ForumDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("_Abp_DatabaseProvider", EfCoreDatabaseProvider.MySql)
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Zhaoxi.Forum.Domain.Category.CategoryEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasComment("主键标识")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.None);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("creation_time")
                        .HasComment("创建时间");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("varchar(2000)")
                        .HasColumnName("description")
                        .HasComment("描叙");

                    b.Property<ulong?>("Enabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit(1)")
                        .HasDefaultValue(1ul)
                        .HasColumnName("enabled")
                        .HasComment("是否启用");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("image")
                        .HasComment("图像");

                    b.Property<ulong?>("IsLocked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit(1)")
                        .HasDefaultValue(0ul)
                        .HasColumnName("is_locked")
                        .HasComment("是否锁定");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("name")
                        .HasComment("名称");

                    b.Property<long?>("ParentCategory")
                        .HasColumnType("bigint")
                        .HasColumnName("parent_category")
                        .HasComment("父板块");

                    b.Property<string>("Pinyin")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("pinyin")
                        .HasComment("拼音");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int")
                        .HasColumnName("sort_order")
                        .HasComment("板块排序");

                    b.HasKey("Id")
                        .HasName("pk_category_id");

                    b.ToTable("category", (string)null);
                });

            modelBuilder.Entity("Zhaoxi.Forum.Domain.Topic.TopicEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasComment("主键标识")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Commits")
                        .HasColumnType("int")
                        .HasColumnName("commits")
                        .HasComment("评论数");

                    b.Property<bool?>("Creamed")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("creamed")
                        .HasComment("是否精华");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("creation_time")
                        .HasComment("创建时间");

                    b.Property<int>("FavouriteTimes")
                        .HasColumnType("int")
                        .HasColumnName("favourite_times")
                        .HasComment("收藏数");

                    b.Property<bool?>("Hoted")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("hoted")
                        .HasComment("是否热门");

                    b.Property<ulong?>("IsLocked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit(1)")
                        .HasDefaultValue(0ul)
                        .HasColumnName("is_locked")
                        .HasComment("是否锁定");

                    b.Property<DateTime?>("LastReciveTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("last_recive_time")
                        .HasComment("最后回复时间");

                    b.Property<int?>("LastReciveUserId")
                        .HasColumnType("int")
                        .HasColumnName("last_recive_userid")
                        .HasComment("最后回复人");

                    b.Property<string>("TopicContent")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("topic_content")
                        .HasComment("内容");

                    b.Property<string>("TopicName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("topic_name")
                        .HasComment("主题");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_date")
                        .HasComment("更新时间");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id")
                        .HasComment("用户id");

                    b.Property<int>("Views")
                        .HasColumnType("int")
                        .HasColumnName("views")
                        .HasComment("流量数");

                    b.Property<long>("category_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id")
                        .HasName("pk_topic_id");

                    b.HasIndex("category_id");

                    b.ToTable("topic", (string)null);
                });

            modelBuilder.Entity("Zhaoxi.Forum.Domain.Topic.TopicEntity", b =>
                {
                    b.HasOne("Zhaoxi.Forum.Domain.Category.CategoryEntity", "Category")
                        .WithMany("Topics")
                        .HasForeignKey("category_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Zhaoxi.Forum.Domain.Category.CategoryEntity", b =>
                {
                    b.Navigation("Topics");
                });
#pragma warning restore 612, 618
        }
    }
}
