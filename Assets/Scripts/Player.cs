using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public Tilemap map;
    public TileBase destructible, fire;

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

    void Kill()
    {
        transform.position = Vector3.zero;
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerState = State.unarmed;
    }

    void FixedUpdate()
    {
        if (transform.position.y < -5)
            Kill();

        Vector3Int pos = Vector3Int.FloorToInt(transform.position + Vector3.Scale(new Vector3(-0.49F, -0.6F, 0), transform.localScale));
        Vector3Int size = new Vector3Int(1 + Mathf.FloorToInt(transform.position.x + 0.49F * transform.localScale.x) - Mathf.FloorToInt(transform.position.x - 0.49F * transform.localScale.x), 1, 1);
        TileBase[] floorTiles = map.GetTilesBlock(new BoundsInt(pos, size));

        move();
        jump();

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

            foreach (TileBase b in floorTiles)
                if (b == fire)
                {
                    Kill();
                    break;
                }
        }
        else
        {
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(Vector2.down * (-Physics2D.gravity) * fridgeGrav);

            for (int i = 0; i < floorTiles.Length; ++i)
            {
               if (floorTiles[i] == destructible)
                    map.SetTile(pos + new Vector3Int(i, 0, 0), null);
            }
        }
    }
}