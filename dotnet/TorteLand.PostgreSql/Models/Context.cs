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
        public virtual DbSet<ArticleBody> ArticleBodies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
                                         {
                                             entity.ToTable("article");

                                             entity.Property(e => e.Id).HasColumnName("id");

                                             entity.Property(e => e.BodyId).HasColumnName("body_id");

                                             entity.Property(e => e.Title)
                                                   .IsRequired()
                                                   .HasMaxLength(100)
                                                   .HasColumnName("title");

                                             entity.HasOne(d => d.Body)
                                                   .WithMany(p => p.Articles)
                                                   .HasForeignKey(d => d.BodyId)
                                                   .OnDelete(DeleteBehavior.ClientSetNull)
                                                   .HasConstraintName("article_body_id");
                                         });

            modelBuilder.Entity<ArticleBody>(entity =>
                                             {
                                                 entity.ToTable("article_body");

                                                 entity.Property(e => e.Id).HasColumnName("id");

                                                 entity.Property(e => e.Body)
                                                       .IsRequired()
                                                       .HasColumnName("body");
                                             });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
