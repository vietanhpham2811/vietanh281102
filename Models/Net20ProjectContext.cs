using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QlBanOpDaDienThoai.Models;

public partial class Net20ProjectContext : DbContext
{
    public Net20ProjectContext()
    {
    }

    public Net20ProjectContext(DbContextOptions<Net20ProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adv> Advs { get; set; }

    public virtual DbSet<CategoriesProduct> CategoriesProducts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrdersDetail> OrdersDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Slide> Slides { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagsProduct> TagsProducts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-AKDPMAJ\\SQLEXPRESS;Initial Catalog=net20_project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adv>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Adv");
               

            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.Photo).HasMaxLength(500);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.DisplayHomePage).HasDefaultValueSql("((0))");
            entity.Property(e => e.Name).HasMaxLength(500);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_customer");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_News_1");

            entity.Property(e => e.Content).HasColumnType("ntext");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.Photo).HasMaxLength(500);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_orders");

            entity.Property(e => e.Create).HasColumnType("date");
            entity.Property(e => e.Status).HasDefaultValueSql("((0))");
        });

        modelBuilder.Entity<OrdersDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_orders_detail");

            entity.ToTable("OrdersDetail");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_news");

            entity.Property(e => e.Content).HasColumnType("ntext");
            entity.Property(e => e.Description).HasMaxLength(4000);
            entity.Property(e => e.Discount).HasDefaultValueSql("((0))");
            entity.Property(e => e.Hot).HasDefaultValueSql("((0))");
            entity.Property(e => e.Name).HasMaxLength(4000);
            entity.Property(e => e.Photo).HasMaxLength(4000);
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.ToTable("Rating");
        });

        modelBuilder.Entity<Slide>(entity =>
        {
            entity.Property(e => e.Info).HasMaxLength(500);
            entity.Property(e => e.Link).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.Photo).HasMaxLength(500);
            entity.Property(e => e.SubTitle).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(500);
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(500);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.Password).HasMaxLength(500);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
