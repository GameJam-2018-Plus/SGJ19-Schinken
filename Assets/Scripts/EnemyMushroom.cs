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
    public GameObject poof;
    private Animator anim;

    public void Death()
    {
        Destroy(gameObject);
        Instantiate(poof, transform.position + poof.transform.position, Quaternion.identity, null);
    }
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();
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
        anim.SetBool("right", direction == 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction *= (-1);
    }
}