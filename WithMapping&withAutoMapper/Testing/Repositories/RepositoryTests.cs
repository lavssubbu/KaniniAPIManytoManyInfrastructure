using System.Linq;
using System.Threading.Tasks;
using APIMMwithoutJunctionModel.Data;
using APIMMwithoutJunctionModel.Models;
using APIMMwithoutJunctionModel.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace APIMMwithoutJunctionModel.Tests.Repositories
{
    public class RepositoryTests
    {
        private static DocPatientContext CreateInMemory()
        {
            var options = new DbContextOptionsBuilder<DocPatientContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            var ctx = new DocPatientContext(options);
            ctx.Database.EnsureCreated();
            return ctx;
        }

        [Fact]
        public async Task DoctorRepository_Add_And_GetAll()
        {
            using var ctx = CreateInMemory();
            var repo = new DoctorRepository(ctx);

            var doctor = new Doctor { DocName = "Dr. Repo", Specialization = "ENT" };
            var added = await repo.Add(doctor);

            added.DocId.Should().BeGreaterThan(0);

            var all = await repo.GetAll();
            all.Should().ContainSingle(d => d.DocName == "Dr. Repo" && d.Specialization == "ENT");
        }

        [Fact]
        public async Task UserRepository_Add_And_GetAll()
        {
            using var ctx = CreateInMemory();
            var repo = new UserRepository(ctx);

            var u = new User { userName = "u1", email = "e@e.com", password = "p", role = "Admin" };
            var added = await repo.Add(u);
            added.userId.Should().BeGreaterThan(0);

            var all = await repo.GetAll();
            all.Should().ContainSingle(x => x.userName == "u1" && x.role == "Admin");
        }

        [Fact]
        public async Task PatientRepository_Add_And_GetAll()
        {
            using var ctx = CreateInMemory();
            var repo = new PatientRepository(ctx);

            var p = new Patient { PatName = "Pat 1" };
            var added = await repo.Add(p);
            added.PatId.Should().BeGreaterThan(0);

            var all = await repo.GetAll();
            all.Should().ContainSingle(x => x.PatName == "Pat 1");
        }
    }
}


