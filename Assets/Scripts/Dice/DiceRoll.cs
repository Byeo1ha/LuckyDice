using UnityEngine;

[RequireComponent(typeof(DiceView))]
public class DiceRoll : MonoBehaviour
{
    [SerializeField] private GameObject outLine;
    private DiceView diceView;

    public int CurrentResult { get; private set; }
    public bool IsSelected { get; private set; }

    public enum DiceType
    {
        D4,
        D6
    }

    [SerializeField] private DiceType diceType;

    public DiceType DT => diceType;

    private void Awake()
    {
        diceView = GetComponent<DiceView>();
        outLine.SetActive(IsSelected);
    }

    public int Roll()
    {
        IsSelected = false;
        outLine.SetActive(IsSelected);

        switch (diceType)
        {
            case DiceType.D4:
                CurrentResult = Random.Range(1, 5);
                break;
            case DiceType.D6:
                CurrentResult = Random.Range(1, 7);
                break;
        }
        
        diceView.PlayRoll(CurrentResult);
        
        return CurrentResult;
    }

    private void OnMouseDown()
    {
        IsSelected = !IsSelected;
        outLine.SetActive(IsSelected);
    }
}
