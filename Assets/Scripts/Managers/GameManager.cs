using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance = null;

    public static GameManager Instance { get { return instance; } }

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

    GameTimeManager gameTimeManager;

    [SerializeField] Transform playerSpawn;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] List<Perk> playerPerks = new();

    private GameObject player;
    private StatManager playerStatManager;
    private PerkManager playerPerkManager;

    void Start()
    {
        player = Instantiate(playerPrefab, playerSpawn.position, Quaternion.identity);

        playerStatManager = player.GetComponent<StatManager>(); // Check if has, throw error if not
        playerPerkManager = player.GetComponent<PerkManager>();

        gameTimeManager = GameTimeManager.Instance;
        gameTimeManager.StartTimer();

        InitializePerks();
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public StatManager GetPlayerStatManager()
    {
        return playerStatManager;
    }

    void InitializePerks()
    {
        foreach (Perk perk in playerPerks)
        {
            playerPerkManager.GainPerk(perk);
        }
    }
}
