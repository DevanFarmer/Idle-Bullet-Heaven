using UnityEngine;

public class MinionsManager : MonoBehaviour
{
    #region Singleton
    private MinionsManager instance = null;

    public static MinionsManager Instance { get { return Instance; } }

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
