using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PhotoSystem.Models
{
    public partial class fotodbContext : DbContext
    {
        public fotodbContext()
        {
        }

        public fotodbContext(DbContextOptions<fotodbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EmpUser> EmpUser { get; set; }
        public virtual DbSet<FoClient> FoClient { get; set; }
        public virtual DbSet<ImageTempPass> ImageTempPass { get; set; }
        public virtual DbSet<TempPass> TempPass { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:tozodb.database.windows.net,1433;Initial Catalog=fotodb;Persist Security Info=False;User ID=mydb;Password=toZocon2244!!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmpUser>(entity =>
            {
                entity.ToTable("EMP_USER");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__EMP_USER__161CF724D6D701A8")
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .HasName("UQ__EMP_USER__B15BE12E596724E3")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientId).HasColumnName("CLIENT_ID");

                entity.Property(e => e.CompanyName)
                    .HasColumnName("COMPANY_NAME")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("EMAIL")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("USERNAME")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.EmpUser)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EMP_USER__CLIENT__02925FBF");
            });

            modelBuilder.Entity<FoClient>(entity =>
            {
                entity.ToTable("FO_CLIENT");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(55)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ImageTempPass>(entity =>
            {
                entity.HasKey(e => new { e.ImageLink, e.TempPasswordId, e.OriginalFileName })
                    .HasName("PK__IMAGE_TE__73FB8CD68D8DE9FB");

                entity.ToTable("IMAGE_TEMP_PASS");

                entity.Property(e => e.ImageLink)
                    .HasColumnName("IMAGE_LINK")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TempPasswordId).HasColumnName("TEMP_PASSWORD_ID");

                entity.Property(e => e.OriginalFileName)
                    .HasColumnName("ORIGINAL_FILE_NAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.TempPassword)
                    .WithMany(p => p.ImageTempPass)
                    .HasForeignKey(d => d.TempPasswordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__IMAGE_TEM__TEMP___084B3915");
            });

            modelBuilder.Entity<TempPass>(entity =>
            {
                entity.ToTable("TEMP_PASS");

                entity.HasIndex(e => e.TempPassword)
                    .HasName("UQ__TEMP_PAS__C4333C6A78822EE7")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustEmail)
                    .HasColumnName("CUST_EMAIL")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustName)
                    .HasColumnName("CUST_NAME")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TempPassword)
                    .IsRequired()
                    .HasColumnName("TEMP_PASSWORD")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserCompanyName)
                    .HasColumnName("USER_COMPANY_NAME")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
