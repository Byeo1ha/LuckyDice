using UnityEngine;

[RequireComponent(typeof(DiceView))]
public class DiceRoll : MonoBehaviour
{
    private DiceView diceView;

    [SerializeField] private GameObject outLine;

    public int CurrentResult { get; private set; }
    public bool IsSelected { get; private set; }

    private void Awake()
    {
        diceView = GetComponent<DiceView>();
        outLine.SetActive(IsSelected);
    }

    public int Roll()
    {
        IsSelected = false;
        outLine.SetActive(IsSelected);
        
        CurrentResult = Random.Range(1, 7);
        diceView.PlayRoll(CurrentResult);
        
        return CurrentResult;
    }

    private void OnMouseDown()
    {
        IsSelected = !IsSelected;

        outLine.SetActive(IsSelected);

        Debug.Log($"주사위 선택 상태 : {IsSelected}");
    }
}
