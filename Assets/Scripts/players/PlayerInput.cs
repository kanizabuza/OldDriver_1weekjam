using UniRx;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private readonly Subject<Unit> moveRightStream = new Subject<Unit>();
    private readonly Subject<Unit> moveLeftStream = new Subject<Unit>();
    private readonly Subject<Unit> skillStream = new Subject<Unit>();
    public Subject<Unit> MoveRightStream => moveRightStream;
    public Subject<Unit> MoveLeftStream => moveLeftStream;
    public Subject<Unit> SkillStream => skillStream;

    /// <summary>
    /// InputをObserveする
    /// </summary>
    private void Start() {
        InputAsObservable.GetKeyDown(KeyCode.RightArrow)
            .Subscribe(moveRightStream.OnNext).AddTo(this);
        InputAsObservable.GetKeyDown(KeyCode.LeftArrow)
            .Subscribe(moveLeftStream.OnNext).AddTo(this);
        InputAsObservable.GetKeyDown(KeyCode.Space)
            .Subscribe(skillStream.OnNext).AddTo(this);
    }
}
