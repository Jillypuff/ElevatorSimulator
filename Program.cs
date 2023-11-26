using ElevatorSimulator;

Building building = new();
Elevator elevator = new();
StatTracker statTracker = new();

SimulationController controller = new(building, elevator, statTracker);

controller.StartSimulation();
