using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System;
using ExitGames.Client.Photon;

public class GameMan : MonoBehaviourPunCallbacks
{   

    public GameObject playerPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = new Vector3(0, 0, 0);
        PhotonNetwork.Instantiate(playerPrefab.name, position, Quaternion.identity);
        PhotonPeer.RegisterType(typeof(Vector3Int), 242, SerializeVector3Int, DeserializeVector3Int);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        // когда текущий игрок покидает комнату
        SceneManager.LoadScene(0); 
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("Player {0} entered room", newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("Player {0} left room", otherPlayer.NickName);
    }
    public static object DeserializeVector3Int(byte[] data)
    {
        Vector3Int result = new Vector3Int();
        result.x = BitConverter.ToInt32(data, 0);
        result.y = BitConverter.ToInt32(data, 4);
        result.z = BitConverter.ToInt32(data, 9);

        return result;
    }

    public static byte[] SerializeVector3Int(object obj)
    {
        Vector3Int vector = (Vector3Int)obj;

        byte[] result = new byte[12];

        BitConverter.GetBytes(vector.x).CopyTo(result, 0);
        BitConverter.GetBytes(vector.y).CopyTo(result, 4);
        BitConverter.GetBytes(vector.z).CopyTo(result, 9);

        return result;

    }


    
}   
