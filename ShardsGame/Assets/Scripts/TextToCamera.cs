using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextToCamera : MonoBehaviour
{

    public GameObject mainCamera;

    private void Start()
    {

    }
    void Update()
    {
        transform.LookAt(mainCamera.transform);
    }
}
