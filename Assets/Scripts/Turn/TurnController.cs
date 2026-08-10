using Cysharp.Threading.Tasks;
using UnityEngine;
using R3;
using VContainer;
using System;

[RequireComponent(typeof(TurnGuideText))]
public class TurnController : MonoBehaviour
{
    [SerializeField] private TurnUI turnUI;

    private DiceController diceController;
    private TurnGuideText turnGuideText;

    private PlayerInfo player1;
    private PlayerInfo player2;

    private Action<int> FinalPowerAction;

    private int power;
    private int player1AttackPower;
    private int player2AttackPower;
    private int player1DefensePower;
    private int player2DefensePower;

    private enum BattlePhase
    {
        Player1Attack,
        Player2Defense,
        Player2Attack,
        Player1Defense
    }

    private BattlePhase battlePhase;

    [Inject]
    public void Construct(
        DiceController diceController,
        [Key(PlayerKey.Player1)]PlayerInfo player1,
        [Key(PlayerKey.Player2)]PlayerInfo player2
        )
    {
        this.diceController = diceController;
        this.player1 = player1;
        this.player2 = player2;
        
    }

    private void Awake()
    {
        turnGuideText = GetComponent<TurnGuideText>();
        battlePhase = BattlePhase.Player1Attack;
    }

    private void Start()
    {
        diceController.finalPower
            .Subscribe(power => this.power = power)
            .AddTo(this);

        Player1TurnAttack();
    }

    public void PhaseFinish()
    { 
        switch (battlePhase)
        {
            case BattlePhase.Player1Attack:
                player1AttackPower = power;
                StartPlayer2Defense();
                break;
            case BattlePhase.Player2Defense:
                player2DefensePower = power;
                StartPlayer1AttackFight();
                break;
            case BattlePhase.Player2Attack:
                player2AttackPower = power;
                StartPlayer1Defense();
                break;
            case BattlePhase.Player1Defense:
                player1DefensePower = power;
                StartPlayer2AttackFight();
                break;
        }
    }

    private async void StartPlayer1Defense()
    {
        battlePhase = BattlePhase.Player1Defense;

        await turnUI.ShowAlertPannel();

        Player1TurnDefense();
        diceController.ResetDice();
    }

    private async void StartPlayer2Defense()
    {
        battlePhase = BattlePhase.Player2Defense;

        await turnUI.ShowAlertPannel();

        Player2TurnDefense();
        diceController.ResetDice();
    }

    private async void StartPlayer1AttackFight()
    {
        await turnUI.ShowAlertPannel();

        int damage = player1AttackPower - player2DefensePower;
        Debug.Log($"최종 >> Player 1 : {player1AttackPower}, Player 2 : {player2DefensePower}");
        if (damage < 0) damage = 0;
        
        await turnUI.FightUI(player1AttackPower, player2DefensePower);

        player2.PlayerDamaged(damage);
        battlePhase = BattlePhase.Player2Attack;
        diceController.ResetDice();

        Player2TurnAttack();
        turnGuideText.Fade().Forget();
    }

    private async void StartPlayer2AttackFight()
    {
        await turnUI.ShowAlertPannel();

        int damage = player2AttackPower - player1DefensePower;
        if (damage < 0) damage = 0;
        
        await turnUI.FightUI(player1DefensePower, player2AttackPower);

        player1.PlayerDamaged(damage);
        Debug.Log($"총 {damage} 만큼의 피해를 받았습니다.");
        battlePhase = BattlePhase.Player1Attack;
        diceController.ResetDice();

        Player1TurnAttack();
        turnGuideText.Fade().Forget();
    }

    private void Player1TurnAttack()
    {
        turnGuideText.Player1AttackTurn();
        turnGuideText.Fade().Forget();
    }

    private void Player2TurnAttack()
    {
        turnGuideText.Player2AttackTurn();
        turnGuideText.Fade().Forget();
    }

    private void Player1TurnDefense()
    {
        turnGuideText.Player1DefenseTurn();
        turnGuideText.Fade().Forget();
    }

    private void Player2TurnDefense()
    {
        turnGuideText.Player2DefenseTurn();
        turnGuideText.Fade().Forget();
    }
}
