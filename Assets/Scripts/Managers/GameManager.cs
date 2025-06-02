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
    [SerializeField] CharacterStats playerStats;
    [SerializeField] List<Perk> playerPerks = new();

    private GameObject player;
    private IStats playerStatManager;
    private PerkManager playerPerkManager;

    void Start()
    {
        SpawnPlayer();

        gameTimeManager = GameTimeManager.Instance;
        gameTimeManager.StartTimer();

        InitializePerks();
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public IStats GetPlayerStatManager()
    {
        return playerStatManager;
    }
    // do in own player spawner script
    void SpawnPlayer()
    {
        player = Instantiate(playerPrefab, playerSpawn.position, Quaternion.identity);

        playerStatManager = player.GetComponent<IStats>(); // Check if has, throw error if not
        playerPerkManager = player.GetComponent<PerkManager>();

        InitializePlayerStats();
    }

    void InitializePlayerStats()
    {
        playerStatManager.InitializeCharacterStats(playerStats);
    }

    void InitializePerks()
    {
        foreach (Perk perk in playerPerks)
        {
            playerPerkManager.GainPerk(perk, false);
        }
    }
}
