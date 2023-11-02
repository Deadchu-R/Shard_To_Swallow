using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPlate : MonoBehaviour
{

    public Transform start1;
    public Transform finish1;
    public GameObject door1;

    public Transform start2;
    public Transform finish2;
    public GameObject door2;

    public float speed = 5.0f;
    public bool MoveToOpen = true;


    void Update()
    {
        if (MoveToOpen)
        {
            door1.transform.position = Vector3.MoveTowards(door1.transform.position, start1.position, speed * Time.deltaTime);
            door2.transform.position = Vector3.MoveTowards(door2.transform.position, start2.position, speed * Time.deltaTime);
        }
        else
        {
            door1.transform.position = Vector3.MoveTowards(door1.transform.position, finish1.position, speed * Time.deltaTime);
            door2.transform.position = Vector3.MoveTowards(door2.transform.position, finish2.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Shard"))
        {
            MoveToOpen = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Shard"))
        {
            MoveToOpen = true;
        }
    }
}
