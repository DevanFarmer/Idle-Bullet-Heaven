using UnityEngine;

// this manager is to handle pausing decay for all minion, might want to rename to minion decay manager instead
public class MinionsManager : MonoBehaviour
{
    #region Singleton
    private static MinionsManager instance = null;

    public static MinionsManager Instance { get { return instance; } }

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

    [SerializeField] public bool minionsDecay { get; private set; }
    private float resetDecayTimer;


    private void Awake()
    {
        HandleSingleton();
    }

    private void Update()
    {
        HandleResetMinionDecayTimer();
    }

    public bool GetMinionsDecay()
    {
        return minionsDecay;
    }

    // when a perk actives, it sends the time. If the time is higher it gets set else "ignored"
    public void PauseMinionsDecay(float time)
    {
        if (resetDecayTimer < time)
        {
            resetDecayTimer = time;
            minionsDecay = false;
        }
    }

    void HandleResetMinionDecayTimer()
    {
        while (resetDecayTimer > 0f)
        {
            resetDecayTimer -= Time.deltaTime;

            if (resetDecayTimer <= 0)
            {
                resetDecayTimer = 0f;
                minionsDecay = true;
            }
        }
    }
}
