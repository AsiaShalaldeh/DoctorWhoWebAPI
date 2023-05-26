using DoctorWho.DB.ModelsDto;
using FluentValidation;

namespace DoctorWho.Web.Validation
{
    public class EpisodeValidator: AbstractValidator<EpisodesDto>
    {
        public EpisodeValidator()
        {
            RuleFor(e => e.AuthorId).NotEmpty().WithMessage("AuthorId is required.");
            RuleFor(e => e.DoctorId).NotEmpty().WithMessage("DoctorId is required.");
            //RuleFor(e => e.SeriesNumber).Length(10).WithMessage("SeriesNumber must be 10 characters long.");
            RuleFor(e => e.EpisodeNumber).GreaterThan(0).WithMessage("EpisodeNumber must be greater than 0.");
        }
    }
}
