using System;
using UnityEngine;

public class StairRailingTrigger : MonoBehaviour
{
    public GameObject maskContainer;
    public Collider2D railingCollider;

    private void OnEnable()
    {
        // Check Player position when entering the scene
        ControlStairStatus(GameObject.Find("Player").transform.position.y < transform.position.y);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            ControlStairStatus(col.transform.position.y < transform.position.y);
        }
    }

    private void ControlStairStatus(bool isBelowTrigger)
    {
        maskContainer.SetActive(!isBelowTrigger);
        railingCollider.enabled = isBelowTrigger;
    }
}