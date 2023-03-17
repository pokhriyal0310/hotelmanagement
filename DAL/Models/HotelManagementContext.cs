using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DAL.Models
{
    public partial class HotelManagementContext : DbContext
    {
        public HotelManagementContext()
        {
        }

        public HotelManagementContext(DbContextOptions<HotelManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee1> Employee1s { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=LIN25005355\\SQLEXPRESS;database=HotelManagement;trusted_connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AI");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.AdminId).HasColumnName("Admin_Id");

                entity.Property(e => e.AdminName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Admin_Name");

                entity.Property(e => e.AdminType)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Admin_Type");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");

                entity.Property(e => e.DateOfBooking).HasColumnType("date");

                entity.Property(e => e.RoomNo).HasColumnName("Room_No");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Booking__Custome__46E78A0C");

                entity.HasOne(d => d.RoomNoNavigation)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.RoomNo)
                    .HasConstraintName("FK__Booking__Room_No__45F365D3");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");

                entity.Property(e => e.CustomerAddress)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Address");

                entity.Property(e => e.CustomerContact).HasColumnName("Customer_Contact");

                entity.Property(e => e.CustomerDob)
                    .HasColumnType("date")
                    .HasColumnName("Customer_DOB");

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Email");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Name");

                entity.Property(e => e.HotelId).HasColumnName("Hotel_Id");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK__Customer__Hotel___3B75D760");
            });

            modelBuilder.Entity<Employee1>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK__Employee__262359ABCFB5CCC0");

                entity.ToTable("Employee1");

                entity.Property(e => e.EmpId).HasColumnName("Emp_Id");

                entity.Property(e => e.EmpGrade)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Emp_Grade");

                entity.Property(e => e.EmpName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Emp_Name");

                entity.Property(e => e.HotelId).HasColumnName("Hotel_Id");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Employee1s)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK__Employee1__Hotel__3E52440B");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.ToTable("hotel");

                entity.Property(e => e.HotelId).HasColumnName("Hotel_Id");

                entity.Property(e => e.HotelAddress)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Hotel_Address");

                entity.Property(e => e.HotelName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Hotel_Name");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.RoomNo)
                    .HasName("PK__Rooms__19EF81FCDB32458B");

                entity.Property(e => e.RoomNo).HasColumnName("Room_No");

                entity.Property(e => e.HotelId).HasColumnName("Hotel_Id");

                entity.Property(e => e.RoomPrice).HasColumnName("Room_Price");

                entity.Property(e => e.RoomType)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Room_Type");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK__Rooms__Hotel_Id__440B1D61");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
