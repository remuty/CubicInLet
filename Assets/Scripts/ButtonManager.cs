using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private SaveManager saveManager;
    // Start is called before the first frame update
    void Start()
    {
        saveManager = GameObject.FindWithTag("SaveManager").GetComponent<SaveManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadLogin()
    {
        SceneManager.LoadScene("Login");
    }

    public void LoadStart()
    {
        PhotonNetwork.LocalPlayer.NickName = SaveManager.save.userName;
        SceneManager.LoadScene("Start");
    }

    public void LoadCreateCharacter()
    {
        SceneManager.LoadScene("CreateCharacter");
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("Main");
    }

    //セーブして退室
    public void LeaveRoom()
    {
        saveManager.Save();
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        LoadStart();
    }

    public void CreateCharacter(GameObject inputField)
    {
        var input = inputField.GetComponent<TMP_InputField>().text;
        if (input != "")
        {
            SaveManager.save.userName = input;
            LoadStart();
        }
    }
}
