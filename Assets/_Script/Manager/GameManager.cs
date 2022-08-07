using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //[SerializeField] FactoryManager factoryManager;
    //[SerializeField] UIManager uiManager;

    private void Awake()
    {
        GameResourcesManager.Instance.Initialize();
        FactoryManager.Instance.Initialize();
        UIManager.Instance.Initialize();
    }

    private void Start()
    {
        LoadScene("Main", LoadSceneMode.Additive);
    }

    //씬로드
    public void LoadScene(string sceneName, LoadSceneMode loadSceneMode)
    {
        SceneLoadSystem.LoadScene(sceneName, loadSceneMode);
    }

    //오브젝트 풀(팩토리)

}
