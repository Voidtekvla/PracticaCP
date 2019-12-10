using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaWaypoint : MonoBehaviour
{
    public float ratio = 5;
    private Vector3 debugPos;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //giveMeADestination(this.gameObject);
    }

    public Vector3 giveMeADestination()
    {
        // Calculamos posiciones aleatorias en un círculo
        Vector3 circlePos = Random.insideUnitCircle * ratio;
        Vector3 pos = new Vector3(circlePos.x, transform.position.y, circlePos.y);
        pos += transform.position;
        debugPos = pos;
        return pos;
    }

    private void OnDrawGizmos()
    {
        // Se comenta para evitar errores de compilacion
        // Dibujamos el radio de la zona de aparición
        /*UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, ratio);

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, 0.1f);

        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(debugPos, 0.1f);*/
    }
}
