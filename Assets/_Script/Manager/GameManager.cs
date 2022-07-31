using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //싱글턴
    public static GameManager Instance;
    [SerializeField] FactoryManager factoryManager;
    private void Awake()
    {
        Instance = this;
        factoryManager.Initalize();
    }

    private void Start()
    {
        LoadScene("Main", LoadSceneMode.Additive);
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    //씬로드
    public void LoadScene(string sceneName, LoadSceneMode loadSceneMode)
    {
        SceneLoadSystem.LoadScene(sceneName, loadSceneMode);
    }

    //오브젝트 풀(팩토리)

}
