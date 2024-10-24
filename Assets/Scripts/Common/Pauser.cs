using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Pauser : MonoBehaviour
{
    public event UnityAction<bool> PauseStateChange;

    private bool isPause;
    public bool Ispause => isPause;

    private void Awake()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoader;
    }

    private void SceneManager_sceneLoader(Scene arg0, LoadSceneMode arg1)
    {
        UnPause();
    }

    public void ChangePauseState()
    {
        if (isPause == true)
            UnPause();
        else
            Pause();
    }

    public void Pause()
    {
        if (isPause == true) return;
        
        Time.timeScale = 0;
        isPause = true;
        PauseStateChange?.Invoke(isPause);
    }
    public void UnPause()
    {
        if (isPause == false) return;

        Time.timeScale = 1;
        isPause = false;
        PauseStateChange?.Invoke(isPause);
    }
}
