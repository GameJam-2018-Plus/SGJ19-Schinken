using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public PolygonCollider2D frame7Left;
    public PolygonCollider2D frame8Left;
    public PolygonCollider2D frame9Left;
    public PolygonCollider2D frame10Left;
    public PolygonCollider2D frame11Left;
    public PolygonCollider2D frame12Left;
    public PolygonCollider2D frame13Left;
    public PolygonCollider2D frame7Right;
    public PolygonCollider2D frame8Right;
    public PolygonCollider2D frame9Right;
    public PolygonCollider2D frame10Right;
    public PolygonCollider2D frame11Right;
    public PolygonCollider2D frame12Right;
    public PolygonCollider2D frame13Right;
    private PolygonCollider2D[] hitboxes;
    private PolygonCollider2D activeHitbox;
    public enum Frame
    {
        f7L,
        f8L,
        f9L,
        f10L,
        f11L,
        f12L,
        f13L,
        f7R,
        f8R,
        f9R,
        f10R,
        f11R,
        f12R,
        f13R,
        clear
    }

    public void updateHitbox(Frame val)
    {
        if (val != Frame.clear)
        {
            activeHitbox.SetPath(0, hitboxes[(int)val].GetPath(0));
            activeHitbox.enabled = true;
            return;
        }
        activeHitbox.pathCount = 0;
        activeHitbox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Enemy") && activeHitbox.enabled == true)
        {
            col.gameObject.GetComponent<EnemyMushroom>().Death();
        }
        if (col.tag.Equals("Boss") && activeHitbox.enabled == true)
        {
            col.gameObject.GetComponent<BossBehaviour>().ReduceBossLive(20);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        hitboxes = new PolygonCollider2D[] { frame7Left, frame8Left, frame9Left, frame10Left, frame11Left, frame12Left, frame13Left, frame7Right, frame8Right, frame9Right, frame10Right, frame11Right, frame12Right, frame13Right };
        activeHitbox = gameObject.AddComponent<PolygonCollider2D>();
        activeHitbox.isTrigger = true;
        activeHitbox.pathCount = 0;
        activeHitbox.enabled = false;
    }
}