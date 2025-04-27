using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ISPMs.Models;

public partial class ManagementSystemContext : DbContext
{
    public ManagementSystemContext()
    {
    }

    public ManagementSystemContext(DbContextOptions<ManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<ScheduleJob> ScheduleJobs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ep-floral-silence-a4gqrm43-pooler.us-east-1.aws.neon.tech;Database=ManagementSystem;Username=ManagementSystem_owner;Password=npg_QeO8HdVAoCR7");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("customers_pkey");

            entity.ToTable("customers");

            entity.Property(e => e.CustomerId)
                .ValueGeneratedNever()
                .HasColumnName("customer_id");
            entity.Property(e => e.ActivationDate)
                .HasMaxLength(200)
                .HasColumnName("activation_date");
            entity.Property(e => e.Address)
                .HasMaxLength(70)
                .HasColumnName("address");
            entity.Property(e => e.Cnic)
                .HasMaxLength(200)
                .HasColumnName("cnic");
            entity.Property(e => e.CustomerStatus)
                .HasMaxLength(60)
                .HasColumnName("customer_status");
            entity.Property(e => e.ExpiryDate)
                .HasMaxLength(200)
                .HasColumnName("expiry_date");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(200)
                .HasColumnName("mobile_no");
            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .HasColumnName("name");
            entity.Property(e => e.PackageId).HasColumnName("package_id");
            entity.Property(e => e.PartnerId).HasColumnName("partner_id");
            entity.Property(e => e.Password).HasColumnName("password");

            entity.HasOne(d => d.Package).WithMany(p => p.Customers)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_package");

            entity.HasOne(d => d.Partner).WithMany(p => p.Customers)
                .HasForeignKey(d => d.PartnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_partner");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("invoices_pkey");

            entity.ToTable("invoices");

            entity.Property(e => e.InvoiceId)
                .ValueGeneratedNever()
                .HasColumnName("invoice_id");
            entity.Property(e => e.InvoiceAmount)
                .HasMaxLength(50)
                .HasColumnName("invoice_amount");
            entity.Property(e => e.InvoiceDate)
                .HasMaxLength(50)
                .HasColumnName("invoice_date");
            entity.Property(e => e.InvoiceStatus)
                .HasMaxLength(100)
                .HasColumnName("invoice_status");
            entity.Property(e => e.PartnerId).HasColumnName("partner_id");

            entity.HasOne(d => d.Partner).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.PartnerId)
                .HasConstraintName("fk_invoice_partner");
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("packages_pkey");

            entity.ToTable("packages");

            entity.Property(e => e.PackageId)
                .ValueGeneratedNever()
                .HasColumnName("package_id");
            entity.Property(e => e.PackageName)
                .HasMaxLength(100)
                .HasColumnName("package_name");
            entity.Property(e => e.PackagePrice)
                .HasMaxLength(80)
                .HasColumnName("package_price");
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.PartnerId).HasName("partner_pkey");

            entity.ToTable("partner");

            entity.Property(e => e.PartnerId)
                .ValueGeneratedNever()
                .HasColumnName("partner_id");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .HasColumnName("address");
            entity.Property(e => e.Cnic)
                .HasMaxLength(100)
                .HasColumnName("cnic");
            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .HasColumnName("email");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(80)
                .HasColumnName("mobile_no");
            entity.Property(e => e.PName)
                .HasMaxLength(50)
                .HasColumnName("p_name");
            entity.Property(e => e.Type)
                .HasMaxLength(90)
                .HasColumnName("type");
        });

        modelBuilder.Entity<ScheduleJob>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("schedule_jobs_pkey");

            entity.ToTable("schedule_jobs");

            entity.Property(e => e.ScheduleId)
                .ValueGeneratedNever()
                .HasColumnName("schedule_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.PackageId).HasColumnName("package_id");
            entity.Property(e => e.ScheduleOn)
                .HasMaxLength(100)
                .HasColumnName("schedule_on");
            entity.Property(e => e.Status)
                .HasMaxLength(90)
                .HasColumnName("status");
            entity.Property(e => e.TaskType)
                .HasMaxLength(90)
                .HasColumnName("task_type");

            entity.HasOne(d => d.Customer).WithMany(p => p.ScheduleJobs)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("fk_schedule_customer");

            entity.HasOne(d => d.Package).WithMany(p => p.ScheduleJobs)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("fk_schedule_package");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
