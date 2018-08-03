using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Nokody.API.Domain.Model.Models
{
  public partial class NokodyModel : DbContext
  {
    //public NokodyModel()
    //    : base("name=NokodyModelConnection")
    //{
    //}



    public NokodyModel(DbContextOptions<NokodyModel> options)
  : base(options)
    {

    }

    public virtual DbSet<Account> Accounts { get; set; }
    public virtual DbSet<Transaction> Transactions { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserIdentity> UserIdentities { get; set; }
    public DbSet<UserType> UserTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
      {
        relationship.DeleteBehavior = DeleteBehavior.Restrict;
      }

      // modelBuilder.Entity<VehicleFeature>().HasKey(vf => new { vf.VehicleId, vf.FeatureId });
      // modelBuilder.Entity<Vehicle>()
      //.HasMany(a => a.Features)
      //.WithOne(c => c.Vehicle)
      //.OnDelete(DeleteBehavior.Cascade);

      base.OnModelCreating(modelBuilder);
    }
  }
}
