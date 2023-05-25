using AutoMapper;
using DoctorWho.DB.Models;
using DoctorWho.DB.ModelsDto;

namespace DoctorWho.Web.Profiles
{
    public class DoctorProfile:Profile
    {
        public DoctorProfile()
        {
            CreateMap<Doctor, DoctorsWithoutEpisodes>();
        }
    }
}
