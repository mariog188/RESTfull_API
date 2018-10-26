using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.API.Models;
using Library.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.API.Helpers;

namespace Library.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Authors")]
    public class AuthorsController : Controller
    {
        private readonly ILibraryRepository _libraryRepository;

        public AuthorsController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpGet()]
        public IActionResult GetAuthors()
        {
            var authorsFromRepo = _libraryRepository.GetAuthors();
            var authors = new List<AuthorDTO>();

            foreach (var item in authorsFromRepo)
            {
                authors.Add(new AuthorDTO
                {
                    Id = item.Id,
                    Name = $"{item.FirstName} {item.LastName}",
                    Genre = item.Genre,
                    Age = item.DateOfBirth.GetCurrentAge()
                });
            }
            return new JsonResult(authors);
        }
    }
}