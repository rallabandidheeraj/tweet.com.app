using com.tweetapp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.tweetapp.Models
{
    public partial class TweetContext : DbContext
    {
        public TweetContext()
        {
        }

        public TweetContext(DbContextOptions<TweetContext> options)
            : base(options)
        {
        }


        public virtual DbSet<UserInfo> Users { get; set; }
        public virtual DbSet<Tweet> Tweets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LTIN309231\\SQLEXPRESS;Database=Twitter;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.EmailId);
                    

                entity.ToTable("Users");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Passcode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasMany(d => d.Tweets)
                   .WithOne(p => p.User);
                   

            });

            modelBuilder.Entity<Tweet>(entity =>
            {
                entity.HasKey(e=>e.TweetId);
                entity.ToTable("Tweet");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.TweetId)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.TweetMessage)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TweetTime).HasColumnType("datetime");
                entity.HasOne(x => x.User)
                .WithMany(x => x.Tweets)
                .HasForeignKey(x => x.EmailId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
