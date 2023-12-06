using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("LevelsMenu");
    }

    public void Quit()
    {
        // Debug.Log("Quit");
        Application.Quit();
    }
}