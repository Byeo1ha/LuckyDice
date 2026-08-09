using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<DiceController>();
        builder.RegisterComponentInHierarchy<TurnController>();

        builder.RegisterComponentInHierarchy<DiceUI>();

        builder.RegisterComponentInHierarchy<PlayerUI>();
        builder.RegisterComponentInHierarchy<PlayerInfo>();
    }
}
