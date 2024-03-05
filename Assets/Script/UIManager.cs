using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private bool paused = false;
    public bool defeat;
    [SerializeField] private GameObject _pausePanel;
    public GameObject defeatPanel;
    [SerializeField] private GameObject[] defeatPanelElements;
    [SerializeField] private GameObject[] _textLabels;
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private GameObject[] lifesUI;
    
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //_textLabels[0].GetComponent<TMP_Text>().text = Player.instance.lifes.ToString();
        defeat = false;
        paused = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !defeat) // Pause menu.
        {
            if (!paused)
            {
                Time.timeScale = 0f;
                paused = true;
                _pausePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                paused = false;
                _pausePanel.SetActive(false);
            }
        }
    }
    public void Defeat() // Once the player lifes = 0
    {
        defeat = true;
        paused = true;
        TMP_Text scoreDoneGO = defeatPanelElements[4].transform.GetChild(1).GetComponent<TMP_Text>();
        TMP_Text moneyCollectedGO = defeatPanelElements[5].transform.GetChild(1).GetComponent<TMP_Text>();

        gameTimeDone();
        scoreDoneGO.text = GlobalVariables.gameScore.ToString();
        moneyCollectedGO.text = Player.instance.money.ToString();

        foreach (GameObject textLabel in _textLabels)
        {
            textLabel.SetActive(false);
        }
        defeatPanel.SetActive(true);
        Time.timeScale = 0f;
        StartCoroutine(DefeatMenu());
    }
    public void LifeUI()
    {
        lifesUI[Player.instance.lifes].SetActive(false);
    }

    public void ScoreUI()
    {
        _textLabels[1].GetComponent<TMP_Text>().text = GlobalVariables.gameScore.ToString();
    }

    public void PlayAgain()
    {
        GlobalVariables.sceneToLoad = 1;
        StartCoroutine(GlobalVariables.toLoadingScene(fadePanel));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        GlobalVariables.sceneToLoad = 0;
        StartCoroutine(GlobalVariables.toLoadingScene(fadePanel));
    }

    public void gameTimeDone()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(GameManager.instance.gameTime);
        int hours = timeSpan.Hours;
        int minutes = timeSpan.Minutes;
        int seconds = timeSpan.Seconds;
        TMP_Text timePlayedGO = defeatPanelElements[3].transform.GetChild(1).GetComponent<TMP_Text>();

        timePlayedGO.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
        
    }

    IEnumerator DefeatMenu() // Defeat menu coroutine. UI elements appear one by one
    {
        foreach (GameObject UI in defeatPanelElements)
        {
            UI.SetActive(true);
            yield return new WaitForSecondsRealtime(0.2f); // 0.2s delay between element
        }
    }

    
}