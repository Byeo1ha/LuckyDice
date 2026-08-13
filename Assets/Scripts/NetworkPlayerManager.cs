using Unity.Netcode;
using UnityEngine;

public class NetworkPlayerManager : NetworkBehaviour
{
    public NetworkVariable<ulong> Player1ClientId = new();
    public NetworkVariable<ulong> Player2ClientId = new();

    public bool IsPlayer1 => 
        NetworkManager.Singleton.LocalClientId == Player1ClientId.Value;

    public bool IsPlayer2 => 
        NetworkManager.Singleton.LocalClientId == Player2ClientId.Value;

    public override void OnNetworkSpawn()
    {
        if (!IsServer)
            return;

        Player1ClientId.Value = NetworkManager.Singleton.LocalClientId;
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;

        Debug.Log($"Player1 : {Player1ClientId}");
    }

    public override void OnNetworkDespawn()
    {
        if (NetworkManager.Singleton != null && IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        if (clientId == Player1ClientId.Value) return;

        Player2ClientId.Value = clientId;

        Debug.Log($"Client Connected : {clientId}");
    }
}
