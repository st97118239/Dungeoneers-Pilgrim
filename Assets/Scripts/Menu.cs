using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool lockCursor = false;

    private Player player;
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Player>();
        }
        
        ResetCursor();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.activeSelf)
            {
                PauseMenu();
            }
            else
            {
                ContinueButton();
            }
        }
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

    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
        lockCursor = false;
        ResetCursor();
        player.isPaused = true;
    }

    public void ContinueButton()
    {
        pauseMenu.SetActive(false);
        lockCursor = true;
        ResetCursor();
        player.isPaused = false;
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

    public void MainMenuButton()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
