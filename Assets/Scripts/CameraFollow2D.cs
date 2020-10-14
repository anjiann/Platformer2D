using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float timeOffset;

    [SerializeField] private Vector2 posOffset;

    private Vector3 velocity;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = player.transform.position;
        endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z = -10;

        //transform.position = Vector3.Lerp(startPos, endPos, timeOffset * Time.deltaTime);
        //transform.position = new Vector3(playerPos.x, playerPos.y, -10);

        transform.position = Vector3.SmoothDamp(startPos, endPos, ref velocity, timeOffset);
    }
}
