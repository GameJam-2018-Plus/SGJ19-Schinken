using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroom : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Transform enemy;
    public float maxDist = 5.5f;
    public float minDist = -5.5f;
    public float movingSpeed = 5f;
    private int direction;

    public void Death()
    {
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Transform>();
        maxDist += enemy.position.x;
        minDist += enemy.position.x;
        direction = -1;
    }

    void Update()
    {
        switch (direction)
        {
            case -1:
                // Moving Left
                if (enemy.position.x > minDist)
                {
                    rb2d.velocity = new Vector2(-movingSpeed, rb2d.velocity.y);
                }
                else
                {
                    direction = 1;
                }
                break;
            case 1:
                //Moving Right
                if (enemy.position.x < maxDist)
                {
                    rb2d.velocity = new Vector2(movingSpeed, rb2d.velocity.y);
                }
                else
                {
                    direction = -1;
                }
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag.Equals("Player"))
        {
            if(collision.gameObject.GetComponent<Player>().playerState==Player.State.fridge)
            {
                if(collision.gameObject.GetComponent<Player>().onGround==false)
                {
                    Destroy(gameObject);
                }
            }
        }
        direction *= (-1);
    }
}