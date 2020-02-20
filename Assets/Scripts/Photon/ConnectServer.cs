using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ConnectServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField input;
    // Start is called before the first frame update
    void Start()
    {
        // PhotonServerSettingsに設定した内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーに接続した時
    public override void OnConnectedToMaster()
    {
        JoinLobby();
    }

    // ロビーに入る
    private void JoinLobby()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public void SetPlayerName()
    {
        PhotonNetwork.LocalPlayer.NickName = input.text;
    }
}
