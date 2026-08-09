using DG.Tweening;
using UnityEngine;

public class DiceView : MonoBehaviour
{
    [SerializeField] private Vector3[] resultRotations = new Vector3[6];

    private Vector3 startPosition;
    private Sequence rollSequence;

    private void Awake()
    {
        startPosition = transform.localPosition;
    }

    public void PlayRoll(int result)
    {
        if (result < 1 || result > 6)
            return;
        
        rollSequence?.Kill();

        Vector3 targetRotation = resultRotations[result - 1];
        Vector3 spinRotation = targetRotation + new Vector3(
            720f,
            1080f,
            720f
        );
        Vector3 randomPosition1 = startPosition + new Vector3(
            Random.Range(-0.2f, 0.2f),
            2f,
            Random.Range(-0.2f, 0.2f)
        );
        Vector3 randomPosition2 = startPosition + new Vector3(
            Random.Range(-0.4f, 0.4f),
            0f,
            Random.Range(-0.4f, 0.4f)
        );

        rollSequence = DOTween.Sequence();

        rollSequence
            .Append(
                transform
                    .DOLocalMove(randomPosition1, 0.5f)
                    .SetEase(Ease.OutQuad)
            )
            .Append(
                transform
                    .DOLocalMove(randomPosition2, 0.25f)
                    .SetEase(Ease.InQuad)
            );

        rollSequence.Insert(
            0f,
            transform
                .DOLocalRotate(
                    spinRotation,
                    0.78f,
                    RotateMode.FastBeyond360
                )
                .SetEase(Ease.OutCubic)
        );

        rollSequence.OnComplete(() =>
        {
            transform.localPosition = randomPosition2;
            transform.localRotation = Quaternion.Euler(targetRotation); 
        });
    }
}
