// See https://aka.ms/new-console-template for more information
using ElevatorGoingUp;

internal class Program
{
    private static void Main(string[] args)
    {
        //Use default or dynamic number of floors or elevators
        Console.WriteLine("Specify program details such as the number of floors and elevators? (Default is 10 floors, 3 elevators and 12 person weight limit) Y/N");
        int numberOfFloors = 10;
        int numberOfElevators = 3;
        int weightLimit = 12;
        var defauktOrCustomSetup = Console.ReadLine();
        if (!string.IsNullOrEmpty(defauktOrCustomSetup) && defauktOrCustomSetup.ToLower() == "y")
        {
            Console.WriteLine("Enter number of floors");
            numberOfFloors = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter number of elevators");
            numberOfElevators = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the elevator weight limit");
            numberOfElevators = int.Parse(Console.ReadLine());
        }
        

        //Dynamic number of elevators
        var elevators = new List<Elevator>();
        for (int i = 1; i <= numberOfElevators; i++)
        {
            var obj = new Elevator()
            {
                Id = i,
                CurrentFloor = 1,
                Direction = "up",
                IsMoving = false,
                NumberOfPeople = 0,
                ElevatorInstructionsList = new List<ElevatorInstructions>()
            };
            elevators.Add(obj);
        }


        //helpers that do heavy lifting
        var elaHelper = new ElevatorHelper(numberOfFloors, weightLimit);
        var elaTrafficManager = new ElevatorTrafficManagerHelper();



        //Loop to keep the app going
        while (true)
        {

            //Chance for user to enter new instructions
            Console.WriteLine("Type NEW to add a new trip or type exit to close the program");
            var newTripOrContinueOrExit = Console.ReadLine().ToLower();
            if (newTripOrContinueOrExit == "new")
            {
                //Place where request for the elevator are done (loops per floor)
                int floorCallingFrom = 0;
                elaHelper.GetValidPositiveInt($"Enter floor number to call the elevator to (1-{numberOfFloors}): ", floorCallingFrom);

                int floorTripDestination = 0;
                elaHelper.GetValidPositiveInt($"Enter floor number you want to go to (1-{numberOfFloors}): ", floorTripDestination);

                int numberOfPeopleForTrip = 0;
                elaHelper.GetValidPositiveInt($"Enter the number of people waiting on the floor for this trip: ", numberOfPeopleForTrip);

                
                var newTrip = elaHelper.CreateNewTrip(floorCallingFrom, floorTripDestination, numberOfPeopleForTrip);
                elaTrafficManager.AssignTripsToElevator(newTrip, elevators);

            }
            if (newTripOrContinueOrExit == "exit")
            {
                break;
            }


            //Make the ela move if they have a trip or display that it's not moving if it has no trips
            for (int i = 0; i < elevators.Count; i++)
            {
                //Only execute this if an ela has a trip
                if (elevators[i].ElevatorInstructionsList.Any())
                {
                    //Make the ela moe through the floors
                    elaHelper.DirectionDeterminer(elevators[i]);

                    //Change the ela's current floor as it moves through each floor
                    elevators[i].CurrentFloor = elevators[i].Direction == "up" ?
                                                elevators[i].CurrentFloor + 1 :
                                                elevators[i].CurrentFloor - 1;

                    elaHelper.ElevatorLoadPeople(elevators[i]);

                    elaHelper.ElevatorOffloadPeople(elevators[i]);

                    elaHelper.ElevatorStatusOutput(elevators[i]);

                }
                else
                {
                    Console.WriteLine($"Elevator {elevators[i].Id} is stationery at floor {elevators[i].CurrentFloor}");
                }

            }

        }
    }
}
















