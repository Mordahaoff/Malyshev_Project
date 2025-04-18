using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Malyshev_Project.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<CategoriesOfProduct> CategoriesOfProducts { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrdersProduct> OrdersProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<StatesOfOrder> StatesOfOrders { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<StoresProduct> StoresProducts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=admin;Password=admin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.IdAddress).HasName("addresses_pkey");

            entity.ToTable("addresses");

            entity.Property(e => e.IdAddress).HasColumnName("id_address");
            entity.Property(e => e.Building)
                .HasMaxLength(5)
                .HasColumnName("building");
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .HasColumnName("city");
            entity.Property(e => e.Street)
                .HasMaxLength(30)
                .HasColumnName("street");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.IdBrand).HasName("brands_pkey");

            entity.ToTable("brands");

            entity.Property(e => e.IdBrand).HasColumnName("id_brand");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name_");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("url");
        });

        modelBuilder.Entity<CategoriesOfProduct>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("categories_of_products_pkey");

            entity.ToTable("categories_of_products");

            entity.Property(e => e.IdCategory).HasColumnName("id_category");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name_");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.IdDiscount).HasName("discounts_pkey");

            entity.ToTable("discounts");

            entity.Property(e => e.IdDiscount).HasColumnName("id_discount");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.DateOfEnd).HasColumnName("date_of_end");
            entity.Property(e => e.DateOfStart)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("date_of_start");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Discounts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("discounts_product_id_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.Property(e => e.IdOrder).HasColumnName("id_order");
            entity.Property(e => e.DateOfStatusChange)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_of_status_change");
            entity.Property(e => e.StateOfOrderId)
                .HasDefaultValue(1)
                .HasColumnName("state_of_order_id");
            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.StateOfOrder).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StateOfOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_state_of_order_id_fkey");

            entity.HasOne(d => d.Store).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_store_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_user_id_fkey");
        });

        modelBuilder.Entity<OrdersProduct>(entity =>
        {
            entity.HasKey(e => e.IdOrdersProducts).HasName("orders_products_pkey");

            entity.ToTable("orders_products");

            entity.Property(e => e.IdOrdersProducts).HasColumnName("id_orders_products");
            entity.Property(e => e.CountOfProduct)
                .HasDefaultValue((short)1)
                .HasColumnName("count_of_product");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrdersProducts)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_products_order_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.OrdersProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_products_product_id_fkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("products_pkey");

            entity.ToTable("products");

            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name_");
            entity.Property(e => e.Photo)
                .HasMaxLength(400)
                .HasColumnName("photo");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Volume).HasColumnName("volume");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_brand_id_fkey");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_category_id_fkey");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.IdReview).HasName("reviews_pkey");

            entity.ToTable("reviews");

            entity.Property(e => e.IdReview).HasColumnName("id_review");
            entity.Property(e => e.DateOfCreation)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("date_of_creation");
            entity.Property(e => e.Grade).HasColumnName("grade");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Text).HasColumnName("text_");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reviews_product_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reviews_user_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name_");
        });

        modelBuilder.Entity<StatesOfOrder>(entity =>
        {
            entity.HasKey(e => e.IdState).HasName("states_of_order_pkey");

            entity.ToTable("states_of_order");

            entity.Property(e => e.IdState).HasColumnName("id_state");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name_");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.IdStore).HasName("stores_pkey");

            entity.ToTable("stores");

            entity.Property(e => e.IdStore).HasColumnName("id_store");
            entity.Property(e => e.AddressId).HasColumnName("address_id");

            entity.HasOne(d => d.Address).WithMany(p => p.Stores)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stores_address_id_fkey");
        });

        modelBuilder.Entity<StoresProduct>(entity =>
        {
            entity.HasKey(e => e.IdStoresProducts).HasName("stores_products_pkey");

            entity.ToTable("stores_products");

            entity.Property(e => e.IdStoresProducts).HasColumnName("id_stores_products");
            entity.Property(e => e.CountOfProduct)
                .HasDefaultValue((short)0)
                .HasColumnName("count_of_product");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.StoreId).HasColumnName("store_id");

            entity.HasOne(d => d.Product).WithMany(p => p.StoresProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stores_products_product_id_fkey");

            entity.HasOne(d => d.Store).WithMany(p => p.StoresProducts)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stores_products_store_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .HasColumnName("gender");
            entity.Property(e => e.Login)
                .HasMaxLength(255)
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name_");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password_");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .HasColumnName("patronymic");
            entity.Property(e => e.Photo)
                .HasMaxLength(400)
                .HasDefaultValueSql("'https://cdn-icons-png.flaticon.com/512/149/149071.png'::character varying")
                .HasColumnName("photo");
            entity.Property(e => e.RoleId)
                .HasDefaultValue(1)
                .HasColumnName("role_id");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");
            entity.Property(e => e.Telephone)
                .HasMaxLength(10)
                .HasColumnName("telephone");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
