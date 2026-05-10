namespace GarageProject.Domain;

public class Motorcycle : Vehicle
{
    public double CylinderVolume { get; set; }

    public Motorcycle(string registrationNumber, string color, int numberOfWheels, FuelType FuelType,
        double cylinderVolume) : base(
        registrationNumber, color, numberOfWheels)
    {
        this.CylinderVolume = cylinderVolume;
    }
}
