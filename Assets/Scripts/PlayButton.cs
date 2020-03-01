using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx.Async;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip button;

    private SceneLoader sceneLoader;

    void Start()
    {
        playButton.onClick.AddListener(onClickPlay);
        sceneLoader = GameObject.Find("FadeCanvas").GetComponent<SceneLoader>();
    }

    private void onClickPlay()
    {
        audio.PlayOneShot(button);
        //SceneManager.LoadScene("Kaiwa");
        sceneLoader.LoadScene(SceneLoader.Scenes.Kaiwa).Forget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
