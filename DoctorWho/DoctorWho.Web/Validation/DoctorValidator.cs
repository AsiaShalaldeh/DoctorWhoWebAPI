using DoctorWho.DB.Models;
using DoctorWho.DB.ModelsDto;
using FluentValidation;

namespace DoctorWho.Web.Validation
{
    public class DoctorValidator: AbstractValidator<DoctorsWithoutEpisodes>
    {
        public DoctorValidator()
        {
            RuleFor(dto => dto.DoctorNumber).NotEmpty().WithMessage("DoctorNumber is required.");
            RuleFor(dto => dto.DoctorName).NotEmpty().WithMessage("DoctorName is required.");

            RuleFor(dto => dto)
            .Must(BeValidEpisodeDates)
            .WithMessage("LastEpisodeDate should be greater than or equal to FirstEpisodeDate.");

            RuleFor(dto => dto)
            .Must(CheckEpisodeDates)
            .WithMessage("LastEpisodeDate should have no value when FirstEpisodeDate has no value.");
        }
        private bool BeValidEpisodeDates(DoctorsWithoutEpisodes doctor)
        {
            // check if LastEpisodeDate is greater than or equal to FirstEpisodeDate
            return doctor.LastEpisodeDate >= doctor.FirstEpisodeDate;
        }
        private bool CheckEpisodeDates(DoctorsWithoutEpisodes doctor)
        {
            // check if LastEpisodeDate has no value when FirstEpisodeDate has no value
            if (!doctor.FirstEpisodeDate.HasValue && doctor.LastEpisodeDate.HasValue)
            {
                return false;
            }

            return true;
        }
    }
}
