﻿using Microsoft.EntityFrameworkCore;

namespace SimplePainterServer.Model;

public class MainDateBase(DbContextOptions<MainDateBase> options) : DbContext(options)
{
    public DbSet<Guess> Guesses { get; set; } = null!;
    public DbSet<ImageInfo> ImageInfos { get; set; } = null!;
    public DbSet<UserInfo> UserInfos { get; set; } = null!;
    public DbSet<WordInfo> WordInfos { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserInfo>()
            .HasMany(e => e.Guesses)
            .WithOne(e => e.User)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<UserInfo>()
            .HasMany(e => e.Images)
            .WithOne(e => e.User)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Guess>()
            .HasOne(e => e.Image)
            .WithMany(e => e.Guesses)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<ImageInfo>()
            .HasOne(e => e.Word)
            .WithMany(e => e.Images)
            .OnDelete(DeleteBehavior.Restrict);
    }
}