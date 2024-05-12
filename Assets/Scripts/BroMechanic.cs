using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroMechanic : MonoBehaviour
{

    [SerializeField]
    private int cooldown;
    [SerializeField]
    private int activeTime;
    [SerializeField]
    private int distance;
    [SerializeField]
    public bool Brothers { get; private set; }

    private float timeToUse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && timeToUse <= 0)
        {
            Brothers = true;
            timeToUse = cooldown;
        }
        else
        {
            timeToUse -= Time.deltaTime;
            if (cooldown-timeToUse >= activeTime)
            {
                Brothers = false;
            }
        }

    }
}
