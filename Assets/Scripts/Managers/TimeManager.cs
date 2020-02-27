using UniRx.Async;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private void Start()
    {
        CountDownAsync().Forget();
    }

    private async UniTask CountDownAsync()
    {
        await UniTask.Delay(1);
    }
}
