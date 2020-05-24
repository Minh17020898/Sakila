using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Sakila.Models
{
    public partial class SakilaContext : DbContext
    {
        public SakilaContext()
        {
        }

        public SakilaContext(DbContextOptions<SakilaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Chapter> Chapter { get; set; }
        public virtual DbSet<ChapterProgress> ChapterProgress { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<CoursePermission> CoursePermission { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<GroupMessage> GroupMessage { get; set; }
        public virtual DbSet<GroupPermission> GroupPermission { get; set; }
        public virtual DbSet<GroupRequest> GroupRequest { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<SystemMessage> SystemMessage { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserMessage> UserMessage { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("data source=localhost;initial catalog=Sakila;persist security info=True;user id=sa;password=123456a@;multipleactiveresultsets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Chapter>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Chapter)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Chapter_Course");
            });

            modelBuilder.Entity<ChapterProgress>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ChapterId });

                entity.HasOne(d => d.Chapter)
                    .WithMany(p => p.ChapterProgress)
                    .HasForeignKey(d => d.ChapterId)
                    .HasConstraintName("FK_ChapterProgress_Chapter");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ChapterProgress)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChapterProgress_User");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateCreated)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Course)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_User");
            });

            modelBuilder.Entity<CoursePermission>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.CourseId });

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CoursePermission)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_CoursePermission_Course");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CoursePermission)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CoursePermission_User");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateCreated)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Group)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Group_Course");
            });

            modelBuilder.Entity<GroupMessage>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.DateSent)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupMessage)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_GroupMessage_Group");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupMessage)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupMessage_User");
            });

            modelBuilder.Entity<GroupPermission>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.GroupId })
                    .HasName("PK_UserInGroup");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupPermission)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_UserInGroup_Group");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupPermission)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserInGroup_User");
            });

            modelBuilder.Entity<GroupRequest>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.GroupId });

                entity.Property(e => e.DateRequested)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupRequest)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_GroupRequest_Group");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupRequest)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupRequest_User");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Content1)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Content2)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Chapter)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.ChapterId)
                    .HasConstraintName("FK_Question_Chapter");
            });

            modelBuilder.Entity<SystemMessage>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.DateSent)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SystemMessage)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SystemMessage_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateCreated)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<UserMessage>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.DateSent)
                    .IsRequired()
                    .IsRowVersion();
            });
        }
    }
}
