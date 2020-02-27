using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public abstract class BaseStage : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject item;
    [SerializeField] private float enemyRate;
    [SerializeField] private float itemRate;
    [SerializeField] private GameStateManager stateManager;

    private bool isPlaying = true;
    private float[] xPos = new float[] { -1.9f, -0.6f, 0.6f, 1.9f };

    Dictionary<int, GameObject> objDict;
    Dictionary<int, float> dropDict;

    private void Start()
    {
        Observable.Interval(TimeSpan.FromSeconds(0.5f))
            .Where(_ => isPlaying)
            .Subscribe(_ => {
                Generate();
                Debug.Log("b");
            }).AddTo(this);

        stateManager.CurrentState
            .FirstOrDefault(x => x == GameState.Finish)
            .Subscribe(_ => isPlaying = false);
    }

    /// <summary>
    /// オブジェクトを生成する
    /// </summary>
    private void Generate()
    {
        InitializeDicts();
        int id = RandomChoose();
        if (id == 2) return;
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
        return new Vector2(xPos[index], transform.position.y);
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
        objDict.Add(1, item);
        objDict.Add(2, null);

        dropDict = new Dictionary<int, float>();
        dropDict.Add(0,enemyRate);
        dropDict.Add(1, itemRate);
        dropDict.Add(2, 100 - (enemyRate+itemRate));
    }
}
