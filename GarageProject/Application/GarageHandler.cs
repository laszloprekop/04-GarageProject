using GarageProject.Domain;

namespace GarageProject.Application;

public class GarageHandler
{
    private Garage<Vehicle>? _garage;
    
    public int Capacity => _garage?.Capacity ?? 0;
    
    public void CreateGarage(int capacity) =>
        _garage = new Garage<Vehicle>(capacity);
    
    // TODO - add parking
    // TODO - add retrieving
    // TODO - add get all vehicles
    // TODO - add get by registration number, type, color, number of wheels, fuel type
    
    // TODO - mock data?
    public void Populate()
    {
        throw new NotImplementedException();
    }
}