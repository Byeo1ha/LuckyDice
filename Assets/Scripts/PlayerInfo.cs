using R3;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public ReactiveProperty<int> PlayerHp = new(100);

    public void PlayerDamaged(int damage)
    {
        PlayerHp.Value -= damage;

        if (PlayerHp.Value < 0) PlayerHp.Value = 0;
    }
}
