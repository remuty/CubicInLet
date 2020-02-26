using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Chat : MonoBehaviour, IPunObservable
{
    public GameObject chatView, textPrefab, content;

    private PhotonView photonView;
    private ScrollRect scroll;
    private RectTransform contentSize;
    private string text;
    private float size;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        scroll = chatView.GetComponent<ScrollRect>();
        contentSize = content.GetComponent<RectTransform>();
        chatView.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //表示・非表示
            chatView.SetActive(!chatView.activeSelf);
        }
        if (contentSize.sizeDelta.y != size)
        {
            //一番下にスクロール
            scroll.verticalNormalizedPosition = 0;
            size = contentSize.sizeDelta.y;
        }
    }

    public void SendMessage(InputField input)
    {
        if (input.text != "")
        {
            photonView.RequestOwnership();
            text = PhotonNetwork.LocalPlayer.NickName + ": " + input.text;
            input.text = "";
            input.ActivateInputField();
            ChatUpdate();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //オーナー
            stream.SendNext(text);
        }
        else
        {
            //オーナー以外
            text = (string)stream.ReceiveNext();
            ChatUpdate();
        }
    }

    void ChatUpdate()
    {
        var message = Instantiate(textPrefab, content.transform);
        message.GetComponent<Text>().text = text;
    }
}
