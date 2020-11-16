using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chatter : MonoBehaviourPunCallbacks, IPunObservable, IChatControls
{
    [PunRPC]
    public void SendChat(string _chatMessage, PhotonMessageInfo _info)
    {
        ChatManager.Instance.chatBox.text += string.Format("{0}<color=blue>{1}</color>: {2}\n", _info.Sender.IsMasterClient ? "<sprite name=Cool face>" : "", _info.Sender.NickName, _chatMessage);
    }

    public void SendChat(string _chatMessage)
    {
        ChatManager.Instance.chatBox.text += string.Format("{0}<color=blue>{1}</color>: {2}\n", PhotonNetwork.IsMasterClient ? "<sprite name=Cool face>" : "", PhotonNetwork.NickName, _chatMessage);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // Return something!
    }

    public void Send()
    {
        if (ChatManager.Instance.inputField.text.Length > 0)
        {
            photonView.RPC("SendChat", RpcTarget.All, ChatManager.Instance.inputField.text);
            //SendChat(ChatManager.Instance.inputField.text); //Optional
            ChatManager.Instance.inputField.text = "";
            ChatManager.Instance.inputField.ActivateInputField();
        }
    }
}

public interface IChatControls
{
    void Send();
}
