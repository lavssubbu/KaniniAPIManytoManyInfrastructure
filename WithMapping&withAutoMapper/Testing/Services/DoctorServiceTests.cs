using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIMMwithoutJunctionModel.DTOs;
using APIMMwithoutJunctionModel.Interface;
using APIMMwithoutJunctionModel.Models;
using APIMMwithoutJunctionModel.Service;
using FluentAssertions;
using Moq;
using Xunit;

namespace APIMMwithoutJunctionModel.Tests.Services
{
    public class DoctorServiceTests
    {
        [Fact]
        public async Task CreateDoctor_MapsAndReturnsDto()
        {
            var repoMock = new Mock<IDocPatient<Doctor>>();
            repoMock
                .Setup(r => r.Add(It.IsAny<Doctor>()))
                .ReturnsAsync((Doctor d) => { d.DocId = 123; return d; });

            var service = new DoctorService(repoMock.Object);

            var dto = new CreateDocDtoc { DocName = "Dr. Test", Specialization = "Cardio" };
            var result = await service.CreateDoctor(dto);

            result.Should().NotBeNull();
            result.DocId.Should().Be(123);
            result.DocName.Should().Be("Dr. Test");
            result.Specialization.Should().Be("Cardio");

            repoMock.Verify(r => r.Add(It.Is<Doctor>(d => d.DocName == dto.DocName && d.Specialization == dto.Specialization)), Times.Once);
        }

        [Fact]
        public async Task GetAllDoc_MapsPatientsToNames()
        {
            var doctors = new List<Doctor>
            {
                new Doctor
                {
                    DocId = 1,
                    DocName = "Dr. A",
                    Specialization = "Neuro",
                    Patients = new List<Patient>
                    {
                        new Patient { PatId = 10, PatName = "Alice" },
                        new Patient { PatId = 11, PatName = "Bob" }
                    }
                }
            };

            var repoMock = new Mock<IDocPatient<Doctor>>();
            repoMock.Setup(r => r.GetAll()).ReturnsAsync(doctors);

            var service = new DoctorService(repoMock.Object);

            var result = await service.GetAllDoc();

            result.Should().HaveCount(1);
            var first = result.First();
            first.DocName.Should().Be("Dr. A");
            first.Specialization.Should().Be("Neuro");
            first.PatientName.Should().BeEquivalentTo(new[] { "Alice", "Bob" });
        }
    }
}


