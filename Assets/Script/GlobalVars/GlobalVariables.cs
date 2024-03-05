using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class GlobalVariables
{
    public static int sceneToLoad;
    public static int gameScore=0;
    public static bool audioExists = false;

    public static IEnumerator toLoadingScene(GameObject fadePanel)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(3, LoadSceneMode.Single); // Starts UI Scene load.
        load.allowSceneActivation = false; // Do not let it actives when ready. 

        fadePanel.SetActive(true);
        var fadePanelColor = fadePanel.GetComponent<Image>().color;

        if (!load.isDone) // While not loaded completly
        {
            while (fadePanelColor.a < 1) // Fade-in while (Changes the alpha value).
            {
                fadePanelColor.a += 0.1f;
                fadePanel.GetComponent<Image>().color = fadePanelColor;
                yield return new WaitForSecondsRealtime(0.1f);
            }
        }
        load.allowSceneActivation = true;

    }
}
