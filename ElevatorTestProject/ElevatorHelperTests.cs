using ElevatorGoingUp;

namespace ElevatorTestProject
{
    [TestClass]
    public class ElevatorHelperTests
    {
        ElevatorHelper elevatorHelper = new ElevatorHelper(10, 12);

        #region DirectionDeterminerTest
       
        #endregion


        #region ElevatorMinMaxFloorReachedDirectionChangeTest
        [TestMethod]
        public void DirectionDeterminerTest_MaxFloorReachedChangeDirectionUpToDown_Down()
        {
            var loadedElevator = new Elevator()
            {
                Id = 1,
                CurrentFloor = 10,
                Direction = "up",
                ElevatorInstructionsList = new List<ElevatorInstructions>()
                {
                    new ElevatorInstructions()
                    {
                        floorCallingFromNumber = 7,
                        direction = "up",
                        floorNumber = 10,
                        numberOfPeopleInLoad = 7,
                        peopleBoarded = true
                    }
                },
                IsMoving = true,
                NumberOfPeople = 5
            };


            elevatorHelper.DirectionDeterminer(loadedElevator);

            Assert.AreEqual("down", loadedElevator.Direction);
        }

        [TestMethod]
        public void DirectionDeterminerTest_MinFloorReachedChangeDirectionDownToUp_Up()
        {
            var loadedElevator = new Elevator()
            {
                Id = 1,
                CurrentFloor = 1,
                Direction = "down",
                ElevatorInstructionsList = new List<ElevatorInstructions>()
                {
                    new ElevatorInstructions()
                    {
                        floorCallingFromNumber = 7,
                        direction = "up",
                        floorNumber = 10,
                        numberOfPeopleInLoad = 7,
                        peopleBoarded = true
                    }
                },
                IsMoving = true,
                NumberOfPeople = 5
            };


            elevatorHelper.DirectionDeterminer(loadedElevator);

            Assert.AreEqual("up", loadedElevator.Direction);
        }

        [TestMethod]
        public void DirectionDeterminerTest_NewTripGoingDownForElevatorGoingUpChangeDirectionUpToDown_Up()
        {
            var loadedElevator = new Elevator()
            {
                Id = 1,
                CurrentFloor = 8,
                Direction = "up",
                ElevatorInstructionsList = new List<ElevatorInstructions>()
                {
                    new ElevatorInstructions()
                    {
                        floorCallingFromNumber = 7,
                        direction = "down",
                        floorNumber = 3,
                        numberOfPeopleInLoad = 7,
                        peopleBoarded = true
                    }
                },
                IsMoving = true,
                NumberOfPeople = 5
            };


            elevatorHelper.DirectionDeterminer(loadedElevator);

            Assert.AreEqual("up", loadedElevator.Direction);
        }


        [TestMethod]
        public void DirectionDeterminerTest_NewTripGoingUpForElevatorGoingDownChangeDirectionDownToUp_Down()
        {
            var loadedElevator = new Elevator()
            {
                Id = 1,
                CurrentFloor = 8,
                Direction = "down",
                ElevatorInstructionsList = new List<ElevatorInstructions>()
                {
                    new ElevatorInstructions()
                    {
                        floorCallingFromNumber = 7,
                        direction = "up",
                        floorNumber = 9,
                        numberOfPeopleInLoad = 7,
                        peopleBoarded = true
                    }
                },
                IsMoving = true,
                NumberOfPeople = 5
            };


            elevatorHelper.DirectionDeterminer(loadedElevator);

            Assert.AreEqual("down", loadedElevator.Direction);
        }
        #endregion

        #region ElevatorLoadPeopleTest
        [TestMethod]
        public void ElevatorLoadPeopleTest_LoadSevenOntoLoadOfFive_Twelve()
        {
            var loadedElevator = new Elevator()
            {
                Id = 1,
                CurrentFloor = 7,
                Direction = "up",
                ElevatorInstructionsList = new List<ElevatorInstructions>()
                {
                    new ElevatorInstructions()
                    {
                        floorCallingFromNumber = 7,
                        direction = "up",
                        floorNumber = 10,
                        numberOfPeopleInLoad = 7,
                        peopleBoarded = true
                    }
                },
                IsMoving = true,
                NumberOfPeople = 5
            };


            elevatorHelper.ElevatorLoadPeople(loadedElevator);

            Assert.AreEqual(12, loadedElevator.NumberOfPeople);
        }
        #endregion

        #region ElevatorOffloadPeopleTest
        [TestMethod]
        public void ElevatorOffloadPeopleTest_OffloadFiveFromLoadOfTen_Five()
        {
            var loadedElevator = new Elevator()
            {
                Id = 1,
                CurrentFloor = 7,
                Direction = "up",
                ElevatorInstructionsList = new List<ElevatorInstructions>() 
                {
                    new ElevatorInstructions()
                    {
                        floorCallingFromNumber = 3,
                        direction = "up",
                        floorNumber = 7,
                        numberOfPeopleInLoad = 5,
                        peopleBoarded = true
                    }
                },
                IsMoving = true,
                NumberOfPeople = 10
            };


            elevatorHelper.ElevatorOffloadPeople(loadedElevator);

            Assert.AreEqual(5, loadedElevator.NumberOfPeople);
        }
        #endregion

        #region WeightLimitCheckTest
        [TestMethod]
        public void CheckAgainstWeightLimitBeforeOnboarding_AboveMaxLimit_False()
        {
            var loadedElevator = new Elevator()
            {
                Id = 1,
                CurrentFloor = 1,
                Direction = "up",
                ElevatorInstructionsList = new List<ElevatorInstructions>(),
                IsMoving = true,
                NumberOfPeople = 8
            };

            var newLoadForElevator = new ElevatorInstructions()
            {
                floorCallingFromNumber = 3,
                direction = "up",
                floorNumber = 7,
                numberOfPeopleInLoad = 6,
                peopleBoarded = false
            };


            var isNEwLoadWithinWeightLimitElevator = elevatorHelper.CheckAgainstWeightLimitBeforeOnboarding(loadedElevator, newLoadForElevator);

            Assert.IsFalse(isNEwLoadWithinWeightLimitElevator);
        }

        [TestMethod]
        public void CheckAgainstWeightLimitBeforeOnboarding_WithinMaxLimit_True()
        {
            var loadedElevator = new Elevator()
            {
                Id = 1,
                CurrentFloor = 1,
                Direction = "up",
                ElevatorInstructionsList = new List<ElevatorInstructions>(),
                IsMoving = true,
                NumberOfPeople = 6
            };

            var newLoadForElevator = new ElevatorInstructions()
            {
                floorCallingFromNumber = 3,
                direction = "up",
                floorNumber = 7,
                numberOfPeopleInLoad = 6,
                peopleBoarded = false
            };


            var isNEwLoadWithinWeightLimitElevator = elevatorHelper.CheckAgainstWeightLimitBeforeOnboarding(loadedElevator, newLoadForElevator);

            Assert.IsTrue(isNEwLoadWithinWeightLimitElevator);
        }
        #endregion



    }
}