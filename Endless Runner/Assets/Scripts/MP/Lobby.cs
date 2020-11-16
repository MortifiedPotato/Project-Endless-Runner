using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject chat;
    [SerializeField] GameObject connecting;

    public void StartMultiplayer()
    {
        PlayerPrefs.SetInt("SP", 0);

        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = "DLC1";
        PhotonNetwork.NickName = "Player " + Random.Range(1, 10).ToString();

        // -> Name server
        // -> Master server
        // -> Game server
        // -> Room server

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("New Room", new Photon.Realtime.RoomOptions() { MaxPlayers = 2 }, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        chat.SetActive(true);
        connecting.gameObject.SetActive(false);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            // proceed to the game
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            // proceed to the game
            //PhotonNetwork.LoadLevel(1);
        }
    }
}
