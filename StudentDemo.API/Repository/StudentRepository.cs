using Dapper;
using StudentDemo.API.Model;

namespace StudentDemo.API.Repository
{
    public class StudentRepository: IStudentRepository
    {
        private readonly DapperContext _context;

        public StudentRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            using var connection = _context.CreateConnection();

            var sql = "SELECT * FROM Students";

            return await connection.QueryAsync<Student>(sql);
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            using var connection = _context.CreateConnection();

            var sql = "SELECT * FROM Students WHERE Id=@Id";

            return await connection.QueryFirstOrDefaultAsync<Student>(
                sql,
                new { Id = id });
        }

        public async Task<int> CreateAsync(Student student)
        {
            using var connection = _context.CreateConnection();

            var sql = @"INSERT INTO Students
                    (Name,Age,Email,Course)
                    VALUES
                    (@Name,@Age,@Email,@Course);

                    SELECT CAST(SCOPE_IDENTITY() as int);";

            return await connection.ExecuteScalarAsync<int>(sql, student);
        }

        public async Task UpdateAsync(Student student)
        {
            using var connection = _context.CreateConnection();

            var sql = @"UPDATE Students
                    SET Name=@Name,
                        Age=@Age,
                        Email=@Email,
                        Course=@Course
                    WHERE Id=@Id";

            await connection.ExecuteAsync(sql, student);
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = _context.CreateConnection();

            var sql = "DELETE FROM Students WHERE Id=@Id";

            await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
