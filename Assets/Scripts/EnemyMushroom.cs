using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroom : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Transform enemy;
    private float maxDist = 12.5f;
    private float minDist = -12.5f;
    private float movingSpeed = 10f;
    private int direction;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Transform>();
        maxDist += enemy.position.x;
        minDist -= enemy.position.x;
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
        direction *= (-1);
    }

}

