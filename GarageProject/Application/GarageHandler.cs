using GarageProject.Domain;

namespace GarageProject.Application;

public class GarageHandler
{
    private Garage<Vehicle>? _garage;

    public int Capacity => _garage?.Capacity ?? 0;

    public void CreateGarage(int capacity) =>
        _garage = new Garage<Vehicle>(capacity);

    public bool ParkVehicle(Vehicle vehicle) => _garage?.Add(vehicle) ?? false;

    // TODO - add removing

    public IEnumerable<Vehicle> GetAll() =>
        _garage ?? Enumerable.Empty<Vehicle>();

    // TODO - add get by registration number, type, color, number of wheels, fuel type

    // TODO - mock data?
    public void Populate()
    {
        ParkVehicle(new Car("ABC123", "Red", 4, FuelType.Gasoline));
        ParkVehicle(new Car("DEF456", "Green", 4, FuelType.Diesel));
        ParkVehicle(new Car("GHI789", "Blue", 4, FuelType.Electric));
        // ParkVehicle(new Motorcycle("XYZ789", "Blue", 2, FuelType.Diesel));
        // ParkVehicle(new Motorcycle("PQR012", "Red", 2, FuelType.Gasoline));
        // ParkVehicle(new Boat("JKL321", "Red", 4, FuelType.Diesel));
    }
}
