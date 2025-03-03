using Microsoft.EntityFrameworkCore;

namespace SimplePainterServer.Model;

public class MainDateBase(DbContextOptions<MainDateBase> options) : DbContext(options)
{
    public DbSet<Guess>          Guesses        { get; set; } = null!;
    public DbSet<ImageInfo>      ImageInfos     { get; set; } = null!;
    public DbSet<UserInfo>       UserInfos      { get; set; } = null!;
    public DbSet<WordInfo>       WordInfos      { get; set; } = null!;
    public DbSet<WordCreateTime> WordCreateTimes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserInfo>().HasMany(e => e.Guesses).WithOne(e => e.User).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<UserInfo>().HasMany(e => e.Images).WithOne(e => e.User).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Guess>().HasOne(e => e.Image).WithMany(e => e.Guesses).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<ImageInfo>().HasOne(e => e.Word).WithMany(e => e.Images).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<WordCreateTime>().HasOne(e=>e.WordInfo).WithMany(e=>e.WordCreateTime).OnDelete(DeleteBehavior.Cascade);
    }
}