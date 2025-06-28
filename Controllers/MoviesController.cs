using IMDBApi_Assignment3.Models.DTOs.Request;
using IMDBApi_Assignment3.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("api/movies")]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int? year)
        {
            try
            {
                var moviesResponse = year.HasValue
                    ? _movieService.GetAll(year.Value)
                    : _movieService.GetAll();

                return Ok(moviesResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var movieResponse = _movieService.GetById(id);
                return Ok(movieResponse);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] MovieRequest request)
        {
            try
            {
                var createdMovie = _movieService.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = createdMovie.Id }, createdMovie.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] MovieRequest request)
        {
            try
            {
                var updatedMovie = _movieService.Update(id, request);
                return Ok(updatedMovie);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _movieService.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
