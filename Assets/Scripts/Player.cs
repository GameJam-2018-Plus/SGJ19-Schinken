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
    [Range(0,20)]
    public float speed=15;
    [Range(0,10)]
    public float jumpForce=7;
    private float timeCounter=0;

    void move(){
        float move=Input.GetAxis("Horizontal")*speed;
        Vector2 speedVec=new Vector2(move, 0);
        rb2d.AddForce(speedVec);
    }

    void jump(){
        float jump=0;
        if(Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y-0.51f), Vector2.down, 0.01f)&&Input.GetKeyDown("space")){
            jump=jumpForce;
        }
        Vector2 jumpVec=new Vector2 (0, jump);
        rb2d.AddForce(jumpVec, ForceMode2D.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d=GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        move();
        jump();
    }
}