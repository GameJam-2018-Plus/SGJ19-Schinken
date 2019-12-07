using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float damping;
    public float shakeDuration;

    private Camera cam;
    private float fov;
    private float shakeTime = -100;
    public float shakeStrength = 5;

    private void Start()
    {
        cam = GetComponent<Camera>();
        fov = cam.fieldOfView;
    }

    public void Shake()
    {
        shakeTime = Time.time;
    }

    void Update()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.transform.position.x, damping), transform.position.y, transform.position.z);

        float x = (Time.time - shakeTime) / shakeDuration;
        if (x < 1)
            cam.fieldOfView = fov - shakeStrength * Mathf.Sin(Mathf.Sqrt(x) * Mathf.PI) * (1 - x);
        else
            cam.fieldOfView = fov;
    }
}