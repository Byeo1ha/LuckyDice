using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    [SerializeField] private DiceRoll[] diceRolls;
    [SerializeField] private int maxReRollCount = 3;

    public ReactiveProperty<int> power = new(0);
    
    private int currentReRollCount; 

    private void Start()
    {
        StartRoll();
    }

    public void StartRoll()
    {
        if (diceRolls == null) return;

        currentReRollCount = maxReRollCount;

        power.Value = 0;
        for (int i = 0; i < diceRolls.Length; i++)
        {
            int result = diceRolls[i].Roll();
            power.Value += result;
        }
    }

    public void ReRoll()
    {
        if (currentReRollCount < 1)
        {
            Debug.Log("리롤 횟수를 모두 사용하였습니다.");
            return;
        }

        int doneRoll = 0;

        for (int i = 0; i < diceRolls.Length; i++)
        {
            if (!diceRolls[i].IsSelected)
                continue;

            int previousResult = diceRolls[i].CurrentResult;

            int newResult = diceRolls[i].Roll();

            power.Value -= previousResult;
            power.Value += newResult;
            doneRoll ++;
        }

        if (doneRoll < 1)
        {
            Debug.Log("선택한 주사위가 없어서 진행하지 못했습니다.");
            return;
        }

        currentReRollCount --;
    }

    public async UniTask ResetDice()
    {
        power.Value = 0;

        await UniTask.NextFrame();

        StartRoll();
    }
}
