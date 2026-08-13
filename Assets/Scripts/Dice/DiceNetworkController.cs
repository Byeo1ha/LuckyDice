using Unity.Netcode;
using UnityEngine;
using VContainer;

public class DiceNetworkController : NetworkBehaviour
{
    private DiceController diceController;
    private NetworkPlayerManager playerManager;

    [Inject]
    public void ConStruct(
        DiceController diceController,
        NetworkPlayerManager playerManager)
    {
        this.diceController = diceController;
        this.playerManager = playerManager;
    }

    /*public override void OnNetworkSpawn()
    {
        if (!IsServer)
            return;

        int[] results = diceController.RollAllDice();

        ApplyRollResultRpc(results);
    }*/

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) 
            && NetworkManager.Singleton.ConnectedClientsIds.Count > 1)
        {
            Debug.Log("주사위 굴리기 시작");
            RequestStartRoll();
        }
    }

    public void RequestStartRoll()
    {
        RequestStartRollRpc();
    }

    [Rpc(SendTo.Server)]
    private void RequestStartRollRpc()
    {
        int[] results = diceController.RollAllDice();

        ApplyRollResultRpc(results);
    }

    [Rpc(SendTo.Everyone)]
    private void ApplyRollResultRpc(int[] results)
    {
        if (IsServer)
            return;
        
        diceController.ApplyRollResults(results);
    }
}
