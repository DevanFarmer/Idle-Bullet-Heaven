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

    [SerializeField] Transform playerSpawn;
    [SerializeField] GameObject playerPrefab;

    private GameObject player;
    private StatManager playerStatManager;

    void Start()
    {
        player = Instantiate(playerPrefab, playerSpawn.position, Quaternion.identity);

        playerStatManager = player.GetComponent<StatManager>(); // Check if has, throw error if not
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public StatManager GetPlayerStatManager()
    {
        return playerStatManager;
    }
}
