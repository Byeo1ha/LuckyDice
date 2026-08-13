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
        int result = Random.Range(1, 7);
        
        ApplyResult(result);
        
        return result;
    }

    public void ApplyResult(int result)
    {
        IsSelected = false;
        outLine.SetActive(IsSelected);

        CurrentResult = result;
        diceView.PlayRoll(result);
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        IsSelected = !IsSelected;
        outLine.SetActive(IsSelected);
    }
}
