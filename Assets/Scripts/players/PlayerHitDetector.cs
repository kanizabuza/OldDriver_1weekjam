using System;
using UniRx;
using UnityEngine;

public class PlayerHitDetector : MonoBehaviour
{
    private Subject<Unit> onDeath = new Subject<Unit>();
    public IObservable<Unit> OnDeath => onDeath;

    private void Start()
    {
        onDeath.AddTo(this);
    }

    /// <summary>
    /// 衝突判定
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onDeath.OnNext(Unit.Default);
    }
}
