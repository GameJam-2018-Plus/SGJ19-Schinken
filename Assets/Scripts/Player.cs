using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int lives=3;
    private Rigidbody2D rb2d;
    private enum State
    {
        unarmed,
        armed,
        fridge
    }
    [Range(0,100)]
    public float speed;
    [Range(0,100)]
    public float downwardForce=1;
    private float timeCounter=0;

    // Start is called before the first frame update
    void Start()
    {
        rb2d=GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float move=Input.GetAxis("Horizontal")*speed;
        float jump=Input.GetAxis("Vertical")*downwardForce;
        Vector2 forces=new Vector2 (move, jump);
        rb2d.AddForce(forces);
    }
}