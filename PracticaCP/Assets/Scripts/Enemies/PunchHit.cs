using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchHit : MonoBehaviour
{

    GameObject canvas;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("ScreenCanvas");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            canvas.gameObject.GetComponent<CanvasManager>().gameOver();
        }
    }
}
