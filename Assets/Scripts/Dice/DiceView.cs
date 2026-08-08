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

        rollSequence = DOTween.Sequence();

        rollSequence
            .Append(
                transform
                    .DOLocalMoveY(startPosition.y + 2f, 0.25f)
                    .SetEase(Ease.InQuad)
            )
            .Append(
                transform
                    .DOLocalMoveY(startPosition.y, 0.35f)
                    .SetEase(Ease.InQuad)
            )
            .Append(
                transform
                    .DOLocalMoveY(startPosition.y + 0.2f, 0.08f)
                    .SetEase(Ease.InQuad)
            )
            .Append(
                transform
                    .DOLocalMoveY(startPosition.y, 0.1f)
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
    }
}
