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

    private void Start()
    {
        skillExecutor.OnSkill.Subscribe(AddScore);
        hitDetector.OnHit.Subscribe(AddScore);
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
