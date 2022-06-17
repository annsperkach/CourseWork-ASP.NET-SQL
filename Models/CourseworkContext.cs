using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication2.Models
{
    public partial class CourseworkContext : DbContext
    {
        public CourseworkContext()
        {
        }

        public CourseworkContext(DbContextOptions<CourseworkContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CAddressee> CAddressees { get; set; }
        public virtual DbSet<CTopic> CTopics { get; set; }
        public virtual DbSet<VLetter> VLetters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress; Database=Coursework;Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<CAddressee>(entity =>
            {
                entity.ToTable("cAddressee");

                entity.Property(e => e.AddresseeName)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Passport)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CTopic>(entity =>
            {
                entity.ToTable("cTopic");

                entity.Property(e => e.TopicName)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VLetter>(entity =>
            {
                entity.ToTable("vLetter");

                entity.Property(e => e.Answer).IsRequired();

                entity.Property(e => e.CAddressee1Id).HasColumnName("cAddressee1Id");

                entity.Property(e => e.CAddressee2Id).HasColumnName("cAddressee2Id");

                entity.Property(e => e.CTopicId).HasColumnName("cTopicId");

                entity.Property(e => e.DepartureTime).HasColumnType("date");

                entity.Property(e => e.ReceiptTime).HasColumnType("date");

                entity.HasOne(d => d.CAddressee1)
                    .WithMany(p => p.VLetterCAddressee1s)
                    .HasForeignKey(d => d.CAddressee1Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_vLetter_cAddressee1");

                entity.HasOne(d => d.CAddressee2)
                    .WithMany(p => p.VLetterCAddressee2s)
                    .HasForeignKey(d => d.CAddressee2Id)
                    .HasConstraintName("FK_vLetter_cAddressee2");

                entity.HasOne(d => d.CTopic)
                    .WithMany(p => p.VLetters)
                    .HasForeignKey(d => d.CTopicId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_vLetter_cTopic");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
