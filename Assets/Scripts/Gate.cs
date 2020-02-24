using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gate : MonoBehaviour
{
    public GameObject gateExit, showKey;
    // Start is called before the first frame update
    void Start()
    {
        showKey.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetPhotonView().IsMine)
            {
                showKey.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    other.transform.position = gateExit.transform.position;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetPhotonView().IsMine)
            {
                showKey.SetActive(false);
            }
        }
    }
}
