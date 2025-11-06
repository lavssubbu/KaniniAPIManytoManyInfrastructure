using APIMMwithoutJunctionModel.Models;
using Microsoft.EntityFrameworkCore;

namespace APIMMwithoutJunctionModel.Data
{
    public class DocPatientContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<User> Users { get; set; }
        public DocPatientContext(DbContextOptions<DocPatientContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>()
                .HasMany(p => p.Patients)
                .WithMany(d => d.Doctors)
                .UsingEntity(dp => dp.ToTable("DocPatient"));

            modelBuilder.Entity<Patient>()
                .HasData(new Patient() { PatId = 11, PatName = "Liya" });

            modelBuilder.Entity<Doctor>()
                .HasData(new Doctor() { DocId = 1, DocName = "Prem", Specialization = "Cardio" });
        }
    }
}
