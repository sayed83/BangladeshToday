using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BangladeshToday.Models
{
    public partial class bangladeshtodayContext : DbContext
    {
        public virtual DbSet<Allvideo> Allvideo { get; set; }
        public virtual DbSet<Newsinfo> Newsinfo { get; set; }
        public virtual DbSet<UserDetails> UserDetails { get; set; }
        public virtual DbSet<Videonews> Videonews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=bangladeshtoday;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Allvideo>(entity =>
            {
                entity.HasKey(e => new { e.VideoId, e.VideoSerial });

                entity.ToTable("allvideo");

                entity.Property(e => e.VideoId).HasColumnName("videoId");

                entity.Property(e => e.VideoSerial)
                    .HasColumnName("videoSerial")
                    .HasMaxLength(200);

                entity.Property(e => e.VideoPath).HasColumnName("videoPath");

                entity.HasOne(d => d.Video)
                    .WithMany(p => p.Allvideo)
                    .HasForeignKey(d => d.VideoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__allvideo__videoI__5441852A");
            });

            modelBuilder.Entity<Newsinfo>(entity =>
            {
                entity.HasKey(e => e.Newsserial);

                entity.ToTable("newsinfo");

                entity.Property(e => e.Newsserial).HasColumnName("newsserial");

                entity.Property(e => e.Author)
                    .HasColumnName("author")
                    .HasMaxLength(200);

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasColumnName("category")
                    .HasMaxLength(200);

                entity.Property(e => e.Datetime)
                    .HasColumnName("datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.Keyword)
                    .IsRequired()
                    .HasColumnName("keyword");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title");


                entity.Property(e => e.CaptionPicture).HasColumnName("captionPicture");
                entity.Property(e => e.Editor).HasColumnName("editor");
                entity.Property(e => e.FeatureNews).HasColumnName("featureNews");

            });

            

            modelBuilder.Entity<UserDetails>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(200)
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(200);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(200);

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Videonews>(entity =>
            {
                entity.ToTable("videonews");

                entity.Property(e => e.VideoNewsId).ValueGeneratedNever();

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasColumnName("category")
                    .HasMaxLength(200);

                entity.Property(e => e.Datetime)
                    .HasColumnName("datetime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Keyword)
                    .IsRequired()
                    .HasColumnName("keyword");

                entity.Property(e => e.Path).HasColumnName("path");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title");
            });
        }
    }
}
