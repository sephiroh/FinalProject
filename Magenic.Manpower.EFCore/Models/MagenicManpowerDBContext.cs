using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Magenic.Manpower.EFCore.Models
{
    public partial class MagenicManpowerDBContext : DbContext
    {
        public virtual DbSet<Applicant> Applicant { get; set; }
        public virtual DbSet<ApplicantLevel> ApplicantLevel { get; set; }
        public virtual DbSet<MagenicRegion> MagenicRegion { get; set; }
        public virtual DbSet<ManpowerRequest> ManpowerRequest { get; set; }
        public virtual DbSet<ManpowerRequestTechnology> ManpowerRequestTechnology { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<PrimarySkill> PrimarySkill { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ReferenceNumber> ReferenceNumber { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RolePermission> RolePermission { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Technology> Technology { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<HrDashboardView> HrDashboardView { get; set; }
        public virtual DbSet<TaggedApplicant> TaggedApplicant { get; set; }
        public virtual DbSet<TaggedApplicantView> TaggedApplicantView { get; set; }
        public virtual DbSet<ApplicantStatus> ApplicantStatus { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applicant>(entity =>
            {
                entity.Property(e => e.ContactNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CurrentPosition)
                    .HasMaxLength(50);

                entity.Property(e => e.PrimarySkillId).IsRequired();

                entity.Property(e => e.CurrentCompany)
                    .HasMaxLength(50);

                entity.Property(e => e.YearsITExperience)
                    .HasMaxLength(10);

                entity.Property(e => e.YearsForSpecificSkills)
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .HasDefaultValueSql("1")
                    ;
                entity.Property(e => e.LevelId)
                    .HasDefaultValueSql("1");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");
                entity.Property(e => e.DateUpdated).HasColumnType("datetime");
                entity.Property(e => e.HireDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ApplicantLevel>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MagenicRegion>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<ManpowerRequest>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.IsChangeRequest).HasDefaultValueSql("0");

                entity.Property(e => e.IsForAdditionalResource).HasDefaultValueSql("0");

                entity.Property(e => e.IsForReplacement).HasDefaultValueSql("0");

                entity.Property(e => e.JobDescription).IsRequired();

                entity.Property(e => e.PrimarySkillId).HasColumnName("PrimarySkillID");

                entity.Property(e => e.ProjectedStartDate).HasColumnType("datetime");

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.HasOne(d => d.PrimarySkill)
                    .WithMany(p => p.ManpowerRequest)
                    .HasForeignKey(d => d.PrimarySkillId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ManpowerRequest_PrimarySkill");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ManpowerRequest)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ManpowerRequest_Project");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.ManpowerRequest)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ManpowerRequest_MagenicRegion");
            });

            modelBuilder.Entity<ManpowerRequestTechnology>(entity =>
            {
                entity.HasKey(e => new { e.ManpowerRequestId, e.TechnologyId })
                    .HasName("PK_ManpowerRequestTechnology");

                entity.Property(e => e.ManpowerRequestId).HasColumnName("ManpowerRequestID");

                entity.Property(e => e.TechnologyId).HasColumnName("TechnologyID");

                entity.HasOne(d => d.ManpowerRequest)
                    .WithMany(p => p.ManpowerRequestTechnology)
                    .HasForeignKey(d => d.ManpowerRequestId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ManpowerRequestTechnology_ManpowerRequest");

                entity.HasOne(d => d.Technology)
                    .WithMany(p => p.ManpowerRequestTechnology)
                    .HasForeignKey(d => d.TechnologyId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ManpowerRequestTechnology_Technology");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("NonClusteredIndex-20170117-180611")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PrimarySkill>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("NonClusteredIndex-20170117-175933")
                    .IsUnique();

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("1");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<ReferenceNumber>(entity =>
            {
                entity.HasIndex(e => e.ReferenceString)
                    .HasName("NonClusteredIndex-20170119-180342")
                    .IsUnique();

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.ReferenceString)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.ReferenceNumber)
                    .HasForeignKey(d => d.LevelId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ReferenceNumber_ApplicantLevel");

                entity.HasOne(d => d.ManpowerRequest)
                    .WithMany(p => p.ReferenceNumber)
                    .HasForeignKey(d => d.ManpowerRequestId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ReferenceNumber_ManpowerRequest");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ReferenceNumber)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ReferenceNumber_Status");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("NonClusteredIndex-20170117-180016")
                    .IsUnique();

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasDefaultValueSql("1");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.PermissionId })
                    .HasName("PK_RolePermission");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RolePermission)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RolePermission_Permission");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermission)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RolePermission_Role");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Technology>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("NonClusteredIndex-20170117-180503")
                    .IsUnique();

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateUpdated).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("NonClusteredIndex-20170117-180429")
                    .IsUnique();

                entity.Property(e => e.ContactNo).HasMaxLength(20);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.IsActive).HasDefaultValueSql("1");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnType("binary(32)");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnType("binary(16)");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_User_Role");
            });

            modelBuilder.Entity<TaggedApplicantView>(entity =>
            {
                entity.Property(e => e.TagDate).HasDefaultValue(DateTime.Now);
            });

            modelBuilder.Entity<ApplicantStatus>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}