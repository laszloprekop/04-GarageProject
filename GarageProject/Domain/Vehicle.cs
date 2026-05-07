namespace GarageProject.Domain;

public abstract class Vehicle
{
    public string RegistrationNumber { get; }
    public string Color { get; set; }
    public int NumberOfWheels { get; set; }

    protected Vehicle(string registrationNumber, string color, int numberOfWheels)
    {
        RegistrationNumber = registrationNumber.ToUpper();
        Color = color;
        NumberOfWheels = numberOfWheels;
    }

    public override string ToString() => $"{GetType().Name} [{RegistrationNumber}] - {Color}, {NumberOfWheels} wheels";
}