using AutoMapper;
using DoctorWho.DB.ModelsDto;
using DoctorWho.DB.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.Web.Controllers
{
    [ApiController]
    [Route("api/episodes")]
    public class EpisodeController : Controller
    {
        private readonly EpisodeRepository _episodeRepository;
        private readonly IMapper _mapper;

        public EpisodeController(EpisodeRepository episodeRepository, IMapper mapper)
        {
            _episodeRepository = episodeRepository ??
                throw new ArgumentNullException(nameof(episodeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet(Name = "GetAllEpisodes")]
        public async Task<IActionResult> GetEpisodes()
        {
            try
            {
                var episodes = await _episodeRepository.GetEpisodesAsync();
                return Ok(_mapper.Map<IEnumerable<EpisodesDto>>(episodes));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
