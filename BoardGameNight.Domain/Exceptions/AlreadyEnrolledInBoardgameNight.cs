using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameNight.Domain.Exceptions
{
    [Serializable]
    public class AlreadyEnrolledInBoardgameNight : Exception
    {
        public AlreadyEnrolledInBoardgameNight()
        {
        }

        public AlreadyEnrolledInBoardgameNight(string message) : base(message)
        {
            ValidationMessage = message;
        }

        public string? ValidationMessage { get; set; }
    }
}
