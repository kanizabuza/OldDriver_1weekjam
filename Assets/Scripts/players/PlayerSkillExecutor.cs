using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillExecutor : MonoBehaviour
{
    [SerializeField] private Animator cutInAnim;
    [SerializeField] private GameObject cutIn;
    [SerializeField] private Slider slider;
    [SerializeField] private int scoreValue = 10;

    private PlayerInput input;
    private float skillGauge = 0;

    private Subject<int> onSkill = new Subject<int>();
    public IObservable<int> OnSkill => onSkill; 

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        input.SkillStream
            .Where(_ => skillGauge > 90)
            .Subscribe(__ => ExecuteSkill()).AddTo(gameObject);

        this.ObserveEveryValueChanged(x => x.skillGauge)
            .Subscribe(_ => slider.value = skillGauge);

        onSkill.AddTo(this);
    }

    private void Update()
    {
        //test
        ChargeSkill(1f);
    }

    /// <summary>
    /// スキルを実行
    /// </summary>
    private void ExecuteSkill()
    {
        skillGauge = 0;

        onSkill.OnNext(scoreValue);

        cutIn.gameObject.SetActive(true);
        cutInAnim.Play("cutInAnimation",0,0.0f);
        Observable.Timer(TimeSpan.FromSeconds(0.75))
            .Subscribe(_ => {
                cutIn.SetActive(false);
            }).AddTo(this);
    }

    /// <summary>
    /// value分だけスキルゲージを溜める
    /// </summary>
    /// <param name="value">足す値</param>
    private void ChargeSkill(float value )
    {
        skillGauge += value;
        if (skillGauge >= 100) skillGauge = 100;
    }
}