using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Player object.
    public GameObject player;
    // Offset of camera.
    private Vector3 offset;
    // Getting starting offset.
    void Start()
    {
        offset = transform.position - player.transform.position;
    }
    // Making sure that camera folows player.
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }

}
