using R3;

public abstract class MergeableEntity : Entity
{
    public readonly ReactiveProperty<int> Level;

    public MergeableEntity(MergeableEntityData data) : base(data)
    {
        Level = new ReactiveProperty<int>(data.Level);
        Level.Subscribe(newLevel => data.Level = newLevel);
    }
}