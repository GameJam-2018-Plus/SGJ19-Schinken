using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public RectTransform hud;
    public float yRange = 3;
    public float damping;
    public float shakeDuration;
    public Vector2 offset;
    public GameObject barrier;

    private Camera cam;
    private float fov;
    private float shakeTime = -100;
    public float shakeStrength = 5;
    private bool freeze = false;
    private void Start()
    {
        cam = GetComponent<Camera>();
        fov = cam.fieldOfView;
    }

    public void Shake()
    {
        shakeTime = Time.time;
    }

    public void Cut()
    {
        transform.position = new Vector3(target.transform.position.x + offset.x, target.transform.position.y + offset.y, transform.position.z);
    }

    void Update()
    {
        //transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -8);
        if (!freeze)
        { transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.transform.position.x + offset.x, damping), transform.position.y + offset.y + Mathf.Max(0, target.transform.position.y - transform.position.y - yRange) - Mathf.Max(0, transform.position.y - target.transform.position.y - yRange), transform.position.z); }
        if (transform.position.x >= 172)
        {
            freeze = true;
            barrier.GetComponent<BoxCollider2D>().isTrigger = false;
            transform.position = new Vector3(172.3f, 4.68f, transform.position.z);
        }
        float x = (Time.time - shakeTime) / shakeDuration;
        if (x < 1)
        {
            float shake = Mathf.Sin(Mathf.Sqrt(x) * Mathf.PI) * (1 - x);
            cam.fieldOfView = fov - shakeStrength * shake;
            hud.localScale = Vector3.one * (1 + shake * 0.01F);
        }
        else
        {
            hud.localScale = Vector3.one;
            cam.fieldOfView = fov;
        }
    }


}