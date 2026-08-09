using R3;
using TMPro;
using UnityEngine;
using VContainer;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text player1HpText;
    [SerializeField] private TMP_Text player2HpText;

    private PlayerInfo playerInfo;

    [Inject]
    public void Construct(PlayerInfo playerInfo)
    {
        this.playerInfo = playerInfo;
    }

    private void Start()
    {
        playerInfo.Player1Hp
            .Subscribe(Player1HpUpdateUI)
            .AddTo(this);

        playerInfo.Player2Hp
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
