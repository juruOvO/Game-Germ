using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//场景控制器管理器，用于加载场景，切换场景，场景淡入淡出
public class SceneControllerManager : SingletonMonobehaviour<SceneControllerManager>
{
    [SerializeField] private float fadeDuration = 2.0f;
    [SerializeField] private CanvasGroup faderCanvasGroup = null;
    public Image faderImage;
    public SceneName startingScene;
    private bool isFading = false;

    public AsyncOperation operation;

    IEnumerator Start()
    {
        EventsNotifier.Instance.OnClickEvent_SceneChange += FadeAndLoadScene;

        faderImage.color = new Color(0f, 0f, 0f, 1f);

        faderCanvasGroup.alpha = 1f;

        yield return StartCoroutine(LoadSceneAndSetActive(startingScene.ToString()));

        EventsNotifier.Instance.CallAfterSceneLoadEvent();

        StartCoroutine(Fade(0f));
    }

    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        yield return operation;

        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

        SceneManager.SetActiveScene(newlyLoadedScene);
    }

    public void FadeAndLoadScene(string sceneName)
    {
        if(!isFading)
        {
            StartCoroutine(FadeAndSwitchScene(sceneName));
        }
    }

    public void FadeAndLoadSceneWithoutWait(string sceneName)
    {
        StartCoroutine(FadeAndSwitchScene(sceneName));
    }

    private IEnumerator FadeAndSwitchScene(string sceneName)
    {
        EventsNotifier.Instance.CallBeforeFadeOutEvent();

        yield return StartCoroutine(Fade(1f));

        EventsNotifier.Instance.CallBeforeSceneUnloadEvent();

        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

        EventsNotifier.Instance.CallAfterSceneLoadEvent();

        yield return StartCoroutine(Fade(0f));

        EventsNotifier.Instance.CallAfterFadeInEvent();
    }

    private IEnumerator Fade(float finalAlpah)
    {
        Debug.Log("Fade");
        isFading = true;

        faderCanvasGroup.blocksRaycasts = true;

        float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpah) / fadeDuration;

        while (!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpah))
        {
            faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpah, fadeSpeed*Time.deltaTime);

            yield return null; 
        }
        
        isFading = false;

        faderCanvasGroup.blocksRaycasts = false;
        Debug.Log("FadeEnd");
    }
}
