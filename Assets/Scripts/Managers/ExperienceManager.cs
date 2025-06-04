using EventBusEventData;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    #region Singleton
    private static ExperienceManager instance = null;

    public static ExperienceManager Instance { get { return instance; } }

    private void Awake()
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

    [SerializeField] int level;
    [SerializeField] int maxLevel;

    [SerializeField] float totalExperience;
    [SerializeField] float nextLevelUp;

    IStats playerStatManager; // get level up manager instead, or call handle level up here

    [SerializeField] List<LevelUpStats> levelUpStats = new();

    private void Start()
    {
        level = 0;
        maxLevel = levelUpStats.Count;
        playerStatManager = GameManager.Instance.GetPlayerStatManager();
    }

    bool notificationSent = false;
    public void GainExp(float exp)
    {
        if (level >= maxLevel)
        {
            if (!notificationSent) { Debug.Log("Player is max level! Cannot gain more exp!"); notificationSent = true; }
            return;
        }

        totalExperience += exp;
        if (totalExperience >= nextLevelUp)
        {
            LevelUp(); // Might be a problem if, somehow, gain exp before getting new nextLevelUp value. Would level up twice.
        }
    }

    void LevelUp()
    {
        // Call StatManager LevelUp
        playerStatManager.LevelUp(levelUpStats[level].LevelUpStatModifiers);
        level++;

        // Set nextLevelUp amount
        nextLevelUp *= 1.5f;

        EventBus.Publish(new LevelUpEvent(level));
    }
}
