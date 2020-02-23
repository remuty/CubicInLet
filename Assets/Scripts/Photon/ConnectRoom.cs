using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class ConnectRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private string[] characterName;
    private void Start()
    {
        // "room"という名前のルームに参加する（ルームが無ければ作成してから参加する）
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    // マッチングが成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        var pos = SaveManager.save.position;
        var n = SaveManager.save.characterNum;
        // マッチング後、自分自身のネットワークオブジェクトを生成する
        PhotonNetwork.Instantiate(characterName[n], pos, Quaternion.identity);
    }
}
