using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance = null;
    public static PlayerManager Instance => _instance;

    // The Prefabs that will be instantiated
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject bearPrefab;

    // Checkpoint data
    [SerializeField]
    private int arrowsAtCheckpoint;
    [SerializeField]
    private Vector2 playerCoordsCheckpoint;

    // Level transition and player usage data
    public int ArrowCount { get; private set; }

    void Awake()
    {
        // If there is no other player manager this one will be used and won't be destroyed on load
        if (_instance == null)
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);
            Instantiate(playerPrefab, playerCoordsCheckpoint, Quaternion.Euler(0, 0, 0));
        }
        // If there already is one this object is destroyed
        else
        {
            Instantiate(playerPrefab, playerCoordsCheckpoint, Quaternion.Euler(0, 0, 0));
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ArrowCount = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SwitchPlayerBear()
    {
        // Looks in the scene to find if there is a gameObject with the Player or Bear scripts
        Player player = FindObjectOfType<Player>();
        Bear bear = FindObjectOfType<Bear>();

        // If there is a player it will instantiate the bearPrefab on the player's position
        if (player != null)
        {
            Instantiate(bearPrefab, player.gameObject.transform.position, player.gameObject.transform.rotation);
            Destroy(player);
        }
        // If there is a bear it will instantiate the playerPrefab on the bear's position
        else if (bear != null)
        {
            Instantiate(playerPrefab, bear.gameObject.transform.position, bear.gameObject.transform.rotation);
            Destroy(bear);
        }
    }

    public void UseArrow()
    {
        if (ArrowCount > 0) ArrowCount--;
        else ArrowCount = 0;
    }

    public void PickUpArrow()
    {
        ArrowCount++;
    }

    public void SetArrowsAtCheckpoint(int ammount)
    {
        arrowsAtCheckpoint = ammount;
    }

    public void SetPlayerCoordsAtCheckpoint(Vector2 coords)
    {
        playerCoordsCheckpoint = coords;
    }
}
