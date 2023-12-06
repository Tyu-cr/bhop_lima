using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTrigger : MonoBehaviour
{
    public GameObject levelCompleteUI;
    public bool finish = false;
    public TimeTracker timeTracker;
    public PB pb;
    public GameObject newRecord;

    private void Start()
    {
        levelCompleteUI.SetActive(false);
        Time.timeScale = 1f;
        finish = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!(other.gameObject.CompareTag("Player"))) return;

        var best = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name);
        var time = timeTracker.elapsedTime;
        if ((time < best) || (best == 0))
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name, time);
            newRecord.SetActive(true);
        }
        else
        {
            newRecord.SetActive(false);
        }
        
        pb.Start();
        Time.timeScale = 0f;
        finish = true;
        levelCompleteUI.gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        finish = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        finish = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}