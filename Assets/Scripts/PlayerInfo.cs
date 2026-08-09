using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int Player1Hp { get; private set; } = 100;
    public int Player2Hp { get; private set; } = 100;

    public void Player1Damaged(int damage)
    {
        Player1Hp -= damage;

        if (Player1Hp < 0) Player1Hp = 0;
    }

    public void Player2Damaged(int damage)
    {
        Player2Hp -= damage;

        if (Player2Hp < 0) Player2Hp = 0;
    }
}
