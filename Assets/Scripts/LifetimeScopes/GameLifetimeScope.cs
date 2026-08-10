using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private PlayerInfo player1;
    [SerializeField] private PlayerInfo player2;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<DiceController>();
        builder.RegisterComponentInHierarchy<TurnController>();

        builder.RegisterComponentInHierarchy<DiceUI>();

        builder.RegisterComponentInHierarchy<PlayerUI>();
        builder.RegisterComponentInHierarchy<SafetyBorder>();

        builder.RegisterComponent(player1).Keyed(PlayerKey.Player1);
        builder.RegisterComponent(player2).Keyed(PlayerKey.Player2);
    }
}
