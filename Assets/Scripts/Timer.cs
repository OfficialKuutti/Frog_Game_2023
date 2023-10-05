using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI bestTimeText;
    public GameObject endMenuPanel;

    private float startTime;
    private float bestTime = -1f;  // Initialize with a default value
    private bool timerActive = false;

    void Start()
    {
        // Load best time from PlayerPrefs
        bestTime = PlayerPrefs.GetFloat("BestTime", -1f);  // Load the initialized default value
        if (bestTime >= 0f)
        {
            // If a valid best time is loaded, update the text
            UpdateBestTimeText(bestTime);
        }

        // Check if TextMeshProUGUI components are assigned
        if (timerText == null || bestTimeText == null)
        {
            Debug.LogError("Timer Text or Best Time Text not assigned. Please assign TextMeshProUGUI components.");
        }
    }

    void Update()
    {
        // Check if the player has moved on the X axis
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
        {
            if (!timerActive && !endMenuPanel.activeSelf)
            {
                // Player has moved, start the timer
                timerActive = true;
                startTime = Time.time;
                Debug.Log("Timer started at " + startTime);
            }
            else if (timerActive && endMenuPanel.activeSelf)
            {
                // Player has moved in endMenuPanel, stop the timer
                StopTimer();
            }
        }

        // Update the timer text
        if (timerActive)
        {
            float elapsedTime = Time.time - startTime;
            UpdateTimerText(elapsedTime);
        }
    }

    void StopTimer()
    {
        // Stop the timer only if it was started
        if (timerActive)
        {
            // Update the best time if the current time is shorter and valid
            if (Time.time - startTime < bestTime || bestTime < 0f)
            {
                bestTime = Time.time - startTime;
                PlayerPrefs.SetFloat("BestTime", bestTime);
                PlayerPrefs.Save();
                UpdateBestTimeText(bestTime);
            }

            // Reset the timer
            timerActive = false;
        }
    }

    void UpdateTimerText(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        int milliseconds = (int)((time * 1000) % 1000);

        string timerString = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);

        // Check if TextMeshProUGUI component is assigned
        if (timerText != null)
        {
            timerText.text = timerString;
        }
        else
        {
            Debug.LogError("Timer Text not assigned. Please assign a TextMeshProUGUI component.");
        }
    }

    void UpdateBestTimeText(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        int milliseconds = (int)((time * 1000) % 1000);

        string bestTimeString = string.Format("Best Time: {0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);

        // Check if TextMeshProUGUI component is assigned
        if (bestTimeText != null)
        {
            bestTimeText.text = bestTimeString;
        }
        else
        {
            Debug.LogError("Best Time Text not assigned. Please assign a TextMeshProUGUI component.");
        }
    }
}