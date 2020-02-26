using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Chat : MonoBehaviour, IPunObservable
{
    public GameObject textPrefab, content;
    private PhotonView photonView;
    private string text;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendMessage(InputField input)
    {
        if (input.text != "")
        {
            photonView.RequestOwnership();
            text = PhotonNetwork.LocalPlayer.NickName + ": " + input.text;
            input.text = "";

            var chat = Instantiate(textPrefab, content.transform);
            chat.GetComponent<Text>().text = text;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // オーナーの場合
        if (stream.IsWriting)
        {
            stream.SendNext(text);
        }
        // オーナー以外の場合
        else
        {
            text = (string)stream.ReceiveNext();
            var chat = Instantiate(textPrefab, content.transform);
            chat.GetComponent<Text>().text = text;
        }
    }
}
