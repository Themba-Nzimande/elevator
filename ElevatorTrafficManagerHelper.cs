using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorGoingUp
{
    public class ElevatorTrafficManagerHelper
    {

        public void AssignTripsToElevator(ElevatorInstructions elevatorInstructions,
										 List<Elevator> elevators)
        {
			try
			{
                //Assign the elevator closest to the request
                var closestElevator = elevators.First(a => a.CurrentFloor == FindClosestFloorNumber(elevators.Select(b => b.CurrentFloor).ToList(), elevatorInstructions.floorCallingFromNumber));
                //Add the trip to the elevator
                closestElevator.ElevatorInstructionsList.Add(elevatorInstructions);
                
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw ex;
			}
        }


		private int FindClosestFloorNumber(List<int> numbers, int target)
        {
            int closestNumber = numbers[0];
            int minDifference = Math.Abs(numbers[0] - target);

            foreach (int number in numbers)
            {
                int difference = Math.Abs(number - target);

                if (difference < minDifference)
                {
                    minDifference = difference;
                    closestNumber = number;
                }
            }

            return closestNumber;
        }



    }
}
