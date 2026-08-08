using UnityEngine;

[CreateAssetMenu(fileName = "GuideDialogueData", menuName = "Scriptable Objects/GuideDialogueData")]
public class GuideDialogueData : ScriptableObject
{
    public string player1AttackTurn;
    public string player1DefenseTurn;
    public string player2AttackTurn;
    public string player2DefenseTurn;
}
