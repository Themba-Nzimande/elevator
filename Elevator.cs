using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorGoingUp
{
    public class Elevator
    {
        public int Id { get; set; }
        public int CurrentFloor { get; set; }
        public bool IsMoving { get; set; }
        public string Direction { get; set; }
        public int NumberOfPeople { get; set; }

        public List<ElevatorInstructions> ElevatorInstructionsList { get; set; }

        



    }
}
