using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;

public class SaveManager : MonoBehaviour
{
    private string filePath;
    public static SaveData save;

    void Awake()
    {
        filePath = Application.persistentDataPath + "/" + ".savedata.json";
        save = new SaveData();
    }

    void Start()
    {
        Load();
    }

    public void Save()
    {
        save.position = transform.position;
        string json = JsonUtility.ToJson(save);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();
    }

    public void Load()
    {
        if (File.Exists(filePath))
        {
            StreamReader streamReader;
            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();
            save = JsonUtility.FromJson<SaveData>(data);
        }
    }

    //セーブして退室
    public void LeaveRoom()
    {
        Save();
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}
