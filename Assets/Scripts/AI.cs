using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

/// <summary>
/// Modos de movimiento que puede adoptar la IA.
/// </summary>
public enum MoveMode
{
    patrol, // Patrullando puntos
    chase,  // Persiguiendo objetivo
    wait    // Esperando en el lugar
}

[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour
{
    public MoveMode moveMode;         // Modo actual de movimiento
    public Animator animator;         // Controlador de animaciones
    bool isHit;                       // Flag para evitar múltiples colisiones

    [Header("Steering")]
    public float patrolSpeed;         // Velocidad al patrullar
    public float chaseSpeed;          // Velocidad al perseguir
    public float maxTimeChasing;      // Tiempo máximo persiguiendo
    public float maxTimeWaiting;      // Tiempo máximo esperando
    public float radiusHit;           // Radio de colisión o alcance

    [Header("Field Of View")]
    public float viewRadius;          // Radio de visión
    public float viewAngle;           // Ángulo de visión
    public LayerMask obstacleMask;    // Máscara de obstáculos que bloquean la visión
    public LayerMask targetMask;      // Máscara del objetivo a detectar

    [Header("Transform")]
    public Transform[] patrolPoint;   // Puntos de patrullaje
    public NavMeshAgent agent;        // Componente NavMeshAgent
    public Transform currentTarget;   // Objetivo actual (jugador u otro)

    Vector3 destination, soundPositionHeared;
    int indexPatrolPoint;
    int lastPatrolPoint = -1;

    float currentTimeChasing, currentTimeWaiting;
    bool isDetectTarget, isHearingSound;

    void Start()
    {
        // Asegura que el agente esté asignado
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        // Ajusta la distancia mínima de detención
        if (agent.stoppingDistance < 0.5f)
            agent.stoppingDistance = 0.5f;
    }

    void Update()
    {
        // Ejecuta el comportamiento según el modo de movimiento
        switch (moveMode)
        {
            case MoveMode.patrol:
                Patroling();
                break;
            case MoveMode.chase:
                Chasing();
                break;
            case MoveMode.wait:
                Waiting();
                break;
        }

        FieldOfView(); // Revisa si el objetivo está en la vista
        Animations();  // Actualiza animaciones
    }

    /// <summary>
    /// Controla la animación según el estado de movimiento.
    /// </summary>
    void Animations()
    {
        if (animator == null) return;
        animator.SetBool("chase", moveMode == MoveMode.chase);
    }

    /// <summary>
    /// Detecta si hay un objetivo dentro del campo de visión.
    /// </summary>
    void FieldOfView()
    {
        isDetectTarget = false;

        Collider[] range = Physics.OverlapSphere(transform.position, viewRadius, targetMask, QueryTriggerInteraction.Ignore);

        if (range.Length > 0)
        { 
            currentTarget = range[0].transform;
            Vector3 direction = (currentTarget.position - transform.position).normalized;

            // Comprueba si está dentro del ángulo de visión
            if (Vector3.Angle(transform.forward, direction) < viewAngle / 2)
            {
                float m_distance = Vector3.Distance(transform.position, currentTarget.position);

                // Comprueba si no hay obstáculos
                if (!Physics.Raycast(transform.position, direction, m_distance, obstacleMask, QueryTriggerInteraction.Ignore))
                {
                    isDetectTarget = true;

                    if (moveMode != MoveMode.chase)
                        SwitchMoveMode(MoveMode.chase);
                }
            }
        }
    }

    /// <summary>
    /// Comportamiento de patrullaje entre puntos.
    /// </summary>
    void Patroling()
    {
        if (agent.speed != patrolSpeed)
            agent.speed = patrolSpeed;

        // Si llegó al destino
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && agent.velocity.magnitude < 0.1f)
        {
            SwitchMoveMode(MoveMode.wait);
            isHearingSound = false;
        }
    }

    /// <summary>
    /// Comportamiento de persecución del objetivo.
    /// </summary>
    void Chasing()
    {
        agent.speed = chaseSpeed;
        agent.destination = currentTarget.position;

        // Comprueba si está dentro del rango de colisión
        Collider[] col = Physics.OverlapSphere(transform.position, radiusHit, targetMask, QueryTriggerInteraction.Ignore);
        if (col.Length > 0 && !isHit)
        {
            Debug.Log("Juego terminado/bajar vida");
            isHit = true;
        }

        // Controla el tiempo máximo de persecución
        if (currentTimeChasing > maxTimeChasing)
        {
            SwitchMoveMode(MoveMode.wait);
        }
        else if(!isDetectTarget)
        {
            currentTimeChasing += Time.deltaTime;
        }
    }

    /// <summary>
    /// Comportamiento de espera entre cambios de estado.
    /// </summary>
    void Waiting()
    {
        if (currentTimeWaiting > maxTimeWaiting)
        {
            SwitchMoveMode(MoveMode.patrol);
        }
        else
        {
            currentTimeWaiting += Time.deltaTime;
        }
    }

    /// <summary>
    /// Devuelve un índice aleatorio de patrullaje diferente al actual y al anterior.
    /// </summary>
    int GetRandomPatrolIndexExcept(int current, int previous)
    {
        if (patrolPoint.Length <= 2)
            return (current + 1) % patrolPoint.Length;

        List<int> indices = new List<int>();
        for (int i = 0; i < patrolPoint.Length; i++)
        {
            if (i != current && i != previous)
                indices.Add(i);
        }   

        return indices[Random.Range(0, indices.Count)];
    }

    /// <summary>
    /// Cambia el estado actual de la IA.
    /// </summary>
    void SwitchMoveMode(MoveMode moveMode1)
    {
        switch (moveMode1)
        {
            case MoveMode.patrol:
                int newIndex = GetRandomPatrolIndexExcept(indexPatrolPoint, lastPatrolPoint);
                lastPatrolPoint = indexPatrolPoint;
                indexPatrolPoint = newIndex;
                destination = isHearingSound ? soundPositionHeared : patrolPoint[indexPatrolPoint].position;
                agent.destination = destination;
                Debug.Log("Cambiando punto de patrullaje a: " + indexPatrolPoint.ToString());
                break;

            case MoveMode.chase:
                isHearingSound = false;
                currentTimeChasing = 0;
                break;

            case MoveMode.wait:
                agent.destination = transform.position;
                currentTimeWaiting = 0;
                break;
        }

        moveMode = moveMode1;
        Debug.Log("El modo del enemigo cambia a " + moveMode.ToString());
    }

    /// <summary>
    /// Simula la reacción de la IA al oír un sonido (solo si no está persiguiendo).
    /// </summary>
    public void HearingSound(Vector3 m_destination)
    {
        if (moveMode == MoveMode.chase) return;

        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(m_destination, path);
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            soundPositionHeared = m_destination;
            isHearingSound = true;

            SwitchMoveMode(MoveMode.patrol);
            Debug.Log("Hearing sound");
        }
    }

    /// <summary>
    /// Dibuja gizmos para visualizar la visión y el área de acción de la IA.
    /// </summary>
    void OnDrawGizmos()
    {
        if (agent == null) return;

        Gizmos.DrawWireSphere(transform.position, agent.stoppingDistance);
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        if (currentTarget != null && isDetectTarget)
            Gizmos.DrawLine(transform.position, currentTarget.position);

        float halffov = viewAngle / 2f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halffov, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halffov, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawRay(transform.position, leftRayDirection * viewRadius);
        Gizmos.DrawRay(transform.position, rightRayDirection * viewRadius);
    }
}
