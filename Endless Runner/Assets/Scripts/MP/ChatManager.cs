using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public TMP_Text chatBox;
    public TMP_InputField inputField;

    public static ChatManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        chatBox.text = "";
        inputField.ActivateInputField();
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    }

    public void QuitToTitle()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        chatBox.text += string.Format("\t<color=grey><sprite name=Smiley Face>{0} has joined!</color>\n", newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        chatBox.text += string.Format("\t<color=grey><sprite name=Sad Face>{0} has left!</color>\n", otherPlayer.NickName);
    }
}
