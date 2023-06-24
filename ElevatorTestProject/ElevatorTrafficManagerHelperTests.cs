using ElevatorGoingUp;

namespace ElevatorTestProject
{
    [TestClass]
    public class ElevatorTrafficManagerHelperTests
    {
        ElevatorTrafficManagerHelper elevatorTrafficManagerHelper = new ElevatorTrafficManagerHelper();



        #region ElevatorTrafficManagerHelperTest
        [TestMethod]
        public void AssignTripsToElevatorTest_AssignNewTripToFirstElevatorOutOfThreeElevators_ID1()
        {
            var elevators = new List<Elevator>();
            for (int i = 1; i <= 3; i++)
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

            var newTrip = new ElevatorInstructions()
            {
                floorCallingFromNumber = 7,
                direction = "up",
                floorNumber = 10,
                numberOfPeopleInLoad = 7,
                peopleBoarded = true
            };


            elevatorTrafficManagerHelper.AssignTripsToElevator(newTrip, elevators);

            Assert.AreEqual(1, elevators[0].ElevatorInstructionsList.Count);
        }


        [TestMethod]
        public void AssignTripsToElevatorTest_AssignNewTripToThirdElevatorOutOfThreeElevators_ID3()
        {
            var elevators = new List<Elevator>();
            for (int i = 1; i <= 3; i++)
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

            elevators[0].CurrentFloor = 7;
            elevators[1].CurrentFloor = 5;


            var newTrip = new ElevatorInstructions()
            {
                floorCallingFromNumber = 2,
                direction = "up",
                floorNumber = 10,
                numberOfPeopleInLoad = 7,
                peopleBoarded = true
            };


            elevatorTrafficManagerHelper.AssignTripsToElevator(newTrip, elevators);

            Assert.AreEqual(1, elevators[2].ElevatorInstructionsList.Count);
        }
        #endregion


        #region FindClosestFloorNumberTest
        [TestMethod]
        public void FindClosestFloorNumber_ElevatorsOnFloors3And8And10WithTripFromFloor2_3()
        {
            var elevators = new List<Elevator>();
            for (int i = 1; i <= 3; i++)
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

            elevators[0].CurrentFloor = 7;
            elevators[1].CurrentFloor = 5;


            var closestElevatorCurrentFloor = elevatorTrafficManagerHelper.FindClosestFloorNumber(new List<int>() {3, 8, 10 }, 2);

            Assert.AreEqual(3, closestElevatorCurrentFloor);
        }
        #endregion


    }
}