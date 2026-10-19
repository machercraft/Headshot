using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class Zombie_AI : MonoBehaviourPun
{
    public NavMeshAgent agent;
    public float space;
    public Transform fallow;
    public string player_name;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!PhotonNetwork.InRoom) if (!PhotonNetwork.IsMasterClient) this.GetComponent<Zombie_AI>().enabled = false;
        
        //fallow = GameObject.Find(player_name).transform;
    }
      
    // Update is called once per frame
    void Update()
    {
        //agent.destination = fallow.position;
        agent.stoppingDistance = space;
    }
}
