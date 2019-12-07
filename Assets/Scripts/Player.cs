using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private int lives = 3;
    private Transform trans;
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
    private float fridgeTimeCounter = 0;
    [Range(0, 10)]
    public float maxFridgeTime=10;
    [Range(0, 10)]
    public float maxSchinkenTime=10;
    private float schinkenTimeCounter=0;
    [Range(0, 100)]
    public float fridgeGrav = 70;
    private float startPosX, startPosY;

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

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag.Equals("Enemy"))
        {
            Reset();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerState = State.unarmed;
        trans=GetComponent<Transform>();
        startPosX=trans.position.x;
        startPosY=trans.position.y;
    }

    void Reset()
    {
        lives--;
        if(lives>0)
        {
            rb2d.velocity=Vector2.zero;
            playerState=State.unarmed;
            trans.position=new Vector2(startPosX, startPosY);
        }
        else{
            SceneManager.LoadScene(2);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fridge"))
        {
            if (playerState != State.fridge)
            {
                playerState = State.fridge;                
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(Vector2.down * (-Physics2D.gravity) * fridgeGrav);
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
            fridgeTimeCounter+=Time.deltaTime;
            if(fridgeTimeCounter>maxFridgeTime)
            {
                Reset();
            }
        }
    }
}