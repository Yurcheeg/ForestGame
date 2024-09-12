using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    Camera cam;
    [SerializeField]private float rotateSpeed;

    private void Awake()
    {
        cam = Camera.main;
    }
    public void Rotate()
    {
        StartCoroutine(RotateCamera());
    }

    private IEnumerator RotateCamera()
    {
        CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
        cameraFollow.SetFollow(false);

        Vector3 startPos = cam.transform.position;
        Vector3 endPos = new Vector3 (cam.transform.position.x - CameraFollow.playerOffset, cam.transform.position.y,cam.transform.position.z);
        float elapsedTime = 0f;
        float duration = 1f;

        while (elapsedTime < duration)
        {
            cam.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime);
            elapsedTime += Time.deltaTime * rotateSpeed;
            yield return null;
            if(cam.transform.position == endPos)
            {
                break;
            }
        }
        cameraFollow.ReverseOffset();

        cameraFollow.SetFollow(true);
        yield return null;
    }
}
