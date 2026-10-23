using BoardGameNight.Domain.Models;
using BoardGameNight.DomainServices;
using BoardGameNight.DomainServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BoardGameNight.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardgameNightController(IBoardgameNightRepository boardgameNightRepository, IBoardgameNightService boardgameNightService, IUserRepository userRepository, IUserService userService) : ControllerBase
    {
        private readonly IBoardgameNightRepository boardgameNightRepository = boardgameNightRepository;
        private readonly IBoardgameNightService boardgameNightService = boardgameNightService;
        private readonly IUserRepository userRepository = userRepository;
        private readonly IUserService userService = userService;

        [HttpGet]
        public IActionResult Get()
        {

            ICollection<BoardgameNight> boardgameNights = boardgameNightService.getAll();
            if (boardgameNights.IsNullOrEmpty()) return Ok("Geen bordspelavonden gevonden");

            return Ok(boardgameNights);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            BoardgameNight? boardGameNight = boardgameNightService.getById(id);
            if (boardGameNight != null) return Ok(boardGameNight);

            return NotFound("Bordspellen avond niet gevonden");
        }

        [HttpPost]
        public IActionResult Post()
        {
            return StatusCode(StatusCodes.Status501NotImplemented, "Deze functie is nog niet geïmplementeerd");
        }

        [HttpPut("{boardgameNightId}")]
        public IActionResult Put()
        {
            return StatusCode(StatusCodes.Status501NotImplemented, "Deze functie is nog niet geïmplementeerd");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented, "Deze functie is nog niet geïmplementeerd");
        }

        [Authorize]
        [HttpPut("{boardgameNightId}/registrations")]
        public IActionResult PutRegistration(int boardgameNightId)
        {
            var currentUser = HttpContext.User.Identity;
            User? user = userService.getAll().SingleOrDefault(u => u.Email == currentUser!.Name);

            if (user == null) return StatusCode(StatusCodes.Status500InternalServerError, "Neem contact op met de ontwikkelaars");

            try
            {
                boardgameNightService.addParticipant(boardgameNightId, user.Id);
                return Ok("Succesvol aangemeld bij bordspellen avond");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}/registrations")]
        public IActionResult DeleteRegistration(int id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented, "Deze functie is nog niet geïmplementeerd");
        }

        [HttpGet("{id}/registrations")]
        public IActionResult GetRegistrations()
        {
            return StatusCode(StatusCodes.Status501NotImplemented, "Deze functie is nog niet geïmplementeerd");
        }
    }
}
