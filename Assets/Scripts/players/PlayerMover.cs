using System;
using System.Collections;
using UniRx;
using UniRx.Async;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private PlayerInput input;
    private bool isRight = false;
    private float[] xPos = { -1.9f, -0.6f, 0.6f, 1.9f };
    private float currentPos = 0.6f;

    private void Start()
    {
        input = GetComponent<PlayerInput>();

        input.MoveRightStream
            .TakeUntilDestroy(this)
            .Subscribe(_ =>
            {
                isRight = true;
                MoveAsync(isRight).Forget();
            });
        input.MoveLeftStream
            .TakeUntilDestroy(this)
            .Subscribe(_ => {
                isRight = false;
                MoveAsync(isRight).Forget();
            });
    }

    /// <summary>
    /// 左右移動
    /// </summary>
    /// <param name="direction">移動方向</param>
    private async UniTask MoveAsync(bool isRight)
    {        
        float pos = currentPos;

        int moveId = 0;
        if (pos < -0.6f && !isRight) return;
        if (pos < -0.6f && isRight) moveId = 1;
        if (pos >= -0.6f && pos <= 0.6f && !isRight) moveId = 0;
        if (pos >= -0.6f && pos <= 0.6f && isRight) moveId = 2;
        if (pos >= 0.6f && pos < 1.9f && !isRight)moveId = 1;
        if (pos >= 0.6f && pos < 1.9f && isRight) moveId = 3;
        if (pos > 0.6f && !isRight) moveId = 2;
        if (pos > 0.6f && isRight) return;

        LeanTween.moveX(this.gameObject, xPos[moveId], 0.3f).setEaseOutCubic();
        currentPos = xPos[moveId];
        await UniTask.CompletedTask;
    }
}
