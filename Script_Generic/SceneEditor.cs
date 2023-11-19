using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using My;

/// <summary>
/// 各シーンに一つのみ配置<br/>
/// シーンを跨ぐと削除
/// </summary>
public class SceneEditor : MonoBehaviour
{
    [field: SerializeField] public bool debugMode { get; private set; }
    [SerializeField] private Instancer canvas;
    [SerializeField] private FadeInstancer fadeObj;

    protected virtual void Start()
    {
        SceneCheck();
        canvas.Initialize();
        canvas.Instance();
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneCheck();
    }

    protected virtual void Update()
    {
        canvas.Update();
    }


    /// <summary>
    /// 現在のシーンを取得し、Scenes In Buildに含まれていなければデバッグモードに移行する
    /// </summary>
    private void SceneCheck()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (IsSceneInBuildSettings(currentSceneName) == false)
        {
            InDebugMode();
        }
    }

    private bool IsSceneInBuildSettings(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneName == name)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// メッセージを表示し、デバッグモードに移行する
    /// </summary>
    private void InDebugMode()
    {
        if (debugMode == false)
        {


            debugMode = true;

            Debug.LogWarning("例外シーン");
        }
    }

    public T EditorGet<T>()
    {
        return GameObject.FindWithTag("SceneEditor").GetComponent<T>();
    }
}