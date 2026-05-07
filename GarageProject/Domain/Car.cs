namespace GarageProject.Domain;

public class Car : Vehicle
{
    public FuelType FuelType { get; set; }

    public Car(string registrationNumber, string color, int numberOfWheels, FuelType fuelType) : base(
        registrationNumber, color, numberOfWheels)
    {
        FuelType = fuelType;
    }

    public override string ToString() =>
        base.ToString() + $", {FuelType}";
}