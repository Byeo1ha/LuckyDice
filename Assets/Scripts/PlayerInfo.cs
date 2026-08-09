using R3;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public ReactiveProperty<int> Player1Hp = new(100);
    public ReactiveProperty<int> Player2Hp = new(100);

    public void Player1Damaged(int damage)
    {
        Player1Hp.Value -= damage;

        if (Player1Hp.Value < 0) Player1Hp.Value = 0;
    }

    public void Player2Damaged(int damage)
    {
        Player2Hp.Value -= damage;

        if (Player2Hp.Value < 0) Player2Hp.Value = 0;
    }
}
