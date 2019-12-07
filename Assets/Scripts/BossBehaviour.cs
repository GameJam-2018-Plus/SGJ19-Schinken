using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    //boss has different states
    public enum State
    {
        idle, //do nothing, but not as much time
        spores, //throw venom balls
        root,//track player
        shake //earthquake
    }

    private State state;
    // Start is called before the first frame update
    void Start()
    {
        state = State.idle;
        StartCoroutine();
    }



    void FixedUpdate()
    {
        if (state == State.spores) //spore attac
        {
            SporeAttacking();
        }
        else if (state == State.root) //root attac
        {
            RootAttacking();
        }
        else if (state == State.shake) //earthquake attac
        {
            ShakeAttacking();
        }
        else //idle
        {
            Idle();
        }
    }

    void SporeAttacking()
    {
        Debug.Log("Spore animation");
        //do animation of attacking with spores
        //change after animation to 50 % idle, 25 % root, 25 % shake
    }
    void RootAttacking()
    {
        Debug.Log("RootAttacking");
        //do animation of attacking with root
        //change after animation to 80 % idle, 10 % spore, 10 % shake
    }
    void ShakeAttacking()
    {
        Debug.Log("Earthquake!");
        //do animation of attacking with earthquake
        //change after animation to 80 % idle, 10 % root, 10 % spore
    }
    IEnumerator Idle()
    {
        Debug.Log("Idle");
        //wait for random seconds 
        yield return new WaitForSeconds(6);
        System.Random rand = new System.Random();
        int randNumber = rand.Next(0, 3); //rand int 0,1,2

        //change after wait to 33% spore, 33% root, 33% shake
        switch (randNumber)
        {
            case 0:
                ShakeAttacking();
                break;
            case 1:
                RootAttacking();
                break;
            case 2:
                SporeAttacking();
                break;
        }
    }
}
