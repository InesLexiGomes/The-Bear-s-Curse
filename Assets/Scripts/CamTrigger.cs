using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] private Vector3 position;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        camera.transform.position = position;
    }
}
