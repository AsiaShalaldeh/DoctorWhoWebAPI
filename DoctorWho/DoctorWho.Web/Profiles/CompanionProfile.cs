using AutoMapper;
using DoctorWho.DB.Models;
using DoctorWho.DB.ModelsDto;

namespace DoctorWho.Web.Profiles
{
    public class CompanionProfile:Profile
    {
        public CompanionProfile()
        {
            CreateMap<CompanionDto, Companion>();
        }
    }
}
