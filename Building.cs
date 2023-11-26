namespace ElevatorSimulator
{
    internal class Building
    {
        public int Floors { get; set; }
        public bool HasStairs { get; set; }
        public int AmountOfElevators { get; set; }
        public int OpeningTime { get; set; }
        public int ClosingTime { get; set; }
        public List<Person> PeopleInBuilding { get; set; }

        public Building(int floors = 4, bool hasStairs = false, int amountOfElevators = 1, int openingTime = 60 * 60 * 8, int closingTime = 60 * 60 * 23)
        {
            Floors = floors;
            HasStairs = hasStairs;
            AmountOfElevators = amountOfElevators;
            OpeningTime = openingTime;
            ClosingTime = closingTime;
            PeopleInBuilding = [];
        }
    }
}
