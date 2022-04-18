using TMPro;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text _fps;
    [SerializeField] private Canvas _ui;
    [SerializeField] private Canvas _exitMenu;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0;
            _ui.gameObject.SetActive(false);
            _exitMenu.gameObject.SetActive(true);
        }
        
        _fps.text = "FPS " + (int)(1.0f / Time.smoothDeltaTime);
    }
}
