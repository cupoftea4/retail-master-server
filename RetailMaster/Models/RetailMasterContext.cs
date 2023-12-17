using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RetailMaster.Models;

public partial class RetailMasterContext : DbContext
{
    public RetailMasterContext(DbContextOptions<RetailMasterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<EventReminder> EventReminders { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceProduct> InvoiceProducts { get; set; }

    public virtual DbSet<PersonalDiscount> PersonalDiscounts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<Receipt> Receipts { get; set; }

    public virtual DbSet<ReceiptProduct> ReceiptProducts { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<User> Users { get; set; }

    private string hostConnectionString = "server=localhost;port=3307;database=retail_master;user=root;password=1234";
    
    private string dockerConnectionString = "server=db;port=3306;database=retail_master;user=root;password=1234";
    // private readonly string? _connectionString = _config["RetailMasterDB"];

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (dockerConnectionString == null)
        {
            throw new InvalidOperationException("Database connection string 'RetailMasterDB' is not set.");
        }
        optionsBuilder.UseMySql(hostConnectionString, ServerVersion.Parse("8.2.0-mysql"));
    }    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PRIMARY");

            entity.ToTable("clients");

            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.SecondName)
                .HasMaxLength(50)
                .HasColumnName("second_name");
        });

        modelBuilder.Entity<EventReminder>(entity =>
        {
            entity.HasKey(e => e.ReminderId).HasName("PRIMARY");

            entity.ToTable("event_reminders");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.ReminderId).HasColumnName("reminder_id");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasColumnName("description");
            entity.Property(e => e.Frequency)
                .HasDefaultValueSql("'daily'")
                .HasColumnType("enum('daily','weekly','monthly','yearly')")
                .HasColumnName("frequency");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.EventReminders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("event_reminders_ibfk_1");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PRIMARY");

            entity.ToTable("invoices");

            entity.HasIndex(e => e.ShopId, "shop_id");

            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.Date)
                .HasMaxLength(10)
                .HasColumnName("date");
            entity.Property(e => e.Note)
                .HasMaxLength(50)
                .HasColumnName("note");
            entity.Property(e => e.Printed)
                .HasDefaultValueSql("'0'")
                .HasColumnName("printed");
            entity.Property(e => e.ShopId).HasColumnName("shop_id");
            
        });

        modelBuilder.Entity<InvoiceProduct>(entity =>
        {
            entity.HasKey(e => e.InvoiceProductId).HasName("PRIMARY");

            entity.ToTable("invoice_products");

            entity.HasIndex(e => e.InvoiceId, "invoice_id");

            entity.HasIndex(e => e.ProductId, "product_id");

            entity.Property(e => e.InvoiceProductId).HasColumnName("invoice_product_id");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.RetailPrice).HasColumnName("retail_price");
            entity.Property(e => e.WholeReceiptProductPrice).HasColumnName("wholereceipt_product_price");
            entity.Property(e => e.WrittenOff)
                .HasDefaultValueSql("'0'")
                .HasColumnName("written_off");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceProducts)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoice_products_ibfk_1");

            entity.HasOne(d => d.Product).WithMany(p => p.InvoiceProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoice_products_ibfk_2");
        });

        modelBuilder.Entity<PersonalDiscount>(entity =>
        {
            entity.HasKey(e => e.DiscountId).HasName("PRIMARY");

            entity.ToTable("personal_discounts");

            entity.HasIndex(e => e.ClientId, "client_id");

            entity.Property(e => e.DiscountId).HasColumnName("discount_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.DiscountRate).HasColumnName("discount_rate");

            entity.HasOne(d => d.Client).WithMany(p => p.PersonalDiscounts)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personal_discounts_ibfk_1");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.ToTable("products");

            entity.HasIndex(e => e.Barcode, "barcode").IsUnique();

            entity.HasIndex(e => e.CategoryId, "category_id");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Barcode)
                .HasMaxLength(14)
                .HasColumnName("barcode");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("product_categories");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Receipt>(entity =>
        {
            entity.HasKey(e => e.ReceiptId).HasName("PRIMARY");

            entity.ToTable("receipts");

            entity.HasIndex(e => e.ClientId, "client_id");

            entity.HasIndex(e => e.SellerId, "seller_id");

            entity.HasIndex(e => e.ShopId, "shop_id");

            entity.Property(e => e.ReceiptId).HasColumnName("receipt_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.SellerId).HasColumnName("seller_id");
            entity.Property(e => e.ShopId).HasColumnName("shop_id");

            entity.HasOne(d => d.Client).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("receipts_ibfk_1");

            entity.HasOne(d => d.Seller).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("receipts_ibfk_3");
            
        });

        modelBuilder.Entity<ReceiptProduct>(entity =>
        {
            entity.HasKey(e => e.ReceiptProductId).HasName("PRIMARY");

            entity.ToTable("receipt_products");

            entity.HasIndex(e => e.DiscountId, "discount_id");

            entity.HasIndex(e => e.ProductId, "product_id");

            entity.HasIndex(e => e.ReceiptId, "receipt_id");

            entity.Property(e => e.ReceiptProductId).HasColumnName("receipt_product_id");
            entity.Property(e => e.Date)
                .HasMaxLength(10)
                .HasColumnName("date");
            entity.Property(e => e.DiscountId).HasColumnName("discount_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ReceiptId).HasColumnName("receipt_id");

            entity.HasOne(d => d.Discount).WithMany(p => p.ReceiptProducts)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("receipt_products_ibfk_3");

            entity.HasOne(d => d.Product).WithMany(p => p.ReceiptProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("receipt_products_ibfk_2");

            entity.HasOne(d => d.Receipt).WithMany(p => p.ReceiptProducts)
                .HasForeignKey(d => d.ReceiptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("receipt_products_ibfk_1");
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.ShopId).HasName("PRIMARY");

            entity.ToTable("shops");

            entity.Property(e => e.ShopId).HasColumnName("shop_id");
            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.ShopId, "shop_id");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasColumnType("enum('admin','seller','worker')")
                .HasColumnName("role");
            entity.Property(e => e.SecondName)
                .HasMaxLength(50)
                .HasColumnName("second_name");
            entity.Property(e => e.ShopId).HasColumnName("shop_id");
            
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
