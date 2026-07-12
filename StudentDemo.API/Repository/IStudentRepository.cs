using StudentDemo.API.Model;

namespace StudentDemo.API.Repository
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();

        Task<Student?> GetByIdAsync(int id);

        Task<int> CreateAsync(Student student);

        Task UpdateAsync(Student student);

        Task DeleteAsync(int id);
    }
}
