using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx.Async;

public class ResultButtonManager : MonoBehaviour
{
    [SerializeField] private Button GotoTitleButton;
    [SerializeField] private Button RankingButton;

    private SceneLoader sceneLoader;

    void Start()
    {
        sceneLoader = GameObject.Find("FadeCanvas").GetComponent<SceneLoader>();

        GotoTitleButton.onClick.AddListener(LoadTitle);
        RankingButton.onClick.AddListener(ShowRanking);
    }

    private void LoadTitle()
    {
        //SceneManager.LoadScene("Title");
        sceneLoader.LoadScene(SceneLoader.Scenes.Title).Forget();
    }

    private void ShowRanking()
    {
        Debug.Log("Ranking");
    }
}
