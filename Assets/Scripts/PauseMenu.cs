using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool pause = false;
    public GameObject pauseUI;
    public EndTrigger endTrigger;

    private void Start()
    {
        endTrigger = endTrigger.GetComponent<EndTrigger>();
        Resume();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape)) && (!endTrigger.finish))
        {
            if (pause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        pause = false;
    }

    private void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        pause = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        // Debug.Log("Quit");
        Application.Quit();
    }
}