using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int lives = 3;
    private Rigidbody2D rb2d;
    private enum State
    {
        unarmed,
        armed,
        fridge
    }
    private State playerState;
    [Range(0, 20)]
    public float speed = 15;
    [Range(0, 10)]
    public float jumpForce = 7;
    private float timeCounter = 0;
    [Range(0, 100)]
    public float fridgeGrav = 70;

    void move()
    {
        float move = Input.GetAxis("Horizontal") * speed;
        Vector2 speedVec = new Vector2(move, 0);
        rb2d.AddForce(speedVec);
    }

    void jump()
    {
        float jump = 0;
        if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.51f), Vector2.down, 0.01f) && Input.GetButtonDown("Jump"))
        {
            jump = jumpForce;
        }
        Vector2 jumpVec = new Vector2(0, jump);
        rb2d.AddForce(jumpVec, ForceMode2D.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerState = State.unarmed;
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fridge"))
        {
            if (playerState != State.fridge)
            {
                playerState = State.fridge;
            }
            else
            {
                playerState = State.unarmed;
            }
        }
        else if (Input.GetButtonDown("Schinken"))
        {
            if (playerState == State.armed)
            {
                playerState = State.unarmed;
            }
            else if (playerState != State.fridge)
            {
                playerState = State.armed;
            }
        }
        if (playerState != State.fridge)
        {
            move();
            jump();
        }
        else
        {
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(Vector2.down * (-Physics2D.gravity) * fridgeGrav);
        }
    }
}