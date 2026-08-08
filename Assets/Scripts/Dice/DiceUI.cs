using UnityEngine;
using R3;
using TMPro;
using VContainer;

public class DiceUI : MonoBehaviour
{
    [SerializeField] private TMP_Text powerText;
    [SerializeField] private GameObject rollButton;
    [SerializeField] private GameObject finishButton;
    [SerializeField] private GameObject reRollButton;

    private DiceController diceController;

    [Inject]
    public void Construct(DiceController diceController)
    {
        this.diceController = diceController;
    }

    private void Start()
    {
        diceController.power
            .Subscribe(UpdateUI)
            .AddTo(this);
    }

    private void UpdateUI(int power)
    {
        powerText.text = power.ToString();
    }
}
