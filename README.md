# ElevatorSimulator

Training project to simulate one or more elevators in a building where people spawn in to work  
Currently working base build with no real features

## Table of Contents

- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)

## Installation

1. **Clone the repository:**
```bash
git clone https://github.com/Jillypuff/ElevatorSimulator.git
```

## Usage
- Just build and run solution
- To change frequency of people spawning change the spawnProbability variable in SimulationController.cs
- To change opening and closing time and/or numbers of floors in buildings change the corresponding variable in Building.cs
- To change elevator capacity change MaximumAmountOfPeople in Elevator.cs


## Contributing

Check Todo list:

Todo list:
- Add more realistic timing. Currently everything happends instantly each second
- Add tracker for how long elevators stay inactive during the day
- Add tracker to compare efficency when adding more elevators to a building or increase speed of each elevator
- Add rushhours when more people spawns in group
- Add features to people:
	- Energy (chance to take stairs if available)
	- Shyness (don't enter elevator if x amount of people already inside)
	- Weight (instead of maximum people have maximum weight on elevators, or both)