using UnityEngine;
using R3;
using TMPro;
using VContainer;
using DG.Tweening;

public class DiceUI : MonoBehaviour
{
    [SerializeField] private TMP_Text defaultPowerText;
    [SerializeField] private TMP_Text plusText;
    [SerializeField] private TMP_Text bonusPowerText;
    [SerializeField] private GameObject rollButton;
    [SerializeField] private GameObject finishButton;
    [SerializeField] private GameObject reRollButton;

    private DiceController diceController;
    private Sequence defaultPowerAnimation;
    private Sequence bonusPowerAnimation;

    [Inject]
    public void Construct(DiceController diceController)
    {
        this.diceController = diceController;
    }

    private void Awake()
    {
        defaultPowerText.alpha = 0f;
        plusText.alpha = 0f;
        bonusPowerText.alpha = 0f;
    }

    private void Start()
    {
        diceController.defaultPower
            .Subscribe(DefaultPowerFade)
            .AddTo(this);
        
        diceController.bonusPower
            .Subscribe(BonusPowerFade)
            .AddTo(this);
    }

    private void UpdateDefaultPowerUI(int power)
    {
        defaultPowerText.text = power.ToString();
    }

    private void UpdateBonusPowerUI(int power)
    {
        bonusPowerText.text = power.ToString();
    }

    private void DefaultPowerFade(int power)
    {
        defaultPowerAnimation?.Kill();
        defaultPowerAnimation = DOTween.Sequence();

        defaultPowerAnimation
            .Append(
                defaultPowerText.DOFade(0f, 0.5f)
            )
            .AppendCallback(
                () => UpdateDefaultPowerUI(power)
            )
            .Append(
                defaultPowerText.DOFade(1f, 0.5f)
            );
    }

    private void BonusPowerFade(int power)
    {
        bonusPowerAnimation?.Kill();
        bonusPowerAnimation = DOTween.Sequence();

        bonusPowerAnimation
            .Append(
                bonusPowerText.DOFade(0f, 0.5f)
            )
            .Join(
                plusText.DOFade(0f, 0.5f)
            )
            .AppendCallback(
                () => UpdateBonusPowerUI(power)
            )
            .Append(
                bonusPowerText.DOFade(1f, 0.5f)
            )
            .Join(
                plusText.DOFade(1f, 0.5f)
            );
    }
}
