using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoadSystem
{
    static bool isAbleToLoadScene = true;

    private static void OnLoadedScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        //씬로드 이벤트 실행 시점은 Awake보다 느림 최소 start에서 실행
        SceneManager.sceneLoaded -= OnLoadedScene;

        //열어준 씬에 포커스를 잡아줌
        SceneManager.SetActiveScene(scene);

        //로드 완료시 진행
        isAbleToLoadScene = true;
    }

    private static IEnumerator AsynLoadScene(string sceneName, LoadSceneMode loadSceneMode)
    {
        if (isAbleToLoadScene == false)
        {
            yield break;
        }

        isAbleToLoadScene = false;

        SceneManager.sceneLoaded += OnLoadedScene;
        var process = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
        yield return new WaitUntil(() => process.isDone);
    }



    public static void LoadScene(string sceneName, LoadSceneMode loadSceneMode)
    {
        // 외부에서 코루틴 호출하게 하지 말고
        // 내부에서 래핑한걸 실행하게 만들자
        GameManager.Instance.StartCoroutine(AsynLoadScene(sceneName, loadSceneMode));
    }

}
