using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGameNight.Domain.Exceptions
{
    [Serializable]
    public class OrganizerEnrolledInOwnBoardgameNight : Exception
    {
        public OrganizerEnrolledInOwnBoardgameNight()
        {
        }

        public OrganizerEnrolledInOwnBoardgameNight(string message) : base(message)
        {
            ValidationMessage = message;
        }

        public string? ValidationMessage { get; set; }
    }
}
