using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _CameraFollow : MonoBehaviour {

    Transform lookAt;
    Vector3 startOffset;

    // Use this for initialization

    public void _FindPlayer()
    {
        lookAt = GameObject.Find("Player").transform;
        startOffset = transform.position - lookAt.position;
    }

    public void _ResetPosition()
    {
        this.transform.position = new Vector3(0, 0, 0);
        lookAt = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (lookAt != null)
        {
            transform.position = lookAt.position + startOffset;
        }
    }
}
