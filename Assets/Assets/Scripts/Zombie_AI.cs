using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Zombie_AI : MonoBehaviourPun
{
    public NavMeshAgent agent;
    public float space;

    public float fallow_min_dis;

    public string player_tag;

    public Vector3 destination;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if (!PhotonNetwork.InRoom) if (!PhotonNetwork.IsMasterClient) this.GetComponent<Zombie_AI>().enabled = false;

        if (!PhotonNetwork.InRoom) return;
        if (!PhotonNetwork.IsMasterClient) agent.enabled = false;
        if (!PhotonNetwork.IsMasterClient) gameObject.GetComponent<Zombie_AI>().enabled = false;

        agent.stoppingDistance = space;
    }

    // Update is called once per frame


    void Update()
    {
        if (!PhotonNetwork.InRoom) return;
        if (!PhotonNetwork.IsMasterClient) return;

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");



        float r_dis = fallow_min_dis;

        foreach (var ob in gameObjects)
        {
            float dis = Vector3.Distance(transform.position,ob.transform.position);
            Debug.Log(ob.name+" "+dis);

            if (dis < r_dis)
            {
                r_dis = dis;
                destination = ob.transform.position;
            }

        }

        agent.destination = destination;
    }
}
