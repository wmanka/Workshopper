namespace Workshopper.Domain.Sessions;

public class DeliveryType(string name, int value) : SmartEnum<DeliveryType>(name, value)
{
    public readonly static DeliveryType Online = new(nameof(Online), 0);
    public readonly static DeliveryType Stationary = new(nameof(Stationary), 1);
    public readonly static DeliveryType Hybrid = new(nameof(Hybrid), 2);
}