﻿namespace ElevatorSimulator
{
    internal class Person
    {
        public int CurrentFloor { get; set; }
        public int TargetFloor { get; set; }
        public int TimeToLeave {  get; set; }

        public Person(int currentTime, int floors)
        {
            CurrentFloor = 1;
            Random random = new();
            int hour = 60 * 60;
            int shortesTimeStaying = Math.Min(currentTime + hour, hour * 23);
            TimeToLeave = random.Next(shortesTimeStaying, hour * 23);
            TargetFloor = random.Next(2, floors + 1);
        }
    }
}
