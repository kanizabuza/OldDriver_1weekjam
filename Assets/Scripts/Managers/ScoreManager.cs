using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private PlayerSkillExecutor skillExecutor;
    [SerializeField] private Text scoreText;

    private int totalScore = 0;

    private void Start()
    {
        skillExecutor.OnSkill.Subscribe(AddScore);
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
