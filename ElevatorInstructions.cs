using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorGoingUp
{
    public class ElevatorInstructions
    {
        public int floorNumber { get; set; }

        public int floorCallingFromNumber { get; set; }

        public int numberOfPeopleInLoad { get; set; }

        public string direction { get; set; }

        public bool peopleBoarded { get; set; }
    }
}
