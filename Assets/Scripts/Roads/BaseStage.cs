using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public abstract class BaseStage : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject trackb;
    [SerializeField] private GameObject trackr;
    [SerializeField] private GameObject trackg;
    [SerializeField] private GameObject item;
    [SerializeField] private GameObject skillItem;
    [SerializeField] private float enemyRate;
    [SerializeField] private float trackrRate;
    [SerializeField] private float trackgRate;
    [SerializeField] private float trackbRate;
    [SerializeField] private float itemRate;
    [SerializeField] private float skillItemRate;
    //[SerializeField] private GameStateManager stateManager;
    private GameStateManager stateManager;
    private PlayerHitDetector hitDetector;
    private bool isPlaying = true;
    private float[] xPos = new float[] { -1.9f, -0.6f, 0.6f, 1.9f };
    private float tempRate;
    private float interval = 0.5f;
    private float preInterval;

    Dictionary<int, GameObject> objDict;
    Dictionary<int, float> dropDict;

    private void Start()
    {
        preInterval = interval;
        Observable.Interval(TimeSpan.FromSeconds(interval))
            .Where(_ => isPlaying == true)
            .Subscribe(_ => {
                if (hitDetector.IsStar) interval = 0.05f;
                if (hitDetector.IsStar == false) interval = preInterval;
                Generate();
            }).AddTo(this);

        stateManager = GameObject.Find("Manager").GetComponent<GameStateManager>();
        hitDetector = GameObject.Find("Player").GetComponent<PlayerHitDetector>();

        stateManager.CurrentState
            .FirstOrDefault(x => x == GameState.Finish)
            .Subscribe(_ => isPlaying = false).AddTo(this);

        tempRate = skillItemRate;
        this.ObserveEveryValueChanged(x => x.hitDetector.IsStar)
            .Subscribe(_ => {
                if (hitDetector.IsStar) {
                    skillItemRate = 0;
                    return;
                }
                skillItemRate = tempRate;
            }).AddTo(this);
    }

    /// <summary>
    /// オブジェクトを生成する
    /// </summary>
    private void Generate()
    {
        InitializeDicts();
        int id = RandomChoose();
        if (id == 6) return;
        if(id == 0) {

        }
        var obj = Instantiate(objDict[id]);
        obj.transform.position = RandomPos();
    }

    /// <summary>
    /// ランダムな座標を与える
    /// </summary>
    /// <returns></returns>
    private Vector2 RandomPos()
    {
        var index = UnityEngine.Random.Range(0, xPos.Length);
        return new Vector2(xPos[index], 7.0f);
    }

    /// <summary>
    /// 生成するオブジェクトをランダムで選ぶ
    /// </summary>
    /// <returns></returns>
    private int RandomChoose()
    {
        float total = 0;

        foreach (KeyValuePair<int,float> e in dropDict) {
            total += e.Value;
        }

        float randomPoint = UnityEngine.Random.value * total;

        foreach (KeyValuePair<int,float> e in dropDict) {
            if (randomPoint < e.Value) {
                return e.Key;
            }
            else {
                randomPoint -= e.Value;
            }
        }
        return 0;
    }

    /// <summary>
    /// 選択するオブジェクトDictionaryの初期化
    /// </summary>
    private void InitializeDicts()
    {
        objDict = new Dictionary<int, GameObject>();
        objDict.Add(0,enemy);
        objDict.Add(1, trackr);
        objDict.Add(2, trackg);
        objDict.Add(3, trackb);
        objDict.Add(4, item);
        objDict.Add(5, skillItem);
        objDict.Add(6, null);

        dropDict = new Dictionary<int, float>();
        dropDict.Add(0,enemyRate);
        dropDict.Add(1, trackrRate);
        dropDict.Add(2, trackgRate);
        dropDict.Add(3, trackbRate);
        dropDict.Add(4, itemRate);
        dropDict.Add(5, skillItemRate);
        dropDict.Add(6, 100 - (enemyRate + trackrRate + trackgRate + trackbRate + itemRate + skillItemRate));
    }
}
