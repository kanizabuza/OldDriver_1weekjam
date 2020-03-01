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
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip button;

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
        audio.PlayOneShot(button);
        sceneLoader.LoadScene(SceneLoader.Scenes.Title).Forget();
    }

    private void ShowRanking()
    {
        audio.PlayOneShot(button);
        Debug.Log("Ranking");
    }
}
