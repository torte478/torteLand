using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TorteLand.PostgreSql.Models
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(
                entity =>
                {
                    entity.ToTable("article");

                    entity.Property(e => e.Id).HasColumnName("id");

                    entity.Property(e => e.Body)
                          .HasMaxLength(1000)
                          .HasColumnName("body");

                    entity.Property(e => e.Title)
                          .HasMaxLength(64)
                          .HasColumnName("title");
                });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
