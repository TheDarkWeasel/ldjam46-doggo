using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SheepController : MonoBehaviour
{
    private Animator sheepAnimator;
    private NavMeshAgent navMeshAgent;
    private Vector3 destination = Vector3.zero;

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
        //No destination set
        if(destination == Vector3.zero)
        {
            int offsetX = Random.Range(-2, 2);
            int offsetZ = Random.Range(-2, 2);

            destination = new Vector3(gameObject.transform.position.x + offsetX, gameObject.transform.position.y, gameObject.transform.position.z + offsetZ);
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(destination);
            PlayRunningAnimation();
        }

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
}
