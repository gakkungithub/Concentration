using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameSceneUtil : SingletonMonoBehaviour<GameSceneUtil>
{
    string currentSceneName = string.Empty;
    string nextSceneName = string.Empty;
    /// <summary>
    /// ÉVÅ[ÉìÇåƒÇ—èoÇ∑
    /// </summary>
    /// <param name="sceneName"></param>
    public void SingleSceneTransration(string sceneName, UnityAction action = null)
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(LoadedSceneInvoke(sceneName, action));
    }

    IEnumerator LoadedSceneInvoke(string sceneName, UnityAction action = null)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name != currentSceneName);
        if (action != null)
        {
            action.Invoke();
        }
    }

    public GameObject[] NextSceneRootGetGameObjects
    {
        get
        {
            GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            return rootGameObjects;
        }
    }
}
