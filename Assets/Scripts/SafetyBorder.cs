using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SafetyBorder : MonoBehaviour
{
    private CancellationTokenSource cts;

    public void Active()
    {
        gameObject.SetActive(true);
    }

    public void DeActive()
    {
        gameObject.SetActive(false);
    }

    public async UniTask BlockUIInteraction(float time)
    {
        cts?.Cancel();
        cts?.Dispose();

        cts = new CancellationTokenSource();

        Active();

        await UniTask.Delay(
            TimeSpan.FromSeconds(time),
            cancellationToken: cts.Token
        );
        
        DeActive();
    }
}
