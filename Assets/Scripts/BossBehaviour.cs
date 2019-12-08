using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BossBehaviour : MonoBehaviour
{

    private Animator anim;
    public Transform model;
    private System.Random rand = new System.Random();


    public int maxHealth = 100;
    public int curHealth = 100;

    //boss has different states
    public enum State
    {
        idle, //do nothing, but not as much time
        spores, //throw venom balls
        root,//track player
        shake //earthquake
    }
    private State playerState;

    // Start is called before the first frame update
    void Start()
    {
        playerState = State.idle;
        anim = model.GetComponentInChildren<Animator>();
        StartCoroutine(Idle());
    }


    public void ReduceBossLive(int damage)
    {
        curHealth -= damage;
        //you won
        if (curHealth <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }

    void SporeAttacking()
    {
        Debug.Log("Spore animation");
        playerState = State.spores;
        anim.SetTrigger("spores");
        //do animation of attacking with spores
        //change after animation to 50 % idle, 25 % root, 25 % shake
        int randNumber = rand.Next(0, 4); //rand int 0,1,2,3
        switch (randNumber)
        {
            case 0:
                ShakeAttacking();
                break;
            case 1:
                RootAttacking();
                break;
            case 2:
                StopAllCoroutines();
                StartCoroutine(Idle());
                break;
            case 3:
                StopAllCoroutines();
                StartCoroutine(Idle());
                break;
        }

    }
    void RootAttacking()
    {
        playerState = State.root;
        anim.SetTrigger("root");
        //do animation of attacking with root
        //change after animation to 80 % idle, 10 % spore, 10 % shake

        int randNumber = rand.Next(0, 11); //rand int 0,1,...,10
        if (randNumber == 9)
        {
            ShakeAttacking();
        }
        else if (randNumber == 10)
        {
            SporeAttacking();
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(Idle());
        }
    }
    void ShakeAttacking()
    {
        playerState = State.shake;
        anim.SetTrigger("earthquake");
        //do animation of attacking with earthquake
        //change after animation to 80 % idle, 10 % root, 10 % spore
        int randNumber = rand.Next(0, 11); //rand int 0,1,...,10
        if (randNumber == 9)
        {
            RootAttacking();
        }
        else if (randNumber == 10)
        {
            SporeAttacking();
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(Idle());
        }
    }
    IEnumerator Idle()
    {
        anim.SetTrigger("idle");
        //wait for random seconds 
        yield return new WaitForSeconds(6);
        int randNumber = rand.Next(0, 3);  //rand int 0,1,2

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
            case 3:
                SporeAttacking();
                break;
        }
    }
}