namespace GarageProject.Domain;

public class Motorcycle : Vehicle
{
    public FuelType FuelType { get; set; }
    public double CylinderVolume { get; set; }

    public Motorcycle(string registrationNumber, string color, int numberOfWheels, FuelType fuelType,
        double cylinderVolume) : base(
        registrationNumber, color, numberOfWheels)
    {
        FuelType = fuelType;
        CylinderVolume = cylinderVolume;
    }
}
