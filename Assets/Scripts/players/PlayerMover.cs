using UniRx;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private PlayerInput input;

    private void Start() {
        input = GetComponent<PlayerInput>();
        Vector2 direction = Vector2.zero;

        input.MoveRightStream
            .Subscribe(_ => {
                direction.x = +0.5f;
                Move(direction);
            }).AddTo(gameObject);
        input.MoveLeftStream
            .Subscribe(_ => {
                direction.x = 0.5f * -1;
                Move(direction);
            }).AddTo(gameObject);

        input.SkillStream
            .Subscribe(_ => Debug.Log("Skill")).AddTo(gameObject);
    }

    private void Move(Vector2 direction) {
        Vector2 pos = transform.position;
        if (pos.x == -1 && direction.x == -0.5f) return;
        if (pos.x == 0.5 && direction.x == 0.5f) return;
        pos += direction;
        transform.position = pos;
    }
}
