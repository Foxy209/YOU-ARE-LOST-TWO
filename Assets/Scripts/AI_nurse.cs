using UnityEngine;
using UnityEngine.AI;

public class AI_nurse : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        FindPlayer();
    }

    void FindPlayer()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("FPSPlayer");
    
        foreach (GameObject obj in allPlayers)
        {
            if (obj.activeInHierarchy)
            {
                player = obj.transform;
                return;
            }
        }
    }

    void Update()
    {
        if (player != null && agent != null && agent.isActiveAndEnabled)
        {
            agent.SetDestination(player.position);
        }
    }
}
