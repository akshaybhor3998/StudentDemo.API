using StudentDemo.API.Model;
using StudentDemo.API.Model.Comman;
using StudentDemo.API.Model.Response;

namespace StudentDemo.API.Service.Interface
{
    public interface IStudentService
    {
        Task<List<GetStudentListResponse>> GetAll();
        Task<GetStudentListResponse> GetById(IdDTO dto);
        Task<int> CreateStudent(CreateStudentRequest student);
        Task<int> UpdateStudent(UpdateStudentRequest student);
        Task<int> Delete(int id);
    }
}
