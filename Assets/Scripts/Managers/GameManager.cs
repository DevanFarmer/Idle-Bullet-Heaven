using EventBusEventData;
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
    PlayerSpawner playerSpawner;

    [SerializeField] Transform playerSpawn;

    private GameObject player;
    private IStats playerStatManager;
    private IPerkManager playerPerkManager;

    private void OnEnable()
    {
        EventBus.Subscribe<LevelUpEvent>(OnLevelUpEvent);
        EventBus.Subscribe<PerkSelectedEvent>(OnPerkSelectedEvent);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<LevelUpEvent>(OnLevelUpEvent);
        EventBus.Unsubscribe<PerkSelectedEvent>(OnPerkSelectedEvent);
    }

    void Start()
    {
        playerSpawner = GetComponent<PlayerSpawner>(); // check if null

        SpawnPlayer();

        gameTimeManager = GameTimeManager.Instance;
        gameTimeManager.StartTimer();
        // on level up event pause and start again on select perk event
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public IStats GetPlayerStatManager()
    {
        return playerStatManager;
    }

    public IPerkManager GetPlayerPerkManager()
    {
        return playerPerkManager;
    }

    void SpawnPlayer()
    {
        player = playerSpawner.SpawnPlayer(playerSpawn.position);

        playerStatManager = player.GetComponent<IStats>(); // Check if has, throw error if not
        playerPerkManager = player.GetComponent<IPerkManager>();
    }

    void OnLevelUpEvent(LevelUpEvent e) 
    {
        gameTimeManager.PauseTimer();
        Time.timeScale = 0f;
    }

    void OnPerkSelectedEvent(PerkSelectedEvent e)
    {
        gameTimeManager.StartTimer();
        Time.timeScale = 1.0f;
    }
}
