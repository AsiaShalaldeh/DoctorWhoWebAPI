using AutoMapper;
using DoctorWho.DB.Models;
using DoctorWho.DB.ModelsDto;

namespace DoctorWho.Web.Profiles
{
    public class EnemyProfile : Profile
    {
        public EnemyProfile()
        {
            CreateMap<Enemy, EnemyDto>();
            CreateMap<EnemyDto, Enemy>();
        }
    }
}
