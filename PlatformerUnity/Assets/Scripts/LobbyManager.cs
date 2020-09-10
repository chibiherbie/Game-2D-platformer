using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : Photon.PunBehaviour
{
    
    void Awake() 
    {   
        print("trying to connect to photon");
        PhotonNetwork.ConnectUsingSettings("v0.1");
    }

    public override void OnConnectedToMaster()
    {   
        print("Connected to photon");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        print("Joined room");
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        print("Failed to join room, creating one");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4}, TypedLobby.Default);
    }

}
