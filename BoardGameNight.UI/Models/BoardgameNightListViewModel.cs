using BoardGameNight.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BoardGameNight.UI.Models
{
    public class BoardgameNightListViewModel
    {
        public required IEnumerable<BoardgameNightViewModel> BoardgameNights { get; set; }
    }
}
