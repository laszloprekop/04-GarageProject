using System.Text.Json.Serialization;

namespace GarageProject.Domain;

/* I have to admit, this polymorphic sorcery here is pasted and customized code - I just really wanted the data persistence to work as a stretch goal.
    I will definitely have to come back and fully understand what's going on here'.
 */

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(Car), "car")]
[JsonDerivedType(typeof(Motorcycle), "motorcycle")]
[JsonDerivedType(typeof(Bus), "bus")]
[JsonDerivedType(typeof(Airplane), "airplane")]
[JsonDerivedType(typeof(Boat), "boat")]

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
