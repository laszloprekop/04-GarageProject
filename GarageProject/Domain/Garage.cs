namespace GarageProject.Domain;

public class Garage
{
    private readonly T?[] _vehicles;
    public int Capacity { get; }
    public int Count => _vehicles.Count(v => v is not null);
    
    public Garage(int capacity)
    {
        Capacity = capacity;
        _vehicles = new T?[capacity];
    }
}