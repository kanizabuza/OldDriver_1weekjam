using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private Button playButton;
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(onClickPlay);
    }

    private void onClickPlay()
    {
        SceneManager.LoadScene("Kaiwa");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
