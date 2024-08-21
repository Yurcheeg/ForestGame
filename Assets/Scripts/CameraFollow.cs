using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform player;
    private float playerOffset;

    private void Start()
    {
        playerOffset = cameraTransform.position.x - player.position.x;
    }
    void Update()
    {
        cameraTransform.transform.position = new Vector3(player.position.x + playerOffset, cameraTransform.position.y, cameraTransform.position.z);
    }
}
