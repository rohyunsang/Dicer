using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;
using Random = UnityEngine.Random;
using Fusion.Sockets;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance;

    private int _lastLevelIndex;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(transform.parent);
    }

    public void ResetLastLevelsIndex()
    {
        _lastLevelIndex = 0;
    }

    public void LoadNextLevel(NetworkRunner runner)
    {
        _lastLevelIndex = _lastLevelIndex + 1 >= SceneManager.sceneCountInBuildSettings ? 1 : _lastLevelIndex + 1;
        string scenePath = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(_lastLevelIndex));
        runner.LoadScene(scenePath);
    }
}
