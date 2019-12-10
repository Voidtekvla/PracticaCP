using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearing : MonoBehaviour
{
    public GameObject parent;
    public GameObject ears;
    private float noiseTresholdClose = 0.0f;
    private float noiseTresholdMid = 0.01f;
    private float noiseTresholdFar = 0.34f;
    private float treshold;

    private void Start()
    {
        ears = gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            float distance = Vector3.Distance(other.transform.position, ears.transform.position);

            if (distance < 5)
                treshold = noiseTresholdClose;
            else if (distance < 10)
                treshold = noiseTresholdMid;
            else
                treshold = noiseTresholdFar;

            if (other.GetComponent<SoundEmision>().noise >= treshold)
            {
                // Dirección del rayo
                Vector3 direction = other.transform.position + Vector3.up / 2 - ears.transform.position;
                //Dibujamos el rayo en depuración
                Debug.DrawRay(ears.transform.position, direction, Color.red);
                if (parent.tag == "Nopperabou")
                    parent.GetComponent<NopperabouBehaviour>().setPerception(true, Vector3.up);
                else
                    parent.GetComponent<FutakuchiBehaviour>().setPerception(true, Vector3.up);
            }
            else
            {
                if (parent.tag == "Nopperabou")
                    parent.GetComponent<NopperabouBehaviour>().setPerception(false, Vector3.up);
                else
                    parent.GetComponent<FutakuchiBehaviour>().setPerception(false, Vector3.up);
            }
        }
        if(other.tag == "Door" && other.GetComponent<SoundEmision>().noise > 0)
        {
            if (parent.tag == "Nopperabou")
                parent.GetComponent<NopperabouBehaviour>().setPerception(true, other.transform.position);
            else
                parent.GetComponent<FutakuchiBehaviour>().setPerception(true, other.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (parent.tag == "Nopperabou")
                parent.GetComponent<NopperabouBehaviour>().setPerception(false, Vector3.up);
            else
                parent.GetComponent<FutakuchiBehaviour>().setPerception(false, Vector3.up);
        }
    }
}
