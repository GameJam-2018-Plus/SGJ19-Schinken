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
    public float speed=20;
    [Range(0,1000)]
    public float jumpForce=1000;
    private float timeCounter=0;

    // Start is called before the first frame update
    void Start()
    {
        rb2d=GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float move=Input.GetAxis("Horizontal")*speed;
        float jump=0;
        if(Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y-0.51f), Vector2.down, 0.01f)){
            jump=Input.GetAxis("Vertical")*jumpForce;
        }
        Vector2 forces=new Vector2 (move, jump);
        rb2d.AddForce(forces);
    }
}