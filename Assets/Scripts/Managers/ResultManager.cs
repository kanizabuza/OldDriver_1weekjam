using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private GameStateManager stateManager;
    [SerializeField] private GameObject resultPopup;
    [SerializeField] private Text scoreText;

    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = GameObject.Find("Manager").GetComponent<ScoreManager>();

        stateManager.CurrentState
            .FirstOrDefault(x => x == GameState.Finish)
            .Subscribe(_ => ShowResult()).AddTo(this);
    }

    /// <summary>
    /// リザルトを表示する
    /// </summary>
    private void ShowResult()
    {
        resultPopup.SetActive(true);
        scoreText.text = scoreManager.TotalScore.ToString();
    }

    /// <summary>
    /// タイトルに戻る
    /// </summary>
    /// <returns></returns>
    private async UniTask GoToTitleAsync()
    {
        await UniTask.Delay(2000);
        SceneManager.LoadScene("Title");
    }
}
