using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GrpcService.Models
{
    public partial class PRN231_Lab02_gRPCContext : DbContext
    {
        public PRN231_Lab02_gRPCContext()
        {
        }

        public PRN231_Lab02_gRPCContext(DbContextOptions<PRN231_Lab02_gRPCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server =moclananhh; database = PRN231_Lab02_gRPC;uid=sa;pwd=123456;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.AId);

                entity.Property(e => e.AId).HasColumnName("a_id");

                entity.Property(e => e.AName)
                    .HasMaxLength(50)
                    .HasColumnName("a_name");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.BId);

                entity.Property(e => e.BId).HasColumnName("b_id");

                entity.Property(e => e.AId).HasColumnName("a_id");

                entity.Property(e => e.BName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("b_name");

                entity.Property(e => e.BPages).HasColumnName("b_pages");

                entity.Property(e => e.BPrice).HasColumnName("b_price");

                entity.Property(e => e.BVersion).HasColumnName("b_version");

                entity.Property(e => e.BYear).HasColumnName("b_year");

                entity.HasOne(d => d.AIdNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AId)
                    .HasConstraintName("FK_Books_Authors");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
