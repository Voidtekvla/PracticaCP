using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseDoor : MonoBehaviour
{
    public float noise = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            noise = 1;
            Debug.Log(noise);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            noise = 0;
            Debug.Log(noise);
        }
    }

}
