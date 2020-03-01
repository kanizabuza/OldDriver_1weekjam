using System;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private Fade fade;

    public enum Scenes
    {
        Title,
        Kaiwa,
        Kaiwa2,
        Main
    }
    private Scenes currentScene = Scenes.Title;

    private void Start()
    {
        fade = GameObject.Find("FadeCanvas").GetComponent<Fade>();
    }

    public async UniTask LoadScene(Scenes scene)
    {
        await fade.FadeIn(1f, () => print("フェードイン完了"));
        switch (scene) {
            case Scenes.Title:
                currentScene = Scenes.Title;
                await SceneManager.LoadSceneAsync("Title");
                break;
            case Scenes.Kaiwa:
                currentScene = Scenes.Kaiwa;
                await SceneManager.LoadSceneAsync("Kaiwa");
                break;
            case Scenes.Kaiwa2:
                currentScene = Scenes.Kaiwa2;
                await SceneManager.LoadSceneAsync("Kaiwa2");
                break;
            case Scenes.Main:
                currentScene = Scenes.Main;
                await SceneManager.LoadSceneAsync("Main");
                break;
        }
        
        Observable.Timer(TimeSpan.FromSeconds(2f))
            .Where(_ => currentScene != Scenes.Main && currentScene != Scenes.Title)
            .Subscribe(_ => fade.FadeOut(0.5f, () => print("フェードアウト完了"))).AddTo(this);
    }
}
