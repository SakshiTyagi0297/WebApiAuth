using AuthenticationPlugin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiAuth.Data;
using WebApiAuth.Models;
using WebApiAuth.Services;

namespace WebApiAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    
    public class RecordController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRecordService _authServices;
        private readonly ILogger<RecordController> _logger;

        public RecordController(AppDbContext context, IRecordService authService, ILogger<RecordController> logger)
        {
            _context = context;
            _authServices = authService;
            _logger=logger;
        }

        [HttpPost("CreateRegister")]
        [AllowAnonymous]
        public async Task<IActionResult> Post(TblUser user)
        {
            _logger.LogInformation("Post Api Run");
            var data = await _authServices.CreateRegister(user);
            if (data)
            {
                _logger.LogInformation("Post Api Run Succesfully");
                return Ok("Record Save successfully");
            }
            _logger.LogInformation("Post Api Generate Bad Request");
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var data= await _authServices.GetAll();
            return Ok(data);
        }
        [HttpDelete("DeleteById")]
        [Authorize]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _authServices.DeleteById(id);
            return NoContent();
        }
            
    }
}
