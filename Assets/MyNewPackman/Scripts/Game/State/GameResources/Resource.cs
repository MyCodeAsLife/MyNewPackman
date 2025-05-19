using R3;

public class Resource
{
    public readonly ResourceData Origin;
    public readonly ReactiveProperty<int> Amount;

    public Resource(ResourceData data)
    {
        Origin = data;
        Amount = new ReactiveProperty<int>(data.Amount);

        Amount.Subscribe(value => data.Amount = value);
    }

    public ResourceType ResourceType => Origin.ResourceType;
}