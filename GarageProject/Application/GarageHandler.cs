using GarageProject.Domain;

namespace GarageProject.Application;

public class GarageHandler
{
    private Garage<Vehicle>? _garage;

    public int Capacity => _garage?.Capacity ?? 0;

    public void CreateGarage(int capacity) =>
        _garage = new Garage<Vehicle>(capacity);

    public bool ParkVehicle(Vehicle vehicle) => _garage?.Add(vehicle) ?? false;

    public Vehicle? RemoveVehicle(string registrationNumber) => _garage?.Remove(registrationNumber);

    public IEnumerable<Vehicle> GetAll() =>
        _garage ?? Enumerable.Empty<Vehicle>();

    // TODO - add get by registration number, type, color, number of wheels, fuel type

    public IEnumerable<IGrouping<string, Vehicle>> GetByType() =>
        GetAll().GroupBy(v => v.GetType().Name);

    // TODO - mock data?
    public void Populate()
    {
        ParkVehicle(new Car("ABC123", "Red", 4, FuelType.Gasoline));
        ParkVehicle(new Car("DEF456", "Green", 4, FuelType.Diesel));
        ParkVehicle(new Car("GHI789", "Blue", 4, FuelType.Electric));
        ParkVehicle(new Motorcycle("XYZ789", "Blue", 2, FuelType: FuelType.Electric, cylinderVolume: 1.6));
        ParkVehicle(new Motorcycle("PQR012", "Red", 2, FuelType.Gasoline, cylinderVolume: 1.8));
        ParkVehicle(new Bus("MNO987", "Silver", 4, 18));
        ParkVehicle(new Bus("STU654", "Bronze", 4, 44));
        ParkVehicle(new Airplane("FGH678", "Chrome", 8, 2 , FuelType.Other));
        ParkVehicle(new Airplane("KLM098", "White", 8, 4, FuelType.Hybrid));
        // ParkVehicle(new Boat("JKL321", "Red", 4, FuelType.Diesel));
    }
}
