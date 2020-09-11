using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;


public class LobbyManager : MonoBehaviourPunCallbacks
{
    GameObject player;
    private void Start() {
        PhotonNetwork.NickName = "Player " + Random.Range(1000, 9000);
        Debug.Log("Player's name is set to " + PhotonNetwork.NickName);

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion= "v0.1";
        PhotonNetwork.ConnectUsingSettings();
    }
    
    private void Log(string message)
    {
        Debug.Log(message);
    }

    public override void OnConnectedToMaster()
    {   
        Debug.Log("Connected to Master");
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions() { MaxPlayers = 4});
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Log("Joined the Room");

        PhotonNetwork.LoadLevel("Level1");
    }

    public void play(){
        SceneManager.LoadScene(4);
    }
}

