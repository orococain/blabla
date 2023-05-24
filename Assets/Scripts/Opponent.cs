using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Opponent : MonoBehaviour
{
    public GameObject loadingScreen;
    public int scene;
    public void LoadScene(int sceneName)
    {
        StartCoroutine(LoadSceneAsync());
        Time.timeScale = 1f;
    }

    private IEnumerator LoadSceneAsync()
    {
        loadingScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(5);
        AsyncOperation async = Application.LoadLevelAsync(scene);
        async.allowSceneActivation = true;
        while (!async.isDone)
        {
            yield return null;
        }

        Time.timeScale = 1f;
    }
}
