namespace ElevatorSimulator
{
    internal class SimulationController
    {
        private readonly Building building;
        private readonly Elevator elevator;
        private readonly StatTracker statTracker;
        int CurrentTime = 0;
        readonly Random random = new();
        readonly int spawnProbability = 95;
        
        public SimulationController(Building building, Elevator elevator, StatTracker statTracter)
        {
            this.building = building;
            this.elevator = elevator;
            this.statTracker = statTracter;
        }

        public void StartSimulation()
        {
            OneDay();
            Results();
        }

        public void Results()
        {
            Console.WriteLine($"Elevator was idle for {statTracker.secondsElevatorIsIdle / (3600)} hours," +
                              $"{statTracker.secondsElevatorIsIdle % 3600 / 60} minutes and " +
                              $"{statTracker.secondsElevatorIsIdle % 60} seconds.");
        }

        private void OneDay()
        {
            while (CurrentTime < 60 * 60 * 24)
            {
                UpdateSimulation();
                CurrentTime++;
            }
        }

        private void UpdateSimulation()
        {
            SpawnPerson();
            CheckForPeopleLeaving();
            UpdateElevator();
            DisplayStatus();
        }

        private void DisplayStatus()
        {
            Console.WriteLine($"People in building: {building.PeopleInBuilding.Count}\n" +
                              $"People in elevator: {elevator.PeopleInElevator.Count}\n" +
                              $"People waiting for elevator: {elevator.ElevatorQueue.Count}\n" +
                              $"Elevator floor: {elevator.CurrentFloor}");
        }

        private void SpawnPerson()
        {
            if (CurrentTime < building.OpeningTime || CurrentTime + 1 > building.ClosingTime)
            {
                return;
            }
            if(random.Next(100) > spawnProbability)
            {
                Person person = new(CurrentTime, building.Floors, building.ClosingTime);
                building.PeopleInBuilding.Add(person);
                elevator.ElevatorQueue.Add(person);
            }
        }

        private void CheckForPeopleLeaving()
        {
            elevator.ElevatorQueue.AddRange(building.PeopleInBuilding.Where(person => person.TimeToLeave == CurrentTime));
        }

        private void UpdateElevator()
        {
            if (elevator.Direction == Elevator.ElevatorDirection.Stationary
                && elevator.ElevatorQueue.Count == 0)
            {
                statTracker.secondsElevatorIsIdle++;
                return;
            }
            if (elevator.PeopleInElevator.Count > 0)
            {
                LetPeopleOff();
            }
            if (elevator.ElevatorQueue.Count > 0)
            {
                LetPeopleOn();
            }
            if (elevator.Direction != Elevator.ElevatorDirection.Stationary)
            {
                MoveElevator();
            }
        }

        private void LetPeopleOff()
        {
            List<Person> peopleGettingOff = elevator.PeopleInElevator.Where(person => person.TargetFloor == elevator.CurrentFloor).ToList();
            if (peopleGettingOff.Count == 0)
            {
                return;
            }
            peopleGettingOff.ForEach(person => person.TargetFloor = 1);
            peopleGettingOff.ForEach(person => person.CurrentFloor = elevator.CurrentFloor);

            elevator.PeopleInElevator.RemoveAll(person => peopleGettingOff.Contains(person));
            if (elevator.CurrentFloor == 1)
            { 
                building.PeopleInBuilding.RemoveAll(person => peopleGettingOff.Contains(person));
            }
            if (elevator.TargetFloor == elevator.CurrentFloor)
            {
                elevator.Direction = Elevator.ElevatorDirection.Stationary;
            }
        }

        private void LetPeopleOn()
        {
            SetDirection();
            int openSpotsInElevator = elevator.MaxAmountOfPeople - elevator.PeopleInElevator.Count;
            List<Person> peopleToBoard = elevator.ElevatorQueue
                .Where(person => 
                  person.CurrentFloor == elevator.CurrentFloor &&
                ((person.TargetFloor > elevator.CurrentFloor && elevator.Direction == Elevator.ElevatorDirection.Up) ||
                 (person.TargetFloor < elevator.CurrentFloor && elevator.Direction == Elevator.ElevatorDirection.Down)))
                .Take(openSpotsInElevator)
                .ToList();
            elevator.ElevatorQueue.RemoveAll(person => peopleToBoard.Contains(person));
            elevator.PeopleInElevator.AddRange(peopleToBoard);
            if (elevator.PeopleInElevator.Count > 0)
            {
                SetTargetFloor();
            }
        }

        private void SetDirection()
        {
            if (elevator.PeopleInElevator.Count == 0)
            {
                if (elevator.ElevatorQueue.First().CurrentFloor > elevator.CurrentFloor)
                {
                    elevator.Direction = Elevator.ElevatorDirection.Up;
                }
                else if (elevator.ElevatorQueue.First().CurrentFloor < elevator.CurrentFloor)
                {
                    elevator.Direction = Elevator.ElevatorDirection.Down;
                }
                else if (elevator.ElevatorQueue.First().TargetFloor > elevator.CurrentFloor)
                {
                    elevator.Direction = Elevator.ElevatorDirection.Up;
                }
                else if (elevator.ElevatorQueue.First().TargetFloor < elevator.CurrentFloor)
                {
                    elevator.Direction = Elevator.ElevatorDirection.Down;
                }
            }
            else if (elevator.TargetFloor > elevator.CurrentFloor)
            {
                elevator.Direction = Elevator.ElevatorDirection.Up;
            }
            else if (elevator.TargetFloor < elevator.CurrentFloor)
            {
                elevator.Direction = Elevator.ElevatorDirection.Down;
            }
            else if (elevator.TargetFloor == elevator.CurrentFloor)
            {
                elevator.Direction = Elevator.ElevatorDirection.Stationary;
            }
        }

        private void SetTargetFloor()
        {
            if (elevator.Direction == Elevator.ElevatorDirection.Up)
            {
                elevator.TargetFloor = elevator.PeopleInElevator.Select(person => person.TargetFloor).Max();
            }
            else if (elevator.Direction == Elevator.ElevatorDirection.Down)
            {
                elevator.TargetFloor = elevator.PeopleInElevator.Select(person => person.TargetFloor).Min();
            }
        }

        private void MoveElevator()
        {
            if (elevator.Direction == Elevator.ElevatorDirection.Up)
            {
                elevator.CurrentFloor++;
            }
            else if (elevator.Direction == Elevator.ElevatorDirection.Down)
            {
                elevator.CurrentFloor--;
            }
        }
    }
}
