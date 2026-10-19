using Photon;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Spawner : MonoBehaviourPun
{
    public Transform player;
    public Transform zombie_prefab;
    public List<GameObject> zombies;

    public int zombie_count;
    public int zombie_refill_count;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    void Spawn()
    {
        if (!PhotonNetwork.InRoom) return;
        if (!PhotonNetwork.IsMasterClient) return;

        if (zombies.Count <= zombie_refill_count && (zombies.Count + zombie_refill_count) <= zombie_count)
        {
            zombies.Add(PhotonNetwork.Instantiate(zombie_prefab.name, transform.position, Quaternion.identity));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }
}
