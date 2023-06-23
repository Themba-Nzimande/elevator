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
        if (Console.ReadLine().ToLower() == "y")
        {
            Console.WriteLine("Enter number of floors");
            numberOfFloors = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter number of elevators");
            numberOfElevators = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the elevator weight limit");
            numberOfElevators = int.Parse(Console.ReadLine());
        }

        //Dynamic elevators
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


        //Need this for umm stuff
        var elaHelper = new ElevatorHelper(numberOfFloors, weightLimit);
        var elaTrafficManager = new ElevatorTrafficManagerHelper();




        while (true)
        {

            //Chance for user to enter new instructions
            Console.WriteLine("Type NEW to add a new trip");
            var newTripOrContinueOrExit = Console.ReadLine();
            if (newTripOrContinueOrExit == "NEW")
            {
                //Place where request for the elevator are done (loops per floor)
                Console.WriteLine("Enter floor number to call the elevator to (1-10): ");
                int floorCallingFrom = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter floor number you want to go to (1-10): ");
                int floorTripDestination = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the number of people waiting on the floor for this trip: ");
                int numberOfPeopleForTrip = int.Parse(Console.ReadLine());

                var newTrip = elaHelper.CreateNewTrip(floorCallingFrom, floorTripDestination, numberOfPeopleForTrip);
                elaTrafficManager.AssignTripsToElevator(newTrip, elevators);

            }
            if (newTripOrContinueOrExit == "EXIT")
            {
                break;
            }


            //Make the ela move if they have a trip
            for (int i = 0; i < elevators.Count; i++)
            {
                //Only execute this if an ela has a trip
                if (elevators[i].ElevatorInstructionsList.Any())
                {
                    //Make the ela moe through the floors
                    elaHelper.DirectionDeterminer(elevators[i]);
                    //Change the ela's current floor as it moves through each floor
                    if (elevators[i].Direction == "up")
                    {
                        elevators[i].CurrentFloor = elevators[i].CurrentFloor + 1;
                    }
                    else
                    {
                        elevators[i].CurrentFloor = elevators[i].CurrentFloor - 1;
                    }
                    //elevators[i].CurrentFloor = elevators[i].Direction == "up" ? (elevators[i].CurrentFloor + 1) : (elevators[i].CurrentFloor - 1);

                    elaHelper.ElevatorLoadPeople(elevators[i]);

                    elaHelper.ElevatorOffloadPeople(elevators[i]);

                    elaHelper.ElevatorStatusOutput(elevators[i]);

                    //elaHelper.ElevatorMinMaxFloorReachedDirectionChange(elevators[i]);
                }
                else
                {
                    Console.WriteLine($"Ela{elevators[i].Id} is stationery at floor {elevators[i].CurrentFloor}");
                }

            }

            ////Make the ela moe through the floors
            ////Check if the ela should go up or down
            //var elaCurrenDirectionBasedOfTripsItHas = string.Empty;
            //    var tt = ela1.ElevatorInstructionsList;
            //    //if it's on ground floor it has to go up DUH
            //    if (ela1.CurrentFloor == 1)
            //    {
            //        elaCurrenDirectionBasedOfTripsItHas = "up";
            //        ela1.Direction = elaCurrenDirectionBasedOfTripsItHas;
            //    }
            //    else if (ela1.CurrentFloor > 1 && ela1.Direction == "up" && ela1.ElevatorInstructionsList.Any(a => a.floorNumber < ela1.CurrentFloor))
            //    {
            //        elaCurrenDirectionBasedOfTripsItHas = "up";
            //        ela1.Direction = elaCurrenDirectionBasedOfTripsItHas;
            //    }
            //    else if (ela1.CurrentFloor > 1 && ela1.Direction == "down" && ela1.ElevatorInstructionsList.Any(a => a.floorNumber > ela1.CurrentFloor))
            //    {
            //        elaCurrenDirectionBasedOfTripsItHas = "down";
            //        ela1.Direction = elaCurrenDirectionBasedOfTripsItHas;
            //    }
            //    //Change the ela's current floor as it moves through each floor
            //    ela1.CurrentFloor = ela1.Direction == "up" ? (ela1.CurrentFloor + 1) : (ela1.CurrentFloor - 1);

            //    //Update ela's number of people if the current floor matches with the instruction floor called from number
            //    if (ela1.ElevatorInstructionsList.Any(a => a.floorCallingFromNumber == ela1.CurrentFloor))
            //    {
            //        ela1.NumberOfPeople = ela1.NumberOfPeople + ela1.ElevatorInstructionsList.First(a => a.floorCallingFromNumber == ela1.CurrentFloor).numberOfPeopleInLoad;
            //        ela1.ElevatorInstructionsList.First(a => a.floorCallingFromNumber == ela1.CurrentFloor).peopleBoarded = true;
            //    }

            //    elaHelper.ElevatorStatusOutput(ela1);

            //    //Chance for user to enter new instructions
            //    Console.WriteLine("Type NEW to add a new trip");
            //    var newTripOrContinueOrExit = Console.ReadLine();
            //    if (newTripOrContinueOrExit == "NEW")
            //    {
            //        elaHelper.AddNewTrip(ela1);
            //    }
            //    if (newTripOrContinueOrExit == "EXIT")
            //    {
            //        break;
            //    }

            //    elaHelper.ElevatorMinMaxFloorReachedDirectionChange(ela1);





        }
    }
}












// Create an elevator controller with 3 elevators and 10 floors
//ElevatorHelper elevatorController = new ElevatorHelper(1, 10);

//while (true)
//{
//    Console.WriteLine("Enter floor number to call the elevator (1-10): ");
//    int floor = int.Parse(Console.ReadLine());

//    Console.WriteLine("Enter the number of people waiting on the floor: ");
//    int numberOfPeople = int.Parse(Console.ReadLine());

//    elevatorController.CallElevator(floor, numberOfPeople);
//    elevatorController.PrintElevatorStatus();

//    Console.WriteLine("Do you want to continue? (y/n)");
//    string input = Console.ReadLine();

//    if (input.ToLower() == "n")
//        break;
//}




