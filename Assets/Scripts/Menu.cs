using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public bool lockCursor = false;
    void Start()
    {
        ResetCursor();
    }

    public void ResetCursor()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void PlayButton()
    {
        SceneManager.LoadSceneAsync("Main");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
