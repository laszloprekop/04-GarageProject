using System.Collections;

namespace GarageProject.Domain;

public class Garage<T> : IEnumerable<T> where T : Vehicle
{
    private readonly T?[] _vehicles;
    public int Capacity { get; }
    public int Count => _vehicles.Count(v => v is not null);

    public Garage(int capacity)
    {
        Capacity = capacity;
        _vehicles = new T?[capacity];
    }

    public bool Add(T vehicle)
    {
        if (Count >= Capacity) return false;
        if (_vehicles.Any(v =>
                v?.RegistrationNumber.Equals(vehicle.RegistrationNumber, StringComparison.OrdinalIgnoreCase) == true))
            return false;

        for (var i = 0; i < _vehicles.Length; i++)
        {
            if (_vehicles[i] is null)
            {
                _vehicles[i] = vehicle;
                return true;
            }
        }

        return false;
    }

    public IEnumerator<T> GetEnumerator() =>
        _vehicles.OfType<T>().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}