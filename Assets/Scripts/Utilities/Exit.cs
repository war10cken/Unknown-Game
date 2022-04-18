using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private Canvas _ui;
    
    public void Quit()
    {
        Application.Quit();
    }

    public void AbortQuit()
    {
        gameObject.SetActive(false);
        _ui.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
