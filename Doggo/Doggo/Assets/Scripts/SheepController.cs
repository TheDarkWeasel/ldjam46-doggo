using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SheepController : MonoBehaviour
{
    private Animator sheepAnimator;
    private NavMeshAgent navMeshAgent;
    private Vector3 destination = Vector3.zero;

    private float speed = 0.5f;

    private float unitSize;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateUpAxis = false;
        sheepAnimator = GetComponentInChildren<Animator>();
        unitSize = GetComponent<Collider>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //Map bounds
        int minX = -5;
        int minZ = -9;
        int maxX = 30;
        int maxZ = 22;

        //No destination set
        if(destination == Vector3.zero)
        {
            int offsetX = Random.Range(-2, 2);
            int offsetZ = Random.Range(-2, 2);

            int x = (int) Mathf.Max(Mathf.Min(gameObject.transform.position.x + offsetX, maxX), minX);
            int z = (int)Mathf.Max(Mathf.Min(gameObject.transform.position.z + offsetZ, maxZ), minZ);

            destination = new Vector3(x, gameObject.transform.position.y, z);
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(destination);
            PlayRunningAnimation();
        }

        TurnTowardsTarget(destination);

        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= (unitSize + navMeshAgent.stoppingDistance))
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude <= 0.6f)
                {
                    navMeshAgent.isStopped = true;
                    destination = Vector3.zero;
                    StopRunningAnimation();
                }
            }
        }
    }

    private void TurnTowardsTarget(Vector3 destination)
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = destination - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = speed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void PlayRunningAnimation()
    {
        sheepAnimator.SetTrigger("Run");
        sheepAnimator.ResetTrigger("Stop");
    }

    private void StopRunningAnimation()
    {
        sheepAnimator.SetTrigger("Stop");
        sheepAnimator.ResetTrigger("Run");
    }

    public bool IsDead()
    {
        return false;
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }
}
