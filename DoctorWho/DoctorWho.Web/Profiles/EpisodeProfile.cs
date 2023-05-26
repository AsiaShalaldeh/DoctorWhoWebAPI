using AutoMapper;
using DoctorWho.DB.Models;
using DoctorWho.DB.ModelsDto;

namespace DoctorWho.Web.Profiles
{
    public class EpisodeProfile : Profile
    {
        public EpisodeProfile()
        {
            CreateMap<Episode, EpisodesDto>();
            CreateMap<EpisodesDto, Episode>(); 
        }
    }
}
