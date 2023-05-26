using AutoMapper;
using DoctorWho.DB.Models;
using DoctorWho.DB.ModelsDto;
using DoctorWho.DB.Repositories;
using DoctorWho.Web.Validation;
using FluentValidation;
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
        [HttpPost("upsert")]
        public ActionResult<Doctor> UpsertDoctor(DoctorsWithoutEpisodes doctor)
        {
            try
            {
                // Validate the DoctorsWithoutEpisodes using FluentValidation rules
                DoctorValidator validator = new DoctorValidator();
                validator.ValidateAndThrow(doctor);

                var upsertedDoctor = new DoctorsWithoutEpisodes
                {
                    DoctorNumber = doctor.DoctorNumber,
                    DoctorName = doctor.DoctorName,
                    BirthDate = doctor.BirthDate,
                    FirstEpisodeDate = doctor.FirstEpisodeDate,
                    LastEpisodeDate = doctor.LastEpisodeDate
                };

                return Ok(upsertedDoctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            try
            {
                // Delete the doctor with the specified id
                var deletedDoctor = _doctorRepository.DeleteDoctor(id);
                if (deletedDoctor == null)
                {
                    return NotFound(); // Doctor not found
                }

                return NoContent(); // Successfully deleted
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the doctor."); // Return a server error response
            }
        }
    }
}
