using System;
using UniRx;
using UniRx.Async;
using UnityEngine;

public class PlayerHitDetector : MonoBehaviour
{
    [SerializeField] private PlayerSkillExecutor skillExecutor;

    private Subject<Unit> onDeath = new Subject<Unit>();
    private Subject<int> onHit = new Subject<int>();
    public IObservable<Unit> OnDeath => onDeath;
    public IObservable<int> OnHit => onHit;

    private bool isStar = false;

    private void Start()
    {
        skillExecutor.OnSkill.Subscribe(async _ => await ChangeStarState(0));
        onDeath.AddTo(this);
    }

    /// <summary>
    /// 無敵状態になる
    /// </summary>
    private async UniTask ChangeStarState(int value)
    {
        isStar = true;
        await UniTask.Delay(5000);
        isStar = false;
    }

    /// <summary>
    /// 敵を破壊しスコアを加算する
    /// </summary>
    /// <param name="enemy">敵</param>
    private void BeatEnemy(GameObject enemy)
    {
        var value = enemy.GetComponent<BaseEnemy>().ScoreValue;
        onHit.OnNext(value);
        Destroy(enemy);
    }

    /// <summary>
    /// 衝突判定
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D obj)
    {
        switch (obj.tag) {
            case "Enemy":
                if(!isStar) {
                    onDeath.OnNext(Unit.Default);
                    return;
                }
                BeatEnemy(obj.gameObject);
                break;
            case "Item":
                var value = obj.GetComponent<BaseItem>().ScoreValue;
                onHit.OnNext(value);
                Destroy(obj);
                break;
            case "SkillItem":
                skillExecutor.ChargeSkill(100);
                Destroy(obj);
                break;
            default:
                break;
        }
    }
}
