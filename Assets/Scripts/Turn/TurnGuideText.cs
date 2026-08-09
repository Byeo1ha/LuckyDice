using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TurnGuideText : MonoBehaviour
{
    [SerializeField] private TMP_Text guideText;
    [SerializeField] private GuideDialogueData guideDialogueData;

    public async UniTask Fade()
    {
        guideText.alpha = 0f;
        guideText.DOFade(1f, 0.5f);
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        
        guideText.DOFade(0f, 0.5f);
    }

    public void FadeIn()
    {
        guideText.alpha = 0f;
        guideText.DOFade(1f, 0.5f);
    }

    public void FadeOut()
    {
        guideText.alpha = 1f;
        guideText.DOFade(0f, 0.5f);
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
}
