using ElevatorSimulator;

Building building = new();
Elevator elevator = new();

SimulationController controller = new(building, elevator);

controller.StartSimulation();
