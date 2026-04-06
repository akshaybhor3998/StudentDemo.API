using Dapper;
using StudentDemo.API.Model;
using StudentDemo.API.Model.Comman;
using StudentDemo.API.Model.Response;
using StudentDemo.API.Repository.Interface;
using System.Data;

namespace StudentDemo.API.Repository.Class
{
    public class StudentRepository : DapperContext, IStudentRepository
    {
        public StudentRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<GetStudentListResponse>> GetAll()
        {
            var query = "SELECT Id,Name,Email,Age,Course,CreatedDate FROM Students";

            using (var connection = CreateConnection())
            {
                var studentObj = await connection.QueryAsync<GetStudentListResponse>(query);
                return studentObj.ToList();
            }
        }

        public async Task<GetStudentListResponse> GetById(IdDTO dto)
        {
            var query = "SELECT Id,Name,Email,Age,Course,CreatedDate FROM Students WHERE Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", dto.Id, System.Data.DbType.Int32);
            using (var connection = CreateConnection())
            {
                var studentObj = await connection.QueryFirstOrDefaultAsync<GetStudentListResponse>(query, parameters);
                return studentObj;
            }
        }

        public async Task<int> CreateStudent(CreateStudentRequest student)
        {
            var query = "INSERT INTO Students(Name,Email,Age,Course,CreatedDate)VALUES(@Name,@Email,@Age,@Course,@CreatedDate)SELECT SCOPE_IDENTITY();";
            var parameters = new DynamicParameters();
            parameters.Add("Name", student.Name, DbType.String);
            parameters.Add("Email", student.Email, DbType.String);
            parameters.Add("Age", student.Age, DbType.Int32);
            parameters.Add("Course", student.Course, DbType.String);
            parameters.Add("CreatedDate", DateTime.UtcNow, DbType.DateTime);
            using (var connection = CreateConnection())
            {
                var id= await connection.ExecuteAsync(query, student);
                return id;
            }                
        }

        public async Task<int> UpdateStudent(UpdateStudentRequest student)
        {
            var query = "UPDATE Students SET Name=@Name,Email=@Email,Age=@Age,Course=@Course WHERE Id=@Id";

            using (var connection = CreateConnection())
            {
                return await connection.ExecuteAsync(query, student);
            }               
        }

        public async Task<int> Delete(int id)
        {
            var query = "DELETE FROM Students WHERE Id=@Id";

            using (var connection = CreateConnection())
            {
                return await connection.ExecuteAsync(query, new { Id = id });
            }                
        }
    }
}
