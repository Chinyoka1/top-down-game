using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportBehaviour : MonoBehaviour
{
    public Transform spawnPos;
    public Animator anim;
    public enum FadeType {Simple, Circle, Bramble}
    public FadeType fadeType = FadeType.Simple;
    public bool changeScene;
    public string sceneName;

    private string fadeInAnimation;
    private string fadeOutAnimation;

    private void Start()
    {
        switch (fadeType)
        {
            case FadeType.Simple:
            {
                anim = GameObject.Find("SimpleFade").GetComponent<Animator>();
                fadeInAnimation = "black_panel_fade_out";
                fadeOutAnimation = "black_panel_fade_in";
                break;
            }
            case FadeType.Circle:
            {
                anim = GameObject.Find("CircleFade").GetComponent<Animator>();
                fadeInAnimation = "circle_fade_in";
                fadeOutAnimation = "circle_fade_out";
                break;
            }
            case FadeType.Bramble:
            {
                anim = GameObject.Find("BrambleFade").GetComponent<Animator>();
                fadeInAnimation = "bramble_fade_in";
                fadeOutAnimation = "bramble_fade_out";
                break;
            }
        }
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
        anim.Play(fadeOutAnimation);
        yield return new WaitForSeconds(1);
        if (changeScene)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            other.transform.position = spawnPos.position;
        }
        yield return new WaitForSeconds(.5f);
        anim.Play(fadeInAnimation);
    }
}