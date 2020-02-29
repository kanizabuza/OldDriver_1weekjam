using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private GameStateManager stateManager;

    void Start()
    {
        stateManager.CurrentState
            .FirstOrDefault(x => x == GameState.Finish)
            .Subscribe(_ => ShowResult());
    }

    /// <summary>
    /// リザルトを表示する
    /// </summary>
    private void ShowResult()
    {
        Debug.Log("GameOver");
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
