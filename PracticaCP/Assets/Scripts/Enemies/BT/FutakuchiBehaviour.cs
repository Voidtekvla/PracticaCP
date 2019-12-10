using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using Panda;

public class FutakuchiBehaviour : MonoBehaviour
{
    // Velocidad
    private float speed = 4f;

    // Ultima posición conocida del jugador
    Vector3 destination = Vector3.zero;

    private GameObject player;
    // Waypoints
    public List<AreaWaypoint> waypoints;


    // NavMeshAgent
    private NavMeshAgent _agent;

    // Animator
    private Animator anim;

    // Contador
    private float waitCounter = 0f;
    private float waitTime = 2f;

    bool door = false;

    #region tasks

    // Comprobamos si el jugador ha sido eschado
    [Task]
    bool IsHeard = false;
    // Comprobamos si el jugador está a la vista
    [Task]
    bool IsVisible = false;

    // En caso de no escuchar al jugador, llega a su última posición, o si no lo ha oído, avanza hasta su siguiente ubicación de patrulla
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
        NavMeshHit hit;
        int aux = Random.Range(0, waypoints.Count);
        NavMesh.SamplePosition(waypoints[aux].GetComponent<AreaWaypoint>().giveMeADestination(), out hit, waypoints[aux].GetComponent<AreaWaypoint>().ratio, NavMesh.AllAreas);
        _agent.destination = hit.position;

        task.Succeed();
    }

    // Cuando el jugador está a la vista, lo sigue
    [Task]
    void moveToPlayer()
    {
        Task task = Task.current;

        if (_agent.destination != destination)
        {
            destination = player.transform.position;
            _agent.SetDestination(destination);

            if (Vector3.Distance(transform.position, player.transform.position) <= _agent.stoppingDistance)
                task.Succeed();
            else
                anim.SetBool("attack", false);
        }
    }

    // Cuando  oye un ruído, lo sigue
    [Task]
    void moveToSound()
    {
        Task task = Task.current;

        if (door)
        {
            _agent.SetDestination(destination);

            if (Vector3.Distance(transform.position, destination) <= _agent.stoppingDistance)
                task.Succeed();
            else
                anim.SetBool("attack", false);
        }
        else if (_agent.destination != destination)
        {
            destination = player.transform.position;

            NavMeshHit hit;
            int aux = Random.Range(0, waypoints.Count);
            NavMesh.SamplePosition(destination, out hit, 0.5f, NavMesh.AllAreas);
            _agent.destination = hit.position;
            _agent.SetDestination(hit.position);

            if (Vector3.Distance(transform.position, destination) <= _agent.stoppingDistance)
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

    public void setPerception(bool heard, Vector3 doorPos)
    {
        if (!doorPos.Equals(Vector3.up))
        {
            destination = doorPos;
            door = true;
        }
        else
            door = false;


        IsHeard = heard;

    }


    public void setPerception(bool seen)
    {
        IsVisible = seen;
        if (IsVisible)
            IsHeard = false;
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
            setPerception(true, Vector3.up);
    }

}