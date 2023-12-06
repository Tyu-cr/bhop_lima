using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PB : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pb;

    public void Start()
    {
        var best = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name);
        if (best == 0)
        {
            pb.text = "PB: 00:00:000";
        }
        else
        {
            var minutes = (int)(best / 60000);
            var seconds = (int)((best / 1000) % 60);
            var milliseconds = (int)(best % 1000);
            pb.text = string.Format("PB: {0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }
    }
}