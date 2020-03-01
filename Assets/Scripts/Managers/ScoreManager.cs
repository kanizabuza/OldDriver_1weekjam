using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private PlayerSkillExecutor skillExecutor;
    [SerializeField] private PlayerHitDetector hitDetector;
    [SerializeField] private Text scoreText;

    private int totalScore = 0;
    public int TotalScore => totalScore;

    private StageManager stageManager;
    private GameStateManager stateManager;
    private bool isFinish = false;

    private void Start()
    {
        skillExecutor.OnSkill.Subscribe(AddScore);
        hitDetector.OnHit.Subscribe(AddScore);

        stageManager = GameObject.Find("Manager").GetComponent<StageManager>();
        stateManager = GameObject.Find("Manager").GetComponent<GameStateManager>();

        Observable.Interval(TimeSpan.FromSeconds(1f))
            .Where(_ => stageManager.IsTutorial != true &&  !isFinish)
            .Subscribe(_ => {
                scoreText.gameObject.SetActive(true);
                AddScore(1);
            }).AddTo(this);

        stateManager.CurrentState
            .FirstOrDefault(x => x == GameState.Finish)
            .Subscribe(_ => isFinish = true).AddTo(this);
    }

    /// <summary>
    /// スコア加算
    /// </summary>
    /// <param name="value">加算する得点</param>
    private void AddScore(int value)
    {
        totalScore += value;

        scoreText.text = "Score: " + totalScore.ToString();
    }
}
