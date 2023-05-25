using AutoMapper;
using DoctorWho.DB.ModelsDto;
using DoctorWho.DB.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.Web.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorController : Controller
    {
        private readonly DoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public DoctorController(DoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository ??
                throw new ArgumentNullException(nameof(doctorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet(Name = "GetAllDoctors")]
        public async Task<IActionResult> GetDoctors()
        {
            try
            {
                var doctors = await _doctorRepository.GetDoctorsAsync();
                return Ok(_mapper.Map<IEnumerable<DoctorsWithoutEpisodes>>(doctors));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
