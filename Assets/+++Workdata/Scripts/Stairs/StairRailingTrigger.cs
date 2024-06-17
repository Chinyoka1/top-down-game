#nullable enable
using System;
using UnityEngine;

public class StairRailingTrigger : MonoBehaviour
{
    public GameObject maskContainer;
    public GameObject? stairMoveCollider;
    public GameObject? teleportTriggger;
    public Collider2D? colliderToHide;
    public bool triggerIsFront = true;

    private void OnEnable()
    {
        OnEnterRoom();
    }

    public void OnEnterRoom()
    {
        if (stairMoveCollider != null) stairMoveCollider.SetActive(false);
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
        if (triggerIsFront)
        {
            maskContainer.SetActive(!isBelowTrigger);
            if (stairMoveCollider != null) stairMoveCollider.SetActive(!isBelowTrigger);
            if (teleportTriggger != null) teleportTriggger.SetActive(!isBelowTrigger);
            if (colliderToHide != null) colliderToHide.enabled = isBelowTrigger;
        }
        else
        {
            maskContainer.SetActive(isBelowTrigger);
        }
    }
}