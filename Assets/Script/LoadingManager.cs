using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private GameObject bgPanel;
    private float count = 0;
    private bool loadScene = false;

    // Start is called before the first frame update
    void Awake()
    {
        AudioListener.volume = 1;
        AudioManager.instance.MusicAudioSource.Stop();
        Time.timeScale = 1;
    }

    void Update()
    {
        count += Time.deltaTime;

        if(count >= 3 && !loadScene)
        {
            StartCoroutine(FromLoadingScene());
            loadScene = true;
        }
     
    }
    // Update is called once per frame
    IEnumerator FromLoadingScene()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(GlobalVariables.sceneToLoad, LoadSceneMode.Additive);
        bool sceneLoaded = false;
        load.allowSceneActivation = false;
        

        var bgPanelColor = bgPanel.GetComponent<Image>().color;
        //load.allowSceneActivation = false;

        var eventSystem = GameObject.Find("EventSystem");
        var loadingText = GameObject.Find("LoadingText");

        while (load.progress<0.9f)
        {
            yield return new WaitForSecondsRealtime(0.3f);
            Debug.Log(load.progress);
        }

        if (load.progress >= 0.9f)
        {
            Destroy(eventSystem);
            load.allowSceneActivation = true;
            sceneLoaded = true;
            yield return new WaitForSecondsRealtime(0.5f);
        }
        Time.timeScale = 0f;

        if (sceneLoaded)
        {
            loadingText.SetActive(false);
            while (bgPanelColor.a > 0) // Fade-out loop
            {
                bgPanelColor.a -= 0.1f;
                bgPanel.GetComponent<Image>().color = bgPanelColor;
                yield return new WaitForSecondsRealtime(0.1f);
            }
            Time.timeScale = 1f;
            AudioManager.instance.StartMusic(AudioManager.instance.allMusic[0]);
            SceneManager.UnloadSceneAsync(3); // 3 == this scene number.
            yield return null;
        }
    }
}
