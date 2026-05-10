namespace GarageProject.Domain;

public class Airplane : Vehicle
{
    public int NumberOfEngines { get; set; }

    public Airplane(string registrationNumber, string color, int numberOfWheels, int numberOfEngines, FuelType other) : base(
        registrationNumber, color, numberOfWheels) => NumberOfEngines = numberOfEngines;
}
