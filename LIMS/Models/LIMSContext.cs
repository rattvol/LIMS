using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LIMS.Models
{
    public partial class LIMSContext : DbContext
    {
        //public LIMSContext()
        //{
        //}

        public LIMSContext(DbContextOptions<LIMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Groupnom> Groupnom { get; set; }
        public virtual DbSet<Nomencl> Nomencl { get; set; }
        public virtual DbSet<Prices> Prices { get; set; }
        public virtual DbSet<Shipdoc> Shipdoc { get; set; }
        public virtual DbSet<Shipment> Shipment { get; set; }
        public virtual DbSet<Supplyer> Supplyer { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Groupnom>(entity =>
            {
                entity.ToTable("groupnom");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Nomencl>(entity =>
            {
                entity.ToTable("nomencl");

                entity.HasIndex(e => e.Groupnomid)
                    .HasName("nomencl_fk");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Groupnomid)
                    .HasColumnName("groupnomid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Groupnom)
                    .WithMany(p => p.Nomencl)
                    .HasForeignKey(d => d.Groupnomid)
                    .HasConstraintName("nomencl_fk");
            });

            modelBuilder.Entity<Prices>(entity =>
            {
                entity.ToTable("prices");

                entity.HasIndex(e => e.Nomenclid)
                    .HasName("prices_fk_1");

                entity.HasIndex(e => e.Supplyerid)
                    .HasName("prices_fk");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nomenclid)
                    .HasColumnName("nomenclid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(12,2)"); ;

                entity.Property(e => e.Supplyerid)
                    .HasColumnName("supplyerid")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Nomencl)
                    .WithMany(p => p.Prices)
                    .HasForeignKey(d => d.Nomenclid)
                    .HasConstraintName("prices_fk_1");

                entity.HasOne(d => d.Supplyer)
                    .WithMany(p => p.Prices)
                    .HasForeignKey(d => d.Supplyerid)
                    .HasConstraintName("prices_fk");
            });

            modelBuilder.Entity<Shipdoc>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PRIMARY");

                entity.ToTable("shipdoc");

               entity.Property(e => e.Shipmentid)
                    .HasColumnName("shipmentid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nomenclid)
                    .HasColumnName("nomenclid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(12,2)");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasColumnType("decimal(12,3)");

                entity.HasOne(d => d.Nomencl)
                    .WithMany(p => p.Shipdoc)
                    .HasForeignKey(d => d.Nomenclid)
                    .HasConstraintName("shipdoc_fk_1");

                entity.HasOne(d => d.Shipment)
                    .WithMany(p => p.Shipdocs)
                    .HasForeignKey(d => d.Shipmentid)
                    .HasConstraintName("shipdoc_fk");
            });

            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.ToTable("shipment");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Suppdate)
                .HasColumnName("suppdate")
                .HasColumnType("double");

                entity.Property(e => e.Supplyerid)
                    .HasColumnName("supplyerid")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Supplyer)
                    .WithMany(p => p.Shipment)
                    .HasForeignKey(d => d.Supplyerid)
                    .HasConstraintName("shipment_fk");
            });

            modelBuilder.Entity<Supplyer>(entity =>
            {
                entity.ToTable("supplyer");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
