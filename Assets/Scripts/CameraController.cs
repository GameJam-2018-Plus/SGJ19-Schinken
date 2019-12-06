using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float damping;

    void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.transform.position.x, damping), transform.position.y, transform.position.z);
    }
}