using IMDBApi_Assignment3.Models.DTOs.Request;
using IMDBApi_Assignment3.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("api/movies/{movieId}/reviews")]
    [Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpGet]
        public IActionResult GetAllByMovieId([FromRoute] int movieId)
        {
            try
            {
                var reviews = _reviewService.GetByMovieId(movieId);
                return Ok(reviews);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id, [FromRoute] int movieId)
        {
            try
            {
                var review = _reviewService.GetById(id, movieId);
                return Ok(review);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Create([FromRoute] int movieId, [FromBody] ReviewRequest request)
        {
            try
            {
                var createdReview = _reviewService.Create(movieId, request);
                return CreatedAtAction(nameof(GetById), new { movieId, id = createdReview.Id }, createdReview.Message);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int movieId, [FromRoute] int id, [FromBody] ReviewRequest request)
        {
            try
            {
                var updatedReview = _reviewService.Update(movieId, id, request);
                return Ok(updatedReview);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int movieId, [FromRoute] int id)
        {
            try
            {
                _reviewService.Delete(movieId, id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete]
        public IActionResult DeleteAllByMovieId([FromRoute] int movieId)
        {
            try
            {
                _reviewService.DeleteAllByMovieId(movieId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
