using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnGuideText : MonoBehaviour
{
    [SerializeField] private TMP_Text guideText;
    //[SerializeField] private TMP_Text reRollFailGuideText;
    [SerializeField] private Image guideBackground;
    [SerializeField] private GuideDialogueData guideDialogueData;

    private DiceController diceController;
    private Sequence reRollFailSequence;

    /*private void OnEnable()
    {
        diceController.FailReRollEvent += ReRollFail;
    }

    private void OnDisable()
    {
        diceController.FailReRollEvent -= ReRollFail;
    }*/

    //Fade 구조를 DOTween으로 변경할 필요가 있음
    public async UniTask Fade()
    {
        FadeIn();
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        FadeOut();
    }

    public void FadeIn()
    {
        guideText.alpha = 0f;
        guideBackground.color = new Color(0f, 0f, 0f, 0f);
        guideText.DOFade(1f, 0.5f);
        guideBackground.DOFade(0.5f, 0.5f);
    }

    public void FadeOut()
    {
        guideText.alpha = 1f;
        guideBackground.color = new Color(0f, 0f, 0f, 0.5f);
        guideText.DOFade(0f, 0.5f);
        guideBackground.DOFade(0f, 0.5f);
    }

    public void Player1AttackTurn()
    {
        guideText.text = guideDialogueData.player1AttackTurn;
    }

    public void Player1DefenseTurn()
    {
        guideText.text = guideDialogueData.player1DefenseTurn;
    }

    public void Player2AttackTurn()
    {
        guideText.text = guideDialogueData.player2AttackTurn;
    }

    public void Player2DefenseTurn()
    {
        guideText.text = guideDialogueData.player2DefenseTurn;
    }

    //ReRollFail 텍스트를 임의로 만들지말고 기존 guide Text를 재활용하자.
    /*private void ReRollFail()
    {
        reRollFailSequence?.Kill();
        reRollFailSequence = DOTween.Sequence();

        reRollFailGuideText.alpha = 0f;
        guideBackground.color = new Color(0f, 0f, 0f, 0f);

        reRollFailSequence
            .Append(
                reRollFailGuideText.DOFade(1f, 0.5f)
            )
            .Join(
                guideBackground.DOFade(0.5f, 0.5f)
            )
            .AppendInterval(1f)
            .Append(
                reRollFailGuideText.DOFade(0f, 0.5f)
            )
            .Join(
                guideBackground.DOFade(0f, 0.5f)
            )
            ;
    }*/
}
