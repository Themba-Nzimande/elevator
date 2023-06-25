using ElevatorGoingUp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
/// <summary>
/// The engine of the app that does the moving, loading and offloading per elevator
/// </summary>
public class ElevatorHelper
{
    public int numberOfFloors { get; set; }

    private int _weightLimit = 12;

    public int weightLimit
    {
        get { return _weightLimit; }
        set { _weightLimit = value; }
    }

    public ElevatorHelper(int numberOfFloors, int weightLimit)
    {
        this.numberOfFloors = numberOfFloors;
        this.weightLimit = weightLimit;
    }

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
            else if (elevator.CurrentFloor == numberOfFloors)
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
            throw;
        }
    }


    public ElevatorInstructions CreateNewTrip(int floorCallingFrom,
                                              int floorTripDestination,
                                              int numberOfPeopleForTrip)
    {
        try
        {
            return new ElevatorInstructions()
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
            throw;
        }
    }


    public void AddNewTrip(Elevator elevator)
    {
        try
        {
            //Place where request for the elevator are done (loops per floor)
            Console.WriteLine($"Enter floor number to call the elevator to (1-{numberOfFloors}): ");
            int floorCallingFrom = int.Parse(Console.ReadLine());

            Console.WriteLine($"Enter floor number you want to go to (1-{numberOfFloors}): ");
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
            if (CheckAgainstWeightLimitBeforeOnboarding(elevator, trip))
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
            throw;
        }
    }

    //not needed so delete
    public void ElevatorMinMaxFloorReachedDirectionChange(Elevator elevator)
    {
        try
        {
            //If ela is at highest/lowest floor then change direction
            if (elevator.CurrentFloor == numberOfFloors || elevator.CurrentFloor == 1)
            {
                elevator.Direction = elevator.CurrentFloor == numberOfFloors ? "down" : "up";
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
            throw;
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
            throw;
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
            throw;
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
            throw;
        }
    }


    public bool CheckAgainstWeightLimitBeforeOnboarding(Elevator elevator,
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
            throw;
        }
    }


   

    public void GetValidPositiveInt(string promptText, int intVariable)
    {
        try
        {
            if (int.TryParse(Console.ReadLine(), out int number) && number > 0)
            {
                Console.WriteLine("Valid positive integer input.");
                intVariable = number;
            }
            else
            {
                Console.WriteLine("Invalid input or not a positive integer. Please try again");
                GetValidPositiveInt(promptText, intVariable);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}

