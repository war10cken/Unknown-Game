using TMPro;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text _fps;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        _fps.text = "FPS " + (int)(1.0f / Time.smoothDeltaTime);
    }
}
