namespace ElevatorSimulator
{
    internal class Elevator
    {
        public enum ElevatorDirection
        {
            Up,
            Down,
            Stationary
        }

        public int CurrentFloor { get; set; }
        public int TargetFloor { get; set; }
        public List<Person> PeopleInElevator { get; set; }
        public int MaxAmountOfPeople { get; set; }
        public ElevatorDirection Direction { get; set; }
        public List<Person> ElevatorQueue { get; set; }
        //public int speed { get; set; }

        public Elevator()
        {
            CurrentFloor = 1;
            TargetFloor = 1;
            PeopleInElevator = [];
            MaxAmountOfPeople = 4;
            Direction = ElevatorDirection.Stationary;
            ElevatorQueue = [];
        }
    }
}
