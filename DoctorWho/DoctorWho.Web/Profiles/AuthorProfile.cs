using AutoMapper;
using DoctorWho.DB.Models;
using DoctorWho.DB.ModelsDto;

namespace DoctorWho.Web.Profiles
{
    public class AuthorProfile:Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorDto, Author>();
        }
    }
}
