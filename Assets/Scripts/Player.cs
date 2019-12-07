using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Tilemap map;
    public TileBase destructible, fire;
    public ParticleSystem ground;
    public Text livesText;
    public RectTransform freezeBar;
    public RectTransform schinkenBar;

    private CameraController cam;
    private bool onGround;

    private int lives = 3;
    private Rigidbody2D rb2d;
    public enum State
    {
        unarmed,
        armed,
        fridge
    }
    public State playerState = State.unarmed;
    [Range(0, 20)]
    public float speed = 15;
    [Range(0, 10)]
    private float jumpForce = 7;
    public float jumpHeight, jumpTime;

    private float timeCounter = 0;

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
        if (onGround && Input.GetButtonDown("Jump"))
            jump = jumpForce;
        Vector2 jumpVec = new Vector2(0, jump);
        rb2d.AddForce(jumpVec, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag.Equals("Enemy"))
            Reset();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        cam = FindObjectOfType<CameraController>();
        playerState = State.unarmed;
        livesText.text = "" + lives;

        jumpForce = 4F * jumpHeight / jumpTime;
        Physics2D.gravity = Vector2.down * jumpForce * 2F / jumpTime;

        startPosX=transform.position.x;
        startPosY=transform.position.y;
    }

    void Reset()
    {
        lives--;
        livesText.text = "" + lives;
        if (lives>0)
        {
            rb2d.velocity=Vector2.zero;
            playerState=State.unarmed;
            transform.position=new Vector2(startPosX, startPosY);
            fridgeTimeCounter = schinkenTimeCounter = 0;
            rb2d.constraints &= ~RigidbodyConstraints2D.FreezePositionX;

            freezeBar.localScale = new Vector3(Mathf.Clamp01(1 - fridgeTimeCounter / maxFridgeTime), 1, 1);
            schinkenBar.localScale = new Vector3(Mathf.Clamp01(1 - schinkenTimeCounter / maxSchinkenTime), 1, 1);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }

    void FixedUpdate()
    {
        if (transform.position.y < -5)
            Reset();

        Vector3Int pos = Vector3Int.FloorToInt(transform.position + Vector3.Scale(new Vector3(-0.49F, -0.6F, 0), transform.localScale));
        Vector3Int size = new Vector3Int(1 + Mathf.FloorToInt(transform.position.x + 0.49F * transform.localScale.x) - Mathf.FloorToInt(transform.position.x - 0.49F * transform.localScale.x), 1, 1);
        TileBase[] floorTiles = map.GetTilesBlock(new BoundsInt(pos, size));

        bool onGround = Physics2D.Raycast(new Vector2(rb2d.position.x, rb2d.position.y - 0.51f * transform.localScale.y), Vector2.down, 0.01F);
        if (onGround && !this.onGround)
        {
            cam.Shake();
            ground.Play();
        }
        this.onGround = onGround;

        if (Input.GetButtonDown("Fridge"))
        {
            if (playerState != State.fridge)
            {
                playerState = State.fridge;
                rb2d.constraints |= RigidbodyConstraints2D.FreezePositionX;
            }
            else
            {
                playerState = State.unarmed;
                rb2d.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            }
        }
        else if (Input.GetButtonDown("Schinken"))
        {
            if (playerState == State.armed)
                playerState = State.unarmed;
            else if (playerState != State.fridge&&schinkenTimeCounter<=0)
                playerState = State.armed;
        }

        if (playerState != State.fridge)
        {
            move();
            jump();
            if(playerState==State.armed)
                schinkenTimeCounter = Mathf.Min(schinkenTimeCounter + Time.fixedDeltaTime, maxSchinkenTime + 0.001F);
            else
                schinkenTimeCounter = Mathf.Max(0, schinkenTimeCounter -= Time.fixedDeltaTime);

            fridgeTimeCounter = Mathf.Max(0, fridgeTimeCounter - Time.fixedDeltaTime);

            foreach (TileBase b in floorTiles)
                if (b is AnimatedTile && ((AnimatedTile) b).tag.Equals("Fire"))
                {
                    Reset();
                    return;
                }
        }
        else
        {
            rb2d.AddForce(Vector2.down * fridgeGrav);

            fridgeTimeCounter = Mathf.Min(fridgeTimeCounter + Time.fixedDeltaTime, maxFridgeTime + 0.001F);
            schinkenTimeCounter = Mathf.Max(0, schinkenTimeCounter - Time.fixedDeltaTime);
            if (fridgeTimeCounter>=maxFridgeTime)
            {
                Reset();
                return;
            }
            for (int i = 0; i < floorTiles.Length; ++i)
            {
               if (floorTiles[i] is AnimatedTile && ((AnimatedTile)floorTiles[i]).tag.Equals("Destructible"))
                    map.SetTile(pos + new Vector3Int(i, 0, 0), null);
            }
        }

        freezeBar.localScale = new Vector3(Mathf.Clamp01(1 - fridgeTimeCounter / maxFridgeTime), 1, 1);
        schinkenBar.localScale = new Vector3(Mathf.Clamp01(1 - schinkenTimeCounter / maxSchinkenTime), 1, 1);
    }
}