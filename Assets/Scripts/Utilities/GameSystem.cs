using UnityEngine;
using UnityEngine.UI;
public class GameSystem : MonoBehaviour
{
    public Text FPS;

    private void Start()
    {
        Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.Confined;

        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        FPS.text = "FPS " + (int)(1.0f / Time.smoothDeltaTime);
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }
}
