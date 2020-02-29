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
    public bool IsStar => isStar;

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
        var startPos = transform.position;
        LeanTween.moveY(this.gameObject, 1.0f, 1.5f).setEaseInOutCubic();

        //await UniTask.Delay(skillExecutor.SkillGauge);
        await UniTask.WaitUntil(() => skillExecutor.SkillGauge <= 0);
        await UniTask.Delay(500);
        isStar = false;
        var targetPos = new Vector2(transform.position.x, -3f);
        LeanTween.move(this.gameObject, targetPos, 0.2f).setEaseInOutCubic();
    }

    /// <summary>
    /// 敵を破壊しスコアを加算する
    /// </summary>
    /// <param name="enemy">敵</param>
    private void BeatEnemy(GameObject enemy)
    {
        var value = enemy.GetComponent<BaseEnemy>().ScoreValue;
        onHit.OnNext(value);
        //Destroy(enemy);
        enemy.GetComponent<BaseEnemy>().StopMove();
        LeanTween.rotateZ(enemy, 50, 1f).setEaseInOutElastic();
        LeanTween.move(enemy, new Vector2(UnityEngine.Random.Range(-5,5), 10f), 0.5f);
        Destroy(enemy, 1f);
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
                Destroy(obj.gameObject);
                break;
            case "SkillItem":
                if(!isStar) skillExecutor.ChargeSkill(50).Forget();
                Destroy(obj.gameObject);
                break;
            default:
                break;
        }
    }
}
