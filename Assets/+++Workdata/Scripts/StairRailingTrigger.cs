using System;
using UnityEngine;

public class StairRailingTrigger : MonoBehaviour
{
    public GameObject maskContainer;
    public GameObject stairMoveCollider;
    public GameObject teleportTriggger;
    public Collider2D colliderToHide;
    public bool triggerIsFront = true;

    private void OnEnable()
    {
        OnEnterRoom();
    }

    public void OnEnterRoom()
    {
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
            colliderToHide.enabled = isBelowTrigger;
            stairMoveCollider.SetActive(!isBelowTrigger);
            teleportTriggger.SetActive(!isBelowTrigger);
        }
        else
        {
            maskContainer.SetActive(isBelowTrigger);
            colliderToHide.enabled = !isBelowTrigger;
            stairMoveCollider.SetActive(isBelowTrigger);
            teleportTriggger.SetActive(isBelowTrigger);
        }
    }
}