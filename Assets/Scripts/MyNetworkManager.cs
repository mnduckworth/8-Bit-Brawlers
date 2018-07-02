using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager {

    public void MyStartHost()
    {
        StartHost();
    }

	public override void OnStartHost()
    {
    }

    public override void OnStartClient(NetworkClient client)
    {
        InvokeRepeating("Connecting", 0f, 1f);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        CancelInvoke();
    }

    void Connecting()
    {
        Debug.Log(".");
    }
}
