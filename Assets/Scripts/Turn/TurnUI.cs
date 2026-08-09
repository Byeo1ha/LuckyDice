using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TurnUI : MonoBehaviour
{
    [SerializeField] private GameObject turnAlertPannel;

    [SerializeField] private TMP_Text player1Power;
    [SerializeField] private TMP_Text player2Power;

    [SerializeField] private RectTransform player1Card;
    [SerializeField] private RectTransform player2Card;
    
    private RectTransform turnAlertPannelTransform;
    private Sequence alertSequence;
    private Sequence player1CardShow;
    private Sequence player2CardShow;

    private void Awake()
    {
        turnAlertPannelTransform = turnAlertPannel.GetComponent<RectTransform>();
        player1Power.gameObject.SetActive(false);
        player2Power.gameObject.SetActive(false);
    }

    public async UniTask ShowAlertPannel()
    {
        if (turnAlertPannelTransform == null) return;

        turnAlertPannelTransform.anchoredPosition = new Vector3(2100f, 200f, 0f);

        alertSequence?.Kill();
        alertSequence = DOTween.Sequence();

        alertSequence
            .Append(
                turnAlertPannelTransform
                .DOAnchorPos(new Vector3(0f, 200f, 0f), 0.8f)
            )
            .AppendInterval(0.8f)
            .Append(
                turnAlertPannelTransform
                .DOAnchorPos(new Vector3(-2100f, 200f, 0f), 0.8f)
            )
            .AppendInterval(1f);

        await alertSequence.AsyncWaitForCompletion();
    }

    public async UniTask FightUI(int p1Power, int p2Power)
    {
        player1Power.gameObject.SetActive(true);
        player2Power.gameObject.SetActive(true);

        player1Power.alpha = 0f;
        player2Power.alpha = 0f;

        player1Power.DOFade(1f, 0.5f);
        player2Power.DOFade(1f, 0.5f);

        player1Power.text = p1Power.ToString();
        player2Power.text = p2Power.ToString();

        await UniTask.WhenAll(
            Player1CardFight(),
            Player2CardFight()
        );

    }

    private async UniTask Player1CardFight()
    {
        player1CardShow?.Kill();
        player1CardShow = DOTween.Sequence();

        Vector3 originalPos = player1Card.anchoredPosition;
        Vector3 originalScale = player1Card.localScale;

        player1CardShow
            .Append(
                player1Card
                .DOAnchorPos(
                    new Vector3(-650, -50, 0), 1f)
            )
            .Join(
                player1Card
                .DOScale(
                    new Vector3(0.4f, 0.4f, 0.4f), 1f)
            )
            .AppendInterval(3f)
            .Append(
                player1Card
                .DOAnchorPos(
                    originalPos, 1f)
            )
            .Join(
                player1Card
                .DOScale(
                    originalScale, 1f)
            )
            .Join(
                player1Power.DOFade(0f, 0.5f)
            );

        await player1CardShow.AsyncWaitForCompletion();
    }

    private async UniTask Player2CardFight()
    {
        player2CardShow?.Kill();
        player2CardShow = DOTween.Sequence();

        Vector3 originalPos = player2Card.anchoredPosition;
        Vector3 originalRotate = player2Card.localEulerAngles;
        Vector3 originalScale = player2Card.localScale;

        player2CardShow
            .Append(
                player2Card
                .DOAnchorPos(
                    new Vector3(650, -50, 0), 1f)
            )
            .Join(
                player2Card
                .DOScale(
                    new Vector3(0.4f, 0.4f, 0.4f), 1f)
            )
            .AppendInterval(3f)
            .Append(
                player2Card
                .DOAnchorPos(
                    originalPos, 1f)
            )
            .Join(
                player2Card
                .DOScale(
                    originalScale, 1f)
            )
            .Join(
                player2Card
                .DOLocalRotate(
                    originalRotate, 1f
                )
            )
            .Join(
                player2Power.DOFade(0f, 0.5f)
            );;

        player2CardShow.Insert(
            0f,
            player2Card
                .DOLocalRotate(
                    new Vector3(0f, 0f, 0f), 1f
                )
            );

        await player2CardShow.AsyncWaitForCompletion();
    }
}
