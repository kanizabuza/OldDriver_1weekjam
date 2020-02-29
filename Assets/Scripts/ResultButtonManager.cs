using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultButtonManager : MonoBehaviour
{
    [SerializeField] private Button GotoTitleButton;
    [SerializeField] private Button RankingButton;
    // Start is called before the first frame update
    void Start()
    {
        GotoTitleButton.onClick.AddListener(LoadTitle);
        RankingButton.onClick.AddListener(ShowRanking);
    }

    private void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }

    private void ShowRanking()
    {
        Debug.Log("Ranking");
    }
}
