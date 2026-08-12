using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using VContainer;

public class DiceController : MonoBehaviour
{
    [SerializeField] private DiceRoll[] diceRolls;
    [SerializeField] private int maxReRollCount = 3;

    [SerializeField] private int[] diceValue;

    public ReactiveProperty<int> defaultPower = new(0);
    public ReactiveProperty<int> bonusPower = new(0);
    public ReactiveProperty<int> finalPower = new(0);

    public ReactiveProperty<int> currentReRollCount; 

    public event Action FailReRollEvent;

    private DiceHandEvaluator diceHandEvaluator;
    private SafetyBorder safetyBorder;

    [Header("보너스 점수")]
    [SerializeField] private int luckyPower = 80;
    [SerializeField] private int fourPower = 50;
    [SerializeField] private int straightPower = 40;
    [SerializeField] private int fullHousePower = 30;
    [SerializeField] private int triplePower = 20;
    [SerializeField] private int pairTwoPower = 15;
    [SerializeField] private int pairOnePower = 10;

    [Inject]
    public void Construct(SafetyBorder safetyBorder)
    {
        this.safetyBorder = safetyBorder;
    }

    private void Awake()
    {
        diceValue = new int[diceRolls.Length];
        diceHandEvaluator = new DiceHandEvaluator();
        currentReRollCount = new(maxReRollCount);
    }

    private void Start()
    {
        StartRoll();
    }

    public void StartRoll()
    {
        if (diceRolls == null) return;

        currentReRollCount.Value = maxReRollCount;

        safetyBorder.BlockUIInteraction(1f).Forget();

        defaultPower.Value = 0;
        for (int i = 0; i < diceRolls.Length; i++)
        {
            int result = diceRolls[i].Roll();
            diceValue[i] = result;
            
            defaultPower.Value += result;
        }

        DiceHandResult();
        finalPower.Value = defaultPower.Value + bonusPower.Value;
        Debug.Log(finalPower.Value);
    }

    public void ReRoll()
    {
        if (currentReRollCount.Value < 1)
        {
            Debug.Log("리롤 횟수를 모두 사용하였습니다.");
            return;
        }

        int doneRoll = 0;

        for (int i = 0; i < diceRolls.Length; i++)
        {
            if (diceRolls[i].IsSelected) doneRoll++;
        }

        if (doneRoll < 1)
        {
            Debug.Log("선택한 주사위가 없어서 진행하지 못했습니다.");
            FailReRollEvent?.Invoke();
            return;
        }

        safetyBorder.BlockUIInteraction(1f).Forget();

        for (int i = 0; i < diceRolls.Length; i++)
        {
            if (!diceRolls[i].IsSelected)
                continue;

            int previousResult = diceRolls[i].CurrentResult;

            int newResult = diceRolls[i].Roll();

            diceValue[i] = newResult;
            defaultPower.Value -= previousResult;
            defaultPower.Value += newResult;
            doneRoll ++;
        }

        DiceHandResult();
        currentReRollCount.Value --;
        finalPower.Value = defaultPower.Value + bonusPower.Value;
        Debug.Log(finalPower.Value);
    }

    public void ResetDice()
    {
        defaultPower.Value = 0;
        bonusPower.Value = 0;
        finalPower.Value = 0;

        StartRoll();
    }

    private void DiceHandResult()
    {
        bonusPower.Value = 0;
        DiceHand result = diceHandEvaluator.Evaluate(diceValue);
        switch (result)
        {
            case DiceHand.Lucky:
                bonusPower.Value = luckyPower;
                break;
            case DiceHand.Four:
                bonusPower.Value = fourPower;
                break;
            case DiceHand.Straight:
                bonusPower.Value = straightPower;
                break;
            case DiceHand.FullHouse:
                bonusPower.Value = fullHousePower;
                break;
            case DiceHand.Triple:
                bonusPower.Value = triplePower;
                break;
            case DiceHand.Pair_2:
                bonusPower.Value = pairTwoPower;
                break;
            case DiceHand.Pair_1:
                bonusPower.Value = pairOnePower;
                break;
            case DiceHand.None:
                return;
        }
    }
}
