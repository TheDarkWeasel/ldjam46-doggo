using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfController : MonoBehaviour
{
    private SheepController target = null;
    private NavMeshAgent navMeshAgent;

    private Animator wolfAnimator;

    private float speed = 0.5f;

    private float unitSize;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateUpAxis = false;

        wolfAnimator = GetComponent<Animator>();
        unitSize = GetComponent<Collider>().bounds.size.z;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null || target.IsDead())
        {
            SheepController[] sheep = FindObjectsOfType<SheepController>();
            target = sheep[Random.Range(0, sheep.Length)];
        }

        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(target.GetPosition());
        PlayRunningAnimation();

        TurnTowardsTarget();

        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= (unitSize + navMeshAgent.stoppingDistance))
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude <= 0.6f)
                {
                    navMeshAgent.isStopped = true;
                    target = null;
                    StopRunningAnimation();
                }
            }
        }
    }

    private void TurnTowardsTarget()
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = target.GetPosition() - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = speed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, -targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void OnCollisionEnter(Collision collision)
    {
        
    }

    private void PlayRunningAnimation()
    {
        wolfAnimator.SetTrigger("Run");
        wolfAnimator.ResetTrigger("Stop");
    }

    private void StopRunningAnimation()
    {
        wolfAnimator.SetTrigger("Stop");
        wolfAnimator.ResetTrigger("Run");
    }
}
