using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public enum MoveMode
{
    patrol, chase, wait
}

[RequireComponent(typeof(NavMeshAgent))]

public class AI : MonoBehaviour
{
    public MoveMode moveMode;
    public Animator animator;
    bool isHit;

    [Header("Steering")]
    public float patrolSpeed;
    public float chaseSpeed;
    public float maxTimeChasing;
    public float maxTimeWaiting;
    public float radiusHit;


    [Header("Field Of View")]
    public float viewRadius;
    public float viewAngle;
    public LayerMask obstacleMask;
    public LayerMask targetMask;

    [Header("Transform")]
    public Transform[] patrolPoint;
    public NavMeshAgent agent;
    public Transform currentTarget;

    Vector3 destination, soundPositionHeared;
    int indexPatrolPoint;
    float currentTimeChasing, currentTimeWaiting;
    bool isDetectTarget, isHearingSound;

    // Start is called before the first frame update
    void Start()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        if (agent.stoppingDistance < 0.5f)
            agent.stoppingDistance = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
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

        FieldOfView();
        Animations();
    }

    void Animations()
    {
        if (animator == null) return;

        animator.SetBool("chase", moveMode == MoveMode.chase);
    }
    void FieldOfView()
    {
        Collider[] range = Physics.OverlapSphere(transform.position, viewRadius, targetMask, QueryTriggerInteraction.Ignore);

        if (range.Length > 0)
        { 
            currentTarget = range[0].transform;

            Vector3 direction = (currentTarget.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, direction) < viewAngle / 2)
            {
                float m_distance = Vector3.Distance(transform.position, currentTarget.position);

                if (!Physics.Raycast(transform.position, direction, m_distance, obstacleMask, QueryTriggerInteraction.Ignore))
                {
                    isDetectTarget = true;

                    if (moveMode != MoveMode.chase)
                    {
                        SwitchMoveMode(MoveMode.chase);
                    }
                }
                else
                {
                    isDetectTarget = false;
                }
            }
            else
            {
                isDetectTarget = false;
            }
        }
        else
        {
            isDetectTarget = false;
        }
    }

    void Patroling()
    {
        agent.speed = patrolSpeed;

        if (agent.remainingDistance < agent.stoppingDistance)
        {
            SwitchMoveMode(MoveMode.wait);
            isHearingSound = false;
        }
    }

    void Chasing()
    {
        agent.speed = chaseSpeed;
        agent.destination = currentTarget.position;

        Collider[] col = Physics.OverlapSphere(transform.position, radiusHit, targetMask, QueryTriggerInteraction.Ignore);

        if (col.Length > 0 && !isHit)
        {
            Debug.Log("Juego terminado/bajar vida");
            isHit = true;
        }

        if (currentTimeChasing > maxTimeChasing)
        {
            SwitchMoveMode(MoveMode.wait);
        }
        else if(!isDetectTarget)
        {
            currentTimeChasing += Time.deltaTime;
        }
    }

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

    void SwitchMoveMode(MoveMode moveMode1)
    {
        switch (moveMode1)
        {
            case MoveMode.patrol:
                int lastIndex = indexPatrolPoint;
                int newIndex = Random.Range(0, patrolPoint.Length);

                if (lastIndex == newIndex)
                {
                    newIndex = Random.Range(0, patrolPoint.Length);
                    Debug.Log("Recalculando punto de patrullaje");
                    return;
                }

                indexPatrolPoint = newIndex;
                if (isHearingSound)
                    destination = soundPositionHeared;
                else
                    destination = patrolPoint[indexPatrolPoint].position;

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
