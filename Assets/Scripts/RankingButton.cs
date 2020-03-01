using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankingButton : MonoBehaviour
{
    [SerializeField] private Button rankingButton;
    private ScoreManager scoreManager;

    void Start()
    {
        rankingButton.onClick.AddListener(showRanking);
        scoreManager = GameObject.Find("Manager").GetComponent<ScoreManager>();
    }

    public void showRanking()
    {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(scoreManager.TotalScore);
        //SceneManager.LoadScene("Ranking", LoadSceneMode.Additive);
    }
}
