using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(DiceView))]
public class DiceRoll : MonoBehaviour
{
    [SerializeField] private GameObject outLine;
    private DiceView diceView;

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
        if (EventSystem.current.IsPointerOverGameObject()) return;

        IsSelected = !IsSelected;
        outLine.SetActive(IsSelected);
    }
}
