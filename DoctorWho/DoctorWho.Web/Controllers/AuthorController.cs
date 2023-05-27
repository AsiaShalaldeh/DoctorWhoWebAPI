using AutoMapper;
using DoctorWho.DB.Models;
using DoctorWho.DB.ModelsDto;
using DoctorWho.DB.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.Web.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorController : Controller
    {
        private readonly AuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorController(AuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository ??
                throw new ArgumentNullException(nameof(authorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPut("{authorId}")]
        public IActionResult UpdateAuthor(int authorId, [FromBody] AuthorDto updatedAuthor)
        {
            Author existingAuthor = _authorRepository.GetAuthorById(authorId);
            if (existingAuthor == null)
            {
                return NotFound("Author not found");
            }

            var newAuthor = _mapper.Map<Author>(updatedAuthor);

            _authorRepository.UpdateAuthor(existingAuthor, newAuthor);

            return Ok("Author was Updated Successfully");
        }
    }
}
