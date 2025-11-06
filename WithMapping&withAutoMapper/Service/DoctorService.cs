using APIMMwithoutJunctionModel.DTOs;
using APIMMwithoutJunctionModel.Interface;
using APIMMwithoutJunctionModel.Models;
using AutoMapper;

namespace APIMMwithoutJunctionModel.Service
{
    public class DoctorService : IService
    {
        private readonly IDocPatient<Doctor> _repo;
        private readonly IMapper _mapper;

        public DoctorService(IDocPatient<Doctor> repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<DoctorDto> CreateDoctor(CreateDocDtoc doc)
        {
          //mapping my createdto to the model doctor
            //var doct = new Doctor
            //{
            //    DocName = doc.DocName,
            //    Specialization = doc.Specialization,

            //};
          var doct =  _mapper.Map<Doctor>(doc);
            //passing doctor model in repo
           var newdoc = await _repo.Add(doct);//return model with id column
            //mapping doctormodel to doctordto with id
            return _mapper.Map<DoctorDto>(newdoc);
            //return new DoctorDto
            //{
            //    DocId = newdoc.DocId,
            //    DocName = newdoc.DocName,
            //    Specialization = newdoc.Specialization,
            //};
        }

        public async Task<IEnumerable<GetDocDto>> GetAllDoc()
        {
          var doc= await _repo.GetAll();//Ienumerable<doctor>
            //mapping doctor to GetDocDto - which will b returned to controller (developer side)
            //return doc.Select(dp => new GetDocDto()
            //{
            //    DocName = dp.DocName,
            //    Specialization = dp.Specialization,
            //    PatientName = dp.Patients!.Select(p => p.PatName).ToList()!
            //});
            return _mapper.Map<IEnumerable<GetDocDto>>(doc);
        }
    }
}
