using ElevatorGoingUp;

public class ElevatorHelper
{
    public void DirectionDeterminer(Elevator elevator)
    {
        try
        {
            //Check if the ela should go up or down
            var elaCurrenDirectionBasedOfTripsItHasLoop = string.Empty;
            //if it's on ground floor it has to go up DUH
            if (elevator.CurrentFloor == 1)
            {
                elaCurrenDirectionBasedOfTripsItHasLoop = "up";
                elevator.Direction = elaCurrenDirectionBasedOfTripsItHasLoop;
            }
            else if (elevator.CurrentFloor == 10)
            {
                elaCurrenDirectionBasedOfTripsItHasLoop = "down";
                elevator.Direction = elaCurrenDirectionBasedOfTripsItHasLoop;
            }
            else if (elevator.CurrentFloor > 1 && elevator.Direction == "up" && elevator.ElevatorInstructionsList.Any(a => a.floorCallingFromNumber > elevator.CurrentFloor))
            {
                elaCurrenDirectionBasedOfTripsItHasLoop = "up";
                elevator.Direction = elaCurrenDirectionBasedOfTripsItHasLoop;
            }
            else if (elevator.CurrentFloor > 1 && elevator.Direction == "down" && elevator.ElevatorInstructionsList.Any(a => a.floorCallingFromNumber < elevator.CurrentFloor))
            {
                elaCurrenDirectionBasedOfTripsItHasLoop = "down";
                elevator.Direction = elaCurrenDirectionBasedOfTripsItHasLoop;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw ex;
        }
    }


    public ElevatorInstructions CreateNewTrip(int floorCallingFrom, 
                                              int floorTripDestination, 
                                              int numberOfPeopleForTrip)
    {
        try
        {
            return  new ElevatorInstructions()
            {
                direction = floorCallingFrom < floorTripDestination ? "up" : "down",
                floorNumber = floorTripDestination,
                floorCallingFromNumber = floorCallingFrom,
                numberOfPeopleInLoad = numberOfPeopleForTrip,
                peopleBoarded = false
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw ex;
        }
    }


    public void AddNewTrip(Elevator elevator)
    {
		try
		{
            //Place where request for the elevator are done (loops per floor)
            Console.WriteLine("Enter floor number to call the elevator to (1-10): ");
            int floorCallingFrom = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter floor number you want to go to (1-10): ");
            int floorTripDestination = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the number of people waiting on the floor for this trip: ");
            int numberOfPeopleForTrip = int.Parse(Console.ReadLine());

            //Create trip instruction for elevator
            var trip = new ElevatorInstructions()
            {
                direction = elevator.CurrentFloor < floorTripDestination ? "up" : "down",
                floorNumber = floorTripDestination,
                floorCallingFromNumber = floorCallingFrom,
                numberOfPeopleInLoad = numberOfPeopleForTrip,
                peopleBoarded = false
            };

            

            //Check if new trip can be boarded on current floor
            if (elevator.CurrentFloor == trip.floorCallingFromNumber)
            {
                elevator.NumberOfPeople = elevator.NumberOfPeople + trip.numberOfPeopleInLoad;
                trip.peopleBoarded = true;
            }

            //Add trip to ela list if it will be within weight limit
            if (CheckAgainstWeightLimitBeforeOnboarding(12, elevator, trip))
            {
                elevator.ElevatorInstructionsList.Add(trip);
            }
            else
            {
                Console.WriteLine($"Trip load will exceed current max capacity");
            }
            
        }
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			throw ex;
		}
    }

    
    public void ElevatorMinMaxFloorReachedDirectionChange(Elevator elevator)
    {
        try
        {
            //If ela is at highest/lowest floor then change direction
            if (elevator.CurrentFloor == 10 || elevator.CurrentFloor == 1)
            {
                elevator.Direction = elevator.CurrentFloor == 10 ? "down" : "up";
            }

            //If elevator has has done it's highest or lowest trip it must change direction
            if (elevator.Direction == "up" && 
                elevator.ElevatorInstructionsList.Any(a => a.direction == "down") &&
                !elevator.ElevatorInstructionsList.Any(a => a.direction == "up"))
            {
                elevator.Direction = "down";
            }
            if (elevator.Direction == "down" &&
                !elevator.ElevatorInstructionsList.Any(a => a.direction == "down") &&
                elevator.ElevatorInstructionsList.Any(a => a.direction == "up"))
            {
                elevator.Direction = "down";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw ex;
        }
    }


    public void ElevatorStatusOutput(Elevator elevator)
    {
        try
        {
            //If ela's current floor matches any instruction floor then offload people
            if (elevator.ElevatorInstructionsList.Any(a => a.floorNumber == elevator.CurrentFloor && a.peopleBoarded))
            {
                //output that people are getting off
                Console.WriteLine($"Ela{elevator.Id} is at floor {elevator.CurrentFloor} and is going offload {elevator.ElevatorInstructionsList.First(a => a.floorNumber == elevator.CurrentFloor).numberOfPeopleInLoad} people.");
                this.ElevatorOffloadPeople(elevator);
                Console.WriteLine($"Ela{elevator.Id} is moving currently at floor {elevator.CurrentFloor} and is going {elevator.Direction} with {elevator.NumberOfPeople} people on.");
            }
            //output that nobody is getting off and that ela is just moving
            else
            {
                Console.WriteLine($"Ela{elevator.Id} is moving currently at floor {elevator.CurrentFloor} and is going {elevator.Direction} with {elevator.NumberOfPeople} people on.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw ex;
        }
    }

    public void ElevatorLoadPeople(Elevator elevator)
    {
        try
        {
            //Update ela's number of people if the current floor matches with the instruction floor called from number
            if (elevator.ElevatorInstructionsList.Any(a => a.floorCallingFromNumber == elevator.CurrentFloor))
            {
                var peopleBeingLoaded = elevator.ElevatorInstructionsList.Where(a => a.floorCallingFromNumber == elevator.CurrentFloor).Sum(b => b.numberOfPeopleInLoad);
                Console.WriteLine($"Ela{elevator.Id} is at floor {elevator.CurrentFloor} and is onloading {peopleBeingLoaded} people.");
                elevator.NumberOfPeople = elevator.NumberOfPeople + peopleBeingLoaded;
                elevator.ElevatorInstructionsList.Where(a => a.floorCallingFromNumber == elevator.CurrentFloor).ToList().ForEach(b => b.peopleBoarded = true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw ex;
        }
    }

    public void ElevatorOffloadPeople(Elevator elevator)
    {
        try
        {
            //Update ela's number of people if the current floor matches with the instruction floor called from number
            if (elevator.ElevatorInstructionsList.Any(a => a.floorNumber == elevator.CurrentFloor && a.peopleBoarded))
            {
                Console.WriteLine($"Ela{elevator.Id} is at floor {elevator.CurrentFloor} and is going offload {elevator.ElevatorInstructionsList.First(a => a.floorNumber == elevator.CurrentFloor).numberOfPeopleInLoad} people.");
                elevator.NumberOfPeople = elevator.NumberOfPeople - elevator.ElevatorInstructionsList.Where(a => a.floorNumber == elevator.CurrentFloor).Sum(b => b.numberOfPeopleInLoad);
                //Delete the trips from the ela
                elevator.ElevatorInstructionsList.RemoveAll(a => a.floorNumber == elevator.CurrentFloor);
                //elevator.ElevatorInstructionsList.First(a => a.floorCallingFromNumber == elevator.CurrentFloor).peopleBoarded = true;
               
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw ex;
        }
    }


    public bool CheckAgainstWeightLimitBeforeOnboarding(int weightLimit, 
                                                        Elevator elevator,
                                                        ElevatorInstructions elevatorInstruction)
    {
        try
        {
            var result = false;

            //Check if the elvator's current load plus the new possible trip is within the weight limit
            if (elevator.NumberOfPeople + elevatorInstruction.numberOfPeopleInLoad <= weightLimit)
            {
                result = true;
            }
            //Check if the elvator's current load plus the new possible trip is within the weight limit if there are
            //people getting off at the current floor where people are coming onboard
            if (elevator.CurrentFloor == elevatorInstruction.floorCallingFromNumber &&
                (elevator.NumberOfPeople - elevator.ElevatorInstructionsList.First(a => a.floorNumber == elevator.CurrentFloor && a.peopleBoarded).numberOfPeopleInLoad) + elevatorInstruction.numberOfPeopleInLoad <= weightLimit)
            {
                result = true;
            }
            //Check if there's a trip ending that will allow the new trip to be onboarded if an existing trip
            //Will be offloaded on the calling floor in the future
            if (elevator.ElevatorInstructionsList.Any(a => a.floorNumber == elevatorInstruction.floorCallingFromNumber) &&
                (elevator.NumberOfPeople - elevator.ElevatorInstructionsList.First(a => a.floorNumber == elevatorInstruction.floorCallingFromNumber).numberOfPeopleInLoad) + elevatorInstruction.numberOfPeopleInLoad <= weightLimit)
            {
                result = true;
            }

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw ex;
        }
    }
//    //private List<Elevator> elevators;
//    private int numberOfFloors;

//    public ElevatorHelper(int numberOfElevators, int numberOfFloors)
//    {
//        //elevators = new List<Elevator>();
//        for (int i = 0; i < numberOfElevators; i++)
//        {
//           // elevators.Add(new Elevator());
//        }

//        this.numberOfFloors = numberOfFloors;
//    }

//    public void CallElevator(int floor, int numberOfPeople)
//    {
//        //Elevator nearestElevator = GetNearestElevator(floor);
//        nearestElevator.IsMoving = true;
//        nearestElevator.Direction = floor > nearestElevator.CurrentFloor ? "up" : "down";
//        nearestElevator.NumberOfPeople = numberOfPeople;

//        // Simulate the elevator moving to the requested floor
//        while (nearestElevator.CurrentFloor != floor)
//        {
//            if (nearestElevator.Direction == "up")
//            {
//                nearestElevator.CurrentFloor++;
//            }
//            else
//            {
//                nearestElevator.CurrentFloor--;
//            }

//            UpdateElevatorStatus(nearestElevator);
//        }

//        nearestElevator.IsMoving = false;
//        nearestElevator.NumberOfPeople = 0;
//    }

//    private void UpdateElevatorStatus(Elevator elevator)
//    {
//        foreach (var person in elevator.Passengers.ToList())
//        {
//            if (person.DestinationFloor == elevator.CurrentFloor)
//            {
//                elevator.Passengers.Remove(person);
//            }
//        }

//        elevator.NumberOfPeople = elevator.Passengers.Count;
//    }

//    private Elevator GetNearestElevator(int floor)
//    {
//        Elevator nearestElevator = elevators[0];
//        int minDistance = Math.Abs(nearestElevator.CurrentFloor - floor);

//        foreach (var elevator in elevators)
//        {
//            int distance = Math.Abs(elevator.CurrentFloor - floor);
//            if (distance < minDistance)
//            {
//                nearestElevator = elevator;
//                minDistance = distance;
//            }
//        }

//        return nearestElevator;
//    }

//    public void PrintElevatorStatus()
//    {
//        for (int i = 0; i < elevators.Count; i++)
//        {
//            Console.WriteLine($"Elevator {i + 1} - Floor: {elevators[i].CurrentFloor}, Moving: {elevators[i].IsMoving}, Direction: {elevators[i].Direction}, People: {elevators[i].NumberOfPeople}");
//        }
//    }
}



//public class Person
//{
//    public int DestinationFloor { get; }

//    public Person(int destinationFloor)
//    {
//        DestinationFloor = destinationFloor;
//    }
//}
