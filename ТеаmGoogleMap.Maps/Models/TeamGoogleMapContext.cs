namespace ТеаmGoogleMap.Maps.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TeamGoogleMapContext : DbContext
    {
        public TeamGoogleMapContext()
            : base("name=TeamGoogleMapContext")
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Street> Streets { get; set; }
        public virtual DbSet<Subdivision> Subdivisions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .Property(e => e.Latitude)
                .HasPrecision(18, 8);

            modelBuilder.Entity<Address>()
                .Property(e => e.Longitude)
                .HasPrecision(18, 8);
        }
    }
}
