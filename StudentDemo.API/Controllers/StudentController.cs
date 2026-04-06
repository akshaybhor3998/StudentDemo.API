using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentDemo.API.Model;
using StudentDemo.API.Model.Comman;
using StudentDemo.API.Service.Interface;

namespace StudentDemo.API.Controllers
{
    [Authorize]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;
        private readonly ILogger<StudentController> _logger;
        public StudentController(IStudentService service, ILogger<StudentController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var studentObj = await _service.GetAll();
                if (studentObj == null)
                {
                    return Ok(new ApiResponse() { Success = false, Errors = new List<string>() { "Getting all student data get error, Pls try again...!" } });
                }
                else
                {
                    return Ok(new ApiResponse() { Success = true, Result = studentObj });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Get All Data API Error : " + ex);
                throw;
            }

        }

        [HttpPost]
        [Route("api/getById")]
        public async Task<IActionResult> GetById(IdDTO dto)
        {
            var studentObj = await _service.GetById(dto);
            if (studentObj == null)
            {
                return Ok(new ApiResponse() { Success = false, Errors = new List<string>() { "getById student data api getting error, Pls try again...!" } });
            }
            else
            {
                return Ok(new ApiResponse() { Success = true, Result = studentObj });
            }
        }

        [HttpPost]
        [Route("api/save")]
        public async Task<IActionResult> CreateStudent(CreateStudentRequest dto)
        {
            try
            {
                var studentObj = await _service.CreateStudent(dto);
                if (studentObj == 0)
                {
                    return Ok(new ApiResponse() { Success = false, Errors = new List<string>() { "Getting error while Create-Student, Please try again..." } });
                }
                else
                {
                    return Ok(new ApiResponse() { Success = true, Result = studentObj });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Create Student API Error : " + ex);
                throw;
            }

        }

        [HttpPost]
        [Route("api/update")]
        public async Task<IActionResult> UpdateStudent(UpdateStudentRequest dto)
        {
            try
            {
                var studentObj = await _service.UpdateStudent(dto);
                if (studentObj == 0)
                {
                    return Ok(new ApiResponse() { Success = false, Errors = new List<string>() { "Getting error while Update-Student, Please try again..." } });
                }
                else
                {
                    return Ok(new ApiResponse() { Success = true, Result = studentObj });
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Update Student API Error Log : " + ex);
                throw;
            }
            
        }

        [HttpPost]
        [Route("api/delete")]
        public async Task<IActionResult> DeleteStudent(IdDTO dto)
        {
            var studentObj = await _service.Delete(dto.Id);
            if (studentObj == 0)
            {
                return Ok(new ApiResponse() { Success = false, Errors = new List<string>() { "Getting error while Update-Student, Please try again..." } });
            }
            else
            {
                return Ok(new ApiResponse() { Success = true, Result = studentObj });
            }
        }
    }
}
