using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Domain.Models
{
    public partial class Mydb1Context : DbContext
    {
        public Mydb1Context()
        {
        }

        public Mydb1Context(DbContextOptions<Mydb1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<CategoryEntity> CategoryEntities { get; set; }
        public virtual DbSet<EfmigrationsHistory> EfmigrationsHistories { get; set; }
        public virtual DbSet<TopicEntity> TopicEntities { get; set; }
        public virtual DbSet<UserEntity> UserEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=175.24.32.140;port=49156;database=Mydb1;uid=root;pwd=111111", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.36-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<CategoryEntity>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Image).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.ParentCategory).HasColumnType("bigint(20)");

                entity.Property(e => e.Pinyin).IsRequired();

                entity.Property(e => e.SortOrder).HasColumnType("int(11)");
            });

            modelBuilder.Entity<EfmigrationsHistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__EFMigrationsHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<TopicEntity>(entity =>
            {
                entity.HasIndex(e => e.CategoryId, "IX_TopicEntities_CategoryId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CategoryId).HasColumnType("int(11)");

                entity.Property(e => e.Commits).HasColumnType("int(11)");

                entity.Property(e => e.FavouriteTimes).HasColumnType("int(11)");

                entity.Property(e => e.LastReciveTime).HasMaxLength(6);

                entity.Property(e => e.LastReciveUserId).HasColumnType("int(11)");

                entity.Property(e => e.TopicContent).IsRequired();

                entity.Property(e => e.TopicName).IsRequired();

                entity.Property(e => e.UpdateDate).HasMaxLength(6);

                entity.Property(e => e.UserId).HasColumnType("bigint(20)");

                entity.Property(e => e.Views).HasColumnType("int(11)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TopicEntities)
                    .HasForeignKey(d => d.CategoryId);
            });

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.BlackTime).HasMaxLength(6);

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.FollowTimes).HasColumnType("int(11)");

                entity.Property(e => e.NickName).IsRequired();

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Phone).IsRequired();

                entity.Property(e => e.Sex).HasColumnType("int(11)");

                entity.Property(e => e.TopicTimes).HasColumnType("int(11)");

                entity.Property(e => e.UserAvatar).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
