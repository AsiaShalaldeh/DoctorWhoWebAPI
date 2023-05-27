using AutoMapper;
using DoctorWho.DB.Models;
using DoctorWho.DB.ModelsDto;
using DoctorWho.DB.Repositories;
using DoctorWho.Web.Validation;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.Web.Controllers
{
    [ApiController]
    [Route("api/episodes")]
    public class EpisodeController : Controller
    {
        private readonly EpisodeRepository _episodeRepository;
        private readonly EnemyRepository _enemyRepository;
        private readonly CompanionRepository _companionRepository;
        private readonly IMapper _mapper;

        public EpisodeController(EpisodeRepository episodeRepository, IMapper mapper,
            EnemyRepository enemyRepository, CompanionRepository companionRepository)
        {
            _episodeRepository = episodeRepository ??
                throw new ArgumentNullException(nameof(episodeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _enemyRepository = enemyRepository ??
                throw new ArgumentNullException(nameof(enemyRepository));
            _companionRepository = companionRepository ??
                throw new ArgumentNullException(nameof(companionRepository));
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

        [HttpPost]
        public IActionResult CreateEpisode([FromBody] EpisodesDto episodeDto)
        {
            try
            {
                // Validate the episode entity
                var validator = new EpisodeValidator();
                var validationResult = validator.Validate(episodeDto);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                var episode = _mapper.Map<Episode>(episodeDto);

                // Create the episode and get the new entity ID
                var newEpisodeId = _episodeRepository.CreateEpisode(episode);

                return Ok(newEpisodeId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("{episodeId}/enemies")]
        public IActionResult AddEnemyToEpisode(int episodeId, [FromBody] EnemyDto enemyDto)
        {
            try
            {
                var episode = _episodeRepository.GetEpisodeById(episodeId);
                if (episode == null)
                {
                    return NotFound("Episode not found"); 
                }

                Enemy enemy = _enemyRepository.GetEnemyById(enemyDto.EnemyId);
                if (enemy == null)
                {
                    enemy = _mapper.Map<Enemy>(enemyDto);
                    enemy = _enemyRepository.CreateEnemy(enemy);
                }
                _enemyRepository.AddEnemyToEpisode(episode, enemy);

                return Ok("Enemy was Added Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.InnerException}");
            }
        }

        [HttpPost("{episodeId}/companions")]
        public IActionResult AddCompanionToEpisode(int episodeId, [FromBody] CompanionDto companionDto)
        {
            try
            {
                var episode = _episodeRepository.GetEpisodeById(episodeId);
                if (episode == null)
                {
                    return NotFound("Episode not found");
                }

                Companion companion = _companionRepository.GetCompanionById(companionDto.CompanionId);
                if (companion == null)
                {
                    companion = _mapper.Map<Companion>(companionDto);
                    companion = _companionRepository.CreateCompanion(companion);
                }
                _companionRepository.AddCompanionToEpisode(episode, companion);

                return Ok("Companion was Added Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.InnerException}");
            }
        }

    }
}
