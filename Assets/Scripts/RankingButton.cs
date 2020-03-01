using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RankingButton : MonoBehaviour
{
    [SerializeField] private Button rankingButton;

    void Start()
    {
        rankingButton.onClick.AddListener(showRanking);
    }

    public void showRanking()
    {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(100);
        //SceneManager.LoadScene("Ranking", LoadSceneMode.Additive);
    }
}
