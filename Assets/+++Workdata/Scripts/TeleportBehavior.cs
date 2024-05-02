using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBehaviour : MonoBehaviour
{
    public Transform spawnPos;
    public Animator anim;

    private void Start()
    {
        // Simple Fade
        //anim = GameObject.Find("SimpleFade").GetComponent<Animator>();
        
        // Circle Fade
        anim = GameObject.Find("CircleFade").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(InitiateTeleport(other));
        }
    }

    IEnumerator InitiateTeleport(Collider2D other)
    {
        //anim.Play("black_panel_fade_in");
        //yield return new WaitForSeconds(1);
        //other.transform.position = spawnPos.position;
        //yield return new WaitForSeconds(.5f);
        //anim.Play("black_panel_fade_out");
        
        anim.Play("circle_fade_out");
        yield return new WaitForSeconds(1);
        other.transform.position = spawnPos.position;
        yield return new WaitForSeconds(.5f);
        anim.Play("circle_fade_in");
    }
}