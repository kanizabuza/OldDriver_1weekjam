using UniRx;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private PlayerHitDetector playerHit;

    private readonly ReactiveProperty<GameState> currentState = new ReactiveProperty<GameState>(GameState.Ready);
    public IReadOnlyReactiveProperty<GameState> CurrentState => currentState;

    private void Start()
    {
        playerHit.OnDeath
            .Subscribe(_ => currentState.Value = GameState.Finish);

        currentState.AddTo(this);
    }
}
