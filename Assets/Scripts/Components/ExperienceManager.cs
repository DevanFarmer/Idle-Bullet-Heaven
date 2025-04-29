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

    [SerializeField] float totalExperience;
    [SerializeField] float nextLevelUp;

    StatManager statManager;

    private void Start()
    {
        statManager = StatManager.Instance;
    }

    public void GainExp(float exp)
    {
        totalExperience += exp;
        if (totalExperience >= nextLevelUp)
        {
            LevelUp(); // Might be a problem if, somehow, gain exp before getting new nextLevelUp value. Would level up twice.
        }
    }

    void LevelUp()
    {
        // Call StatManager LevelUp
        statManager.LevelUp();

        // Set nextLevelUp amount
    }
}
