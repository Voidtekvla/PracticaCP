using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using Panda;

public class EnemyBehaviour : MonoBehaviour
{
    // Velocidad
    private float speed = 2f;

    // Ultima posición conocida del jugador
    Vector3 destinationPlayer = Vector3.zero;

    private GameObject player;
    // Waypoints
    public List<AreaWaypoint> waypoints;


    // NavMeshAgent
    private NavMeshAgent _agent;

    // Animator
    private Animator anim;

    // Contador
    private float waitCounter = 0f;
    private float waitTime = 3f;

    #region tasks

    // Comprobamos si el jugador está a la vista
    [Task]
    bool IsIn = false;

    // En caso de no tener al jugador a la vista, llega a su última posición, o si no lo ha visto, avanza hasta su siguiente ubicación de patrulla
    [Task]
    void stayIdle()
    {
        Task task = Task.current;
        anim.SetBool("attack", false);
        if (_agent.remainingDistance <= _agent.stoppingDistance)
            task.Succeed();

    }

    // Espera en la ubicación
    [Task]
    void wait()
    {
        Task task = Task.current;
        waitCounter += Time.deltaTime;
        if (waitCounter >= waitTime)
        {
            waitCounter = 0;
            task.Succeed();

        }
    }

    // Cuando no persiga al jugador, escoge su siguiente destino de patrulla
    [Task]
    void chooseDestination()
    {
        Task task = Task.current;

        // Buscamos la posición válida en el NavMesh más cercana a la posición que recibe de un AreaWaypoint del agente aleatorio
        
        int aux = Random.Range(0, waypoints.Count);
        try
        {
            NavMeshHit hit;
            NavMesh.SamplePosition(waypoints[aux].GetComponent<AreaWaypoint>().giveMeADestination(), out hit, waypoints[aux].GetComponent<AreaWaypoint>().ratio, NavMesh.AllAreas);
            _agent.destination = hit.position;
        }
        catch(System.Exception e)
        {
            Debug.Log(aux);
        }

        task.Succeed();
    }

    // Cuando el jugador está a la vista, lo sigue
    [Task]
    void moveToPlayer()
    {
        Task task = Task.current;

        if (_agent.destination != destinationPlayer)
        {
            destinationPlayer = player.transform.position;
            _agent.SetDestination(destinationPlayer);

            if (Vector3.Distance(transform.position, player.transform.position) <= _agent.stoppingDistance)
                task.Succeed();
            else
                anim.SetBool("attack", false);
        }
    }

    // Cuando persigue al jugador, lo golpea cuando está a su alcance
    [Task]
    void attack()
    {
        Task task = Task.current;

        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            transform.LookAt(player.transform.position);
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                anim.SetBool("attack", true);
                task.Succeed();
            }
        }
    }
    #endregion

    public void setPerception(bool seen)
    {
        IsIn = seen;
    }


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {
        if (_agent.remainingDistance > _agent.stoppingDistance)
            anim.SetFloat("speed", speed);
        else
        {
            anim.SetFloat("speed", 0);
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.red;
        Gizmos.DrawCube(_agent.destination, Vector3.one);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
            setPerception(true);
    }

}