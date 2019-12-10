using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{

    public GameObject parent;
    public GameObject eyes;

    private void OnTriggerStay(Collider other)
    {
        // Comprueba si ve al jugador
        if (other.tag == "Player")
        {
            // Dirección del rayo vallecano
            Vector3 direction = other.transform.position + Vector3.up / 2 - eyes.transform.position;
            // Lanzamos un rayo desde los "ojos" al jugador
            RaycastHit hit;
            Physics.Raycast(eyes.transform.position, direction, out hit);

            //Dibujamos el rayo en depuración
            Debug.DrawRay(eyes.transform.position, direction, Color.red);
            if (hit.transform.gameObject.tag == "Player") {
                if (parent.tag == "Futakuchi")
                    parent.GetComponent<FutakuchiBehaviour>().setPerception(true);
                else
                    parent.GetComponent<EnemyBehaviour>().setPerception(true);

            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Deja de ver al jugador
        if (other.tag == "Player")
        {

            if (parent.tag == "Futakuchi")
                parent.GetComponent<FutakuchiBehaviour>().setPerception(false);
            else
                parent.GetComponent<EnemyBehaviour>().setPerception(false);
        }
    }
}
