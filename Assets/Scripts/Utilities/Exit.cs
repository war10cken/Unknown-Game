using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] private Canvas _ui;
    [SerializeField] private Canvas _settings;

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void AbortQuit()
    {
        gameObject.SetActive(false);
        _ui.gameObject.SetActive(true);
    }

    public void ToSettings()
    {
        gameObject.SetActive(false);
        _settings.gameObject.SetActive(true);
    }
    
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
