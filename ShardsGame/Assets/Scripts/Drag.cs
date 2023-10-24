using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private bool isHolding = false;
    private Vector3 playerPosition;


    private void Update()
    {
        DragIt();
    }
    void DragIt()
    {

        if (Input.GetKeyDown(KeyCode.C) && !isHolding)
        {
            PickUp();
        }

        if (Input.GetKeyUp(KeyCode.C) && isHolding)
        {
            Drop();
        }

        if (isHolding)
        {
            // Continuously update the object's position to follow the player's position
            transform.position = playerPosition;
        }
    }


    void PickUp()
    {
        isHolding = true;
        playerPosition = transform.position;
    }

    void Drop()
    {
        isHolding = false;
    }
}
