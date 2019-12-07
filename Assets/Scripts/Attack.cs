using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private PolygonCollider2D[] hitboxes;
    private PolygonCollider2D activeHitbox;
    public enum frames
    {
        clear
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.collider.tag.Equals("Enemy"))
        {
            other.gameObject.GetComponent<EnemyMushroom>().Death();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        hitboxes=new PolygonCollider2D[]{};
        activeHitbox=gameObject.AddComponent<PolygonCollider2D>();
        activeHitbox.isTrigger=true;
        activeHitbox.pathCount=0;
    }
}