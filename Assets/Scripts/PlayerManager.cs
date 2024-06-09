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
    public bool hasKey = false;

    // Bear form variables
    [SerializeField] private int maxCooldownTime;
    private int cooldown;

    void Awake()
    {
        // If there is no other player manager this one will be used and won't be destroyed on load
        if (_instance == null)
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);

            // Subscribe to the scene loaded event
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        // If there already is one this object is destroyed
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the scene loaded event to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If the input is pressed and the cooldown is above 0 the player switches between the 2 forms
        if (Input.GetButtonDown("Transform") && (cooldown <= 0))
        {
            SwitchPlayerBear();
        }
        // Otherwise decrease cooldown
        else if (cooldown > 0) cooldown--;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        hasKey = false;
        // Re-instantiate the player at the checkpoint position
        Instantiate(playerPrefab, playerCoordsCheckpoint, Quaternion.identity);
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
            Destroy(player.gameObject);
        }
        // If there is a bear it will instantiate the playerPrefab on the bear's position
        if (bear != null)
        {
            Instantiate(playerPrefab, bear.gameObject.transform.position, bear.gameObject.transform.rotation);
            Destroy(bear.gameObject);
        }

        // Reset Cooldown
        cooldown = maxCooldownTime;
    }

    // When shot decrease arrowcount by 1
    public void UseArrow()
    {
        if (ArrowCount > 0) ArrowCount--;
        else ArrowCount = 0;
    }

    // When picked up increase arrowcount by 1
    public void PickUpArrow()
    {
        ArrowCount++;
    }

    // Sets the ammount of arrows that will be loaded at checkpoint
    public void SetArrowsAtCheckpoint(int ammount)
    {
        arrowsAtCheckpoint = ammount;
    }

    // Sets coordinates that will be loaded at checkpoint
    public void SetPlayerCoordsAtCheckpoint(Vector2 coords)
    {
        playerCoordsCheckpoint = coords;
    }

    public void CheckpointLoad()
    {
        ArrowCount = arrowsAtCheckpoint;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
