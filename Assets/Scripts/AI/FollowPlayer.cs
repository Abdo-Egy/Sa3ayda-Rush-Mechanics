using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    NavMeshAgent Agent;
    Animator Animator;
    bool _followPlayer;
    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            _followPlayer = true;
        if (Input.GetKeyDown(KeyCode.Q))
            _followPlayer = false;
        Animator.SetBool("Run", _followPlayer);

        if (_followPlayer)
        {
            Agent.enabled = true;
            Agent.SetDestination(GameManager.instance.Player.transform.position);
        }
        else
            Agent.enabled = false;
    }
}
