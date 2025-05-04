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

    [SerializeField] bool minionsDecay;

    private void Awake()
    {
        HandleSingleton();
    }

    public bool GetMinionsDecay()
    {
        return minionsDecay;
    }

    public void SetMinionsDecay(bool minionsDecay)
    {
        this.minionsDecay = minionsDecay;
    }
}
