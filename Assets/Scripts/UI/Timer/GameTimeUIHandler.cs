using TMPro;
using UnityEngine;

public class GameTimeUIHandler : MonoBehaviour
{
    GameTimeManager gameTimeManager;

    [SerializeField] TextMeshProUGUI timerTextUI;

    // more performant than creating new variables every frame
    float timeElapsed;
    int minutes, seconds;

    void Start()
    {
        gameTimeManager = GameTimeManager.Instance;
        timeElapsed = 0f;
        minutes = 0;
        seconds = 0;
    }

    void Update()
    {
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        timeElapsed = gameTimeManager.GetTimeElapsed();

        minutes = Mathf.FloorToInt(timeElapsed / 60);
        seconds = Mathf.FloorToInt(timeElapsed % 60);

        timerTextUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
