using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform player;
    public static float playerOffset;
    private bool following = true;
    PlayerMovement playerMovement;

    private void Start()
    {
        UpdateOffset();
        playerMovement = player.GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if(following)
        {
            cameraTransform.transform.position = new Vector3(player.position.x + playerOffset, cameraTransform.position.y, cameraTransform.position.z);
        }
    }
    public void SetFollow(bool follow)
    {
        following = follow;
    }
    private void UpdateOffset()
    {
        playerOffset = (cameraTransform.position.x - player.position.x);
    }
    public void ReverseOffset()
    {
        playerOffset = -playerOffset;
    }
}
