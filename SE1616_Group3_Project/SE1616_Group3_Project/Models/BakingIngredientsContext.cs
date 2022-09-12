using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SE1616_Group3_Project.Models
{
    public partial class BakingIngredientsContext : DbContext
    {
        public BakingIngredientsContext()
        {
        }

        public BakingIngredientsContext(DbContextOptions<BakingIngredientsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Blog> Blogs { get; set; } = null!;
        public virtual DbSet<CartItem> CartItems { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<DeliveryStatus> DeliveryStatuses { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductQuantity> ProductQuantities { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Shop> Shops { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var conf = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
                optionsBuilder.UseSqlServer(conf.GetConnectionString("DbConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>(entity =>
            {
                entity.ToTable("blog");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Detail)
                    .HasColumnType("ntext")
                    .HasColumnName("detail");

                entity.Property(e => e.EnableStatus)
                    .HasColumnName("enable_status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Owner)
                    .HasMaxLength(100)
                    .HasColumnName("owner");

                entity.Property(e => e.PhotoLink)
                    .HasColumnType("ntext")
                    .HasColumnName("photo_link");

                entity.Property(e => e.Title)
                    .HasMaxLength(300)
                    .HasColumnName("title");

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.Owner)
                    .HasConstraintName("FK__blog__owner__5EBF139D");
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.UserEmail })
                    .HasName("PK__cart_ite__6C0DC7D417C63360");

                entity.ToTable("cart_item");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(100)
                    .HasColumnName("user_email");

                entity.Property(e => e.AddedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("added_date");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cart_item__produ__47DBAE45");

                entity.HasOne(d => d.UserEmailNavigation)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.UserEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cart_item__user___48CFD27E");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<DeliveryStatus>(entity =>
            {
                entity.HasKey(e => new { e.OrderItem, e.UpdatedTime })
                    .HasName("PK__delivery__0FE921B1D7B8D891");

                entity.ToTable("delivery_status");

                entity.Property(e => e.OrderItem).HasColumnName("order_item");

                entity.Property(e => e.UpdatedTime)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_time");

                entity.Property(e => e.DeliveryUnit)
                    .HasMaxLength(100)
                    .HasColumnName("delivery_unit");

                entity.Property(e => e.ShippingCompleted).HasColumnName("shipping_completed");

                entity.Property(e => e.ShippingStatus)
                    .HasColumnType("ntext")
                    .HasColumnName("shipping_status");

                entity.HasOne(d => d.OrderItemNavigation)
                    .WithMany(p => p.DeliveryStatuses)
                    .HasForeignKey(d => d.OrderItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__delivery___order__5535A963");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnType("money")
                    .HasColumnName("amount");

                entity.Property(e => e.PaymentMethod).HasColumnName("payment_method");

                entity.Property(e => e.PaymentStatus).HasColumnName("payment_status");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phone")
                    .IsFixedLength();

                entity.Property(e => e.ShipAddress)
                    .HasColumnType("ntext")
                    .HasColumnName("ship_address");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(100)
                    .HasColumnName("user_email");

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .HasColumnName("user_name");

                entity.HasOne(d => d.PaymentMethodNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentMethod)
                    .HasConstraintName("FK__order__payment_m__4E88ABD4");

                entity.HasOne(d => d.UserEmailNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserEmail)
                    .HasConstraintName("FK__order__user_emai__4D94879B");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("order_item");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BoughtDate)
                    .HasColumnType("datetime")
                    .HasColumnName("bought_date");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.PhotoLink)
                    .HasColumnType("ntext")
                    .HasColumnName("photo_link");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(150)
                    .HasColumnName("product_name");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__order_ite__order__52593CB8");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("payment_method");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Method)
                    .HasMaxLength(200)
                    .HasColumnName("method");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Detail)
                    .HasColumnType("ntext")
                    .HasColumnName("detail");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .HasColumnName("name");

                entity.Property(e => e.PhotoLink)
                    .HasColumnType("ntext")
                    .HasColumnName("photo_link");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__product__categor__412EB0B6");
            });

            modelBuilder.Entity<ProductQuantity>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.ShopId, e.UpdateDate })
                    .HasName("PK__product___649AE20D6C8BD589");

                entity.ToTable("product_quantity");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ShopId).HasColumnName("shop_id");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("update_date");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductQuantities)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__product_q__produ__440B1D61");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.ProductQuantities)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__product_q__shop___44FF419A");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.ToTable("shop");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnType("ntext")
                    .HasColumnName("address");

                entity.Property(e => e.StaffEmail)
                    .HasMaxLength(100)
                    .HasColumnName("staff_email");

                entity.HasOne(d => d.StaffEmailNavigation)
                    .WithMany(p => p.Shops)
                    .HasForeignKey(d => d.StaffEmail)
                    .HasConstraintName("FK__shop__staff_emai__3C69FB99");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK__user__AB6E616556B82D4D");

                entity.ToTable("user");

                entity.HasIndex(e => e.Phone, "UQ__user__B43B145F5A2F0575")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Address)
                    .HasColumnType("ntext")
                    .HasColumnName("address");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phone")
                    .IsFixedLength();

                entity.Property(e => e.PhotoLink)
                    .HasColumnType("ntext")
                    .HasColumnName("photo_link");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__user__role_id__398D8EEE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
