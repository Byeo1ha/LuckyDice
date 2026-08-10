using R3;
using TMPro;
using UnityEngine;
using VContainer;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text player1HpText;
    [SerializeField] private TMP_Text player2HpText;

    private PlayerInfo player1;
    private PlayerInfo player2;

    [Inject]
    public void Construct(
        [Key(PlayerKey.Player1)]PlayerInfo player1,
        [Key(PlayerKey.Player2)]PlayerInfo player2)
    {
        this.player1 = player1;
        this.player2 = player2;
    }

    private void Start()
    {
        player1.PlayerHp
            .Subscribe(Player1HpUpdateUI)
            .AddTo(this);

        player2.PlayerHp
            .Subscribe(Player2HpUpdateUI)
            .AddTo(this);
    }

    private void Player1HpUpdateUI(int hp)
    {
        player1HpText.text = hp.ToString();
    }

    private void Player2HpUpdateUI(int hp)
    {
        player2HpText.text = hp.ToString();
    }
}
