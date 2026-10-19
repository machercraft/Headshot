using Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Network_Manneger : MonoBehaviourPunCallbacks
{
    public string version = "0.1";
    public Transform player_prefab;
    public List<Transform> spawn_points;

    public bool connected = false;
    
    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PhotonNetwork.GameVersion = version;
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        RoomOptions room_options = new RoomOptions();
        string room_id = Guid.NewGuid().ToString("D");
        PhotonNetwork.JoinOrCreateRoom("A",room_options,TypedLobby.Default);
        Debug.Log("Connected to main sever !");
    }
    public override void OnJoinedRoom()
    {
        Random random = new Random();
        int ran = random.Next(spawn_points.Count);
        player = PhotonNetwork.Instantiate(player_prefab.name, spawn_points[ran].position,Quaternion.identity);

        connected = true;
        Debug.Log("Connected to room !");
    }
    public override void OnLeftRoom()
    {
        if (connected)
        {
            PhotonNetwork.Destroy(player);
        }
        connected = false;
        Debug.Log("left room !");
    }

    // Update is called once per frame
    void Update() 
    { 
        
    }
}
