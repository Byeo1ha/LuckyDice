using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using R3;
using VContainer;

[RequireComponent(typeof(TurnGuideText))]
public class TurnController : MonoBehaviour
{
    [SerializeField] private TurnUI turnUI;

    private DiceController diceController;
    private TurnGuideText turnGuideText;

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
    public void Construct(DiceController diceController)
    {
        this.diceController = diceController;
    }

    private void Awake()
    {
        turnGuideText = GetComponent<TurnGuideText>();
        battlePhase = BattlePhase.Player1Attack;
    }

    private void Start()
    {
        diceController.power
            .Subscribe(power => this.power = power)
            .AddTo(this);

        Player1TurnAttack().Forget();
    }

    private async UniTask Player1TurnAttack()
    {
        turnGuideText.Player1AttackTurn();
        turnGuideText.FadeIn();
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        turnGuideText.FadeOut();
    }

    public void PhaseFinish()
    {
        Debug.Log("공격 선언 완료");
        Debug.Log($"최종 피해량 {power}");
        
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
        }
    }

    private async void StartPlayer2Defense()
    {
        battlePhase = BattlePhase.Player2Defense;

        await turnUI.ShowAlertPannel(); //요기

        Player2TurnDefense().Forget();
        diceController.ResetDice().Forget();
    }

    private async void StartPlayer1AttackFight()
    {
        await turnUI.ShowAlertPannel();
        
        turnUI.FightUI(player1AttackPower, player2DefensePower);
        turnUI.Player1CardFight();
    }

    private async UniTask Player2TurnDefense()
    {
        turnGuideText.Player2DefenseTurn();
        turnGuideText.FadeIn();
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        turnGuideText.FadeOut();
    }
}
