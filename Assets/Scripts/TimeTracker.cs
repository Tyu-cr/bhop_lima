using TMPro;
using UnityEngine;

public class TimeTracker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;

    private float _startTime;
    public float elapsedTime;

    public void Start()
    {
        _startTime = Time.time;
    }

    private void Update()
    {
        elapsedTime = (Time.time - _startTime) * 1000;
        var minutes = (int)(elapsedTime / 60000);
        var seconds = (int)((elapsedTime / 1000) % 60);
        var milliseconds = (int)(elapsedTime % 1000);
        timeText.text = string.Format("Time: {0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}