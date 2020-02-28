using System.Collections.Generic;
using UniRx;
using UniRx.Async;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject[] stages;
    [SerializeField] private ScoreManager scoreManager;

    Dictionary<int, GameObject> stageDict;
    Dictionary<int, int> scoreDict;

    private int stageId = 0;
    private GameObject preStage;
    private GameObject stage;

    private void Start()
    {
        InitializeDicts();

        stage = Instantiate(stageDict[stageId]);
        preStage = stage;

        this.ObserveEveryValueChanged(_ => scoreManager.TotalScore)
            .Where(t => t > scoreDict[stageId])
            .Subscribe(async _ => await ChangeStage()).AddTo(this);
    }

    /// <summary>
    /// ステージを変更する
    /// </summary>
    private async UniTask ChangeStage()
    {
        preStage = stage;
        stage = Instantiate(stageDict[stageId]);
        stageId++;

        await UniTask.Delay(5000);
        Destroy(preStage);
    }

    /// <summary>
    /// Dictionaryの初期化
    /// </summary>
    private void InitializeDicts()
    {
        stageDict = new Dictionary<int, GameObject>();
        var i = 0;
        foreach (var s in stages) {
            stageDict.Add(i, stages[i]);
            i++;
        }

        scoreDict = new Dictionary<int, int>();
        for (var j = 0; j < stages.Length; j++) {
            scoreDict.Add(j, (int)(250 * Mathf.Pow(2f,j)));
        }
    }
}
