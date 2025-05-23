using UnityEngine;

public class GameTimeManager : MonoBehaviour
{
    #region Singleton
    private static GameTimeManager instance = null;

    public static GameTimeManager Instance { get { return instance; } }

    void HandleSingleton()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    bool timerEnabled;

    float timeElapsed;

    private void Awake()
    {
        HandleSingleton();
        timerEnabled = false;
        timeElapsed = 0f;
    }

    void Update()
    {
        if (!timerEnabled) return;

        timeElapsed += Time.deltaTime;
    }

    public void StartTimer()
    {
        timerEnabled = true;
    }

    public void PauseTimer()
    {
        timerEnabled = false;
    }

    public float GetTimeElapsed()
    {
        return timeElapsed;
    }
}
