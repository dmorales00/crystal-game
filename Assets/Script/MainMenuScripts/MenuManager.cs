using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
    private static int highestScore=0;
    private int currentMoney;
    private int score;
    [SerializeField] private GameObject[] leaderboardItems;
    [SerializeField] private GameObject[] pannels;
    private GameObject actualPanel;
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private Slider volSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TextMeshProUGUI volText;
    [SerializeField] private TextMeshProUGUI sfxText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TMP_Text langSelectText;
    private int actualLang=0;


    private void Awake()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume",1);
    }
    private IEnumerator Start()
    {
        currentMoney = PlayerPrefs.GetInt("Money",0);
        score = PlayerPrefs.GetInt("Score",0);
        Time.timeScale = 1f;

        if (highestScore < score)
        {
            highestScore = score;
        }

        yield return LocalizationSettings.InitializationOperation;

        if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[0])
        {
            langSelectText.text = "English";
            actualLang = 0;
        }
        else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[1])
        {
            langSelectText.text = "Español";
            actualLang = 1;
        }

        AudioManager.instance.SFXAudioSource.volume = PlayerPrefs.GetFloat("SFX",1);
        AudioManager.instance.MusicAudioSource.volume = PlayerPrefs.GetFloat("Music", 1);

        pannels[3].SetActive(true);
        volSlider.value = PlayerPrefs.GetFloat("Volume", 1) * 100;
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", 1) * 100;
        musicSlider.value = PlayerPrefs.GetFloat("Music", 1) * 100;

        yield return null;

        pannels[3].SetActive(false);
        AudioManager.instance.StartMusic(AudioManager.instance.allMusic[0]);
    }


    public void Play()
    {   
        AudioManager.instance.StartSFX(AudioManager.instance.allSFX[0]);
        GlobalVariables.sceneToLoad = 1;
        StartCoroutine(GlobalVariables.toLoadingScene(fadePanel));
    }

    public void Leaderboard()
    {
        AudioManager.instance.StartSFX(AudioManager.instance.allSFX[0]);
        pannels[0].SetActive(false);
        pannels[1].SetActive(true);
        actualPanel = pannels[1];
        leaderboardItems[0].GetComponent<TMP_Text>().text = "Highest Score: " + highestScore;
        leaderboardItems[1].GetComponent<TMP_Text>().text = "Last Score: " + score;
    }

    public void Shop()
    {
        AudioManager.instance.StartSFX(AudioManager.instance.allSFX[0]);
        pannels[0].SetActive(false);
        pannels[2].SetActive(true);
        actualPanel = pannels[2];

        pannels[2].transform.GetChild(0).GetComponent<TMP_Text>().text = "Money: " + currentMoney;
    }

    public void Back()
    {
        AudioManager.instance.StartSFX(AudioManager.instance.allSFX[0]);
        actualPanel.SetActive(false);
        pannels[0].SetActive(true);
    }

    public void Settings()
    {
        AudioManager.instance.StartSFX(AudioManager.instance.allSFX[0]);
        pannels[0].SetActive(false);
        pannels[3].SetActive(true);



        actualPanel = pannels[3];
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void PressedBttn()
    {
        AudioManager.instance.StartSFX(AudioManager.instance.allSFX[0]);
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
        highestScore = 0;
        SceneManager.LoadScene(0);
    }

    public void volumeChange()
    {
        AudioListener.volume = volSlider.value/100;
        volText.text = volSlider.value.ToString();

        PlayerPrefs.SetFloat("Volume", volSlider.value / 100);

    }

    public void sfxChange()
    {
        AudioManager.instance.SFXAudioSource.volume = sfxSlider.value / 100;
        sfxText.text = sfxSlider.value.ToString();

        PlayerPrefs.SetFloat("SFX", sfxSlider.value / 100);
    }

    public void musicChange()
    {
        AudioManager.instance.MusicAudioSource.volume = musicSlider.value / 100;
        musicText.text = musicSlider.value.ToString();

        PlayerPrefs.SetFloat("Music", musicSlider.value / 100);
    }

    public void ChangeLanguageText()
    {
        switch (actualLang)
        {
            case 0:
                langSelectText.text = "English";
                break;

            case 1:
                langSelectText.text = "Español";
                break;
        }
    }

    public void NextLanguage()
    {
        if(actualLang+1 > LocalizationSettings.AvailableLocales.Locales.Count-1)
        {
            actualLang = 0;
        }
        else
        {
            actualLang++;
        }
        ChangeLanguageText();
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[actualLang];

        volSlider.value = PlayerPrefs.GetFloat("Volume", 1) * 100;
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", 1) * 100;
        musicSlider.value = PlayerPrefs.GetFloat("Music", 1) * 100;
    }
    
    public void PrevLanguage()
    {
        if (actualLang-1 < 0)
        {
            actualLang = LocalizationSettings.AvailableLocales.Locales.Count -1;
        }
        else
        {
            actualLang--;
        }

        ChangeLanguageText();
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[actualLang];

        volSlider.value = PlayerPrefs.GetFloat("Volume", 1) * 100;
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", 1) * 100;
        musicSlider.value = PlayerPrefs.GetFloat("Music", 1) * 100;
    }
}
