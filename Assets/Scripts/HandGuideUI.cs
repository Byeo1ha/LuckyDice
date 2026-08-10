using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandGuideUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;

    private Sequence uiSlideAnim;

    private float originalPosY;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosY = rectTransform.anchoredPosition.y;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseExit();
    }

    private void MouseEnter()
    {
        Debug.Log("내려옵니다.");
        uiSlideAnim?.Kill();
        uiSlideAnim = DOTween.Sequence();

        uiSlideAnim
            .Append(
                rectTransform.DOAnchorPosY(140f, 0.5f)
            )
            .Append(
                rectTransform.DOAnchorPosY(160f, 0.3f)
            );
    }

    private void MouseExit()
    {
        Debug.Log("올라갑니다.");
        uiSlideAnim?.Kill();
        uiSlideAnim = DOTween.Sequence();
        
        uiSlideAnim.Append
            (rectTransform.DOAnchorPosY(originalPosY, 0.5f));
    }
}
