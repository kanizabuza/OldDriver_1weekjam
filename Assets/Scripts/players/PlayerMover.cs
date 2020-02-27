using UniRx;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private PlayerInput input;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        Vector2 direction = Vector2.zero;

        input.MoveRightStream
            .Subscribe(_ => {
                direction.x = +1.3f;
                Move(direction);
            }).AddTo(gameObject);
        input.MoveLeftStream
            .Subscribe(_ => {
                direction.x = 1.3f * -1;
                Move(direction);
            }).AddTo(gameObject);
    }

    /// <summary>
    /// 左右移動
    /// </summary>
    /// <param name="direction">移動方向</param>
    private void Move(Vector2 direction)
    {
        Vector2 pos = transform.position;
        if (pos.x <= -1.9f && direction.x == -1.3f) return;
        if (pos.x >= 1.9f && direction.x == 1.3f) return;
        pos += direction;
        transform.position = pos;
    }
}
