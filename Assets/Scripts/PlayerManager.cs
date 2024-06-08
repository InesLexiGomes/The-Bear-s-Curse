using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Level transition and player usage data
    public int ArrowCount { get; private set; }

    void Awake()
    {
        // If there is no other player manager this one will be used and won't be destroyed on load
        if (_instance == null)
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);
        }
        // If there already is one this object is destroyed
        else
        {
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
        GameObject player = FindObjectOfType<Player>().gameObject;
        GameObject bear = FindObjectOfType<Bear>().gameObject;

        // If there is a player it will instantiate the bearPrefab on the player's position
        if (player != null)
        {
            Instantiate(bearPrefab, player.transform.position, player.transform.rotation);
            Destroy(player);
        }
        // If there is a bear it will instantiate the playerPrefab on the bear's position
        else if (bear != null)
        {
            Instantiate(playerPrefab, bear.transform.position, bear.transform.rotation);
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
}
