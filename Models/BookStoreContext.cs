using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Models
{
    public class BookStoreContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public BookStoreContext()
        {
        }

        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=book-store;Username=postgres;Password=23082003");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountId).HasName("Account_pkey");
                entity.ToTable("Account", "book-store--dev");
                entity.Property(e => e.AccountId)
                    .HasDefaultValueSql("uuid_generate_v4()")
                    .HasColumnName("AccountID");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId).HasName("Categories_pkey");
                entity.ToTable("Categories", "book-store--dev");
                entity.Property(e => e.CategoryId)
                    .HasDefaultValueSql("uuid_generate_v4()")
                    .HasColumnName("CategoryID");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId).HasName("Customers_pkey");
                entity.ToTable("Customers", "book-store--dev");
                entity.Property(e => e.CustomerId)
                    .HasDefaultValueSql("uuid_generate_v4()")
                    .HasColumnName("CustomerID");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId).HasName("Orders_pkey");
                entity.ToTable("Orders", "book-store--dev");
                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("OrderID");
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
                entity.Property(e => e.Freight).HasColumnType("money");
                entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("CustomerID");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId }).HasName("Order Details_pkey");
                entity.ToTable("Order Details", "book-store--dev");
                entity.Property(e => e.OrderId)
                    .HasDefaultValueSql("uuid_generate_v4()")
                    .HasColumnName("OrderID");
                entity.Property(e => e.ProductId).HasColumnName("ProductID");
                entity.Property(e => e.Quantity).HasDefaultValue((short)0);
                entity.Property(e => e.UnitPrice).HasColumnType("money");
                entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OrderID");
                entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductID");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId).HasName("Products_pkey");
                entity.ToTable("Products", "book-store--dev");
                entity.Property(e => e.ProductId)
                    .HasDefaultValueSql("uuid_generate_v4()")
                    .HasColumnName("ProductID");
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
                entity.Property(e => e.QuantityPerUnit).HasDefaultValue((short)0);
                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
                entity.Property(e => e.UnitPrice).HasColumnType("money");
                entity.HasOne(d => d.Category).WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CategoryID");
                entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SupplierID");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.SupplierId).HasName("Suppliers_pkey");
                entity.ToTable("Suppliers", "book-store--dev");
                entity.Property(e => e.SupplierId)
                    .HasDefaultValueSql("uuid_generate_v4()")
                    .HasColumnName("SupplierID");
            });

            // Configure Identity related entities
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "AspNetUsers"); // Map ApplicationUser to AspNetUsers table
            });

            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable("AspNetRoles"); // Map IdentityRole to AspNetRoles table
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("AspNetUserRoles"); // Map UserRole to AspNetUserRoles table
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("AspNetUserClaims"); // Map UserClaim to AspNetUserClaims table
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("AspNetUserLogins"); // Map UserLogin to AspNetUserLogins table
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("AspNetUserTokens"); // Map UserToken to AspNetUserTokens table
            });

            // Additional configurations for Identity
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.Role).HasColumnName("Role"); // Map Role property to existing column
            });
        }
    }
}
