using System;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    private void Awake()
    {
        StartCoroutine(GoogleSheetManager.Loader());
    }
}