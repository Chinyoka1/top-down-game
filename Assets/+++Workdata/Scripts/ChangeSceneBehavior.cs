using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneBehavior: MonoBehaviour
{
    public Animator anim;
    public int sceneBuildIndex;

    private void Start()
    {
        anim = GameObject.Find("DoorFadePanel").GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(InitiateTeleport(other));
        }
    }

    IEnumerator InitiateTeleport(Collider2D other)
    {
        anim.Play("black_panel_fade_in");
        yield return new WaitForSeconds(1);
        
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        
        Animator animNew = GameObject.Find("DoorFadePanel").GetComponent<Animator>();
        yield return new WaitForSeconds(.3f);
        animNew.Play("black_panel_fade_out");
    }
}