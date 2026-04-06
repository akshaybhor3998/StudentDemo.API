using StudentDemo.API.Model;
using StudentDemo.API.Model.Comman;
using StudentDemo.API.Model.Response;
using StudentDemo.API.Repository.Interface;
using StudentDemo.API.Service.Interface;

namespace StudentDemo.API.Service.Class
{
    public class StudentService: IStudentService
    {
        private readonly IStudentRepository _iStudentRepository;
        public StudentService(IStudentRepository iStudentRepository)
        {
            _iStudentRepository = iStudentRepository;
        }

        public async Task<List<GetStudentListResponse>> GetAll()=>await _iStudentRepository.GetAll();
        public async Task<GetStudentListResponse> GetById(IdDTO dto)=>await _iStudentRepository.GetById(dto);
        public async Task<int> CreateStudent(CreateStudentRequest student)=>await _iStudentRepository.CreateStudent(student);
        public async Task<int> UpdateStudent(UpdateStudentRequest student) => await _iStudentRepository.UpdateStudent(student);
        public async Task<int> Delete(int id)=>await _iStudentRepository.Delete(id);
    }
}
