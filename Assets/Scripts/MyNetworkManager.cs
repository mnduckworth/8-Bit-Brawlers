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
        Debug.Log("Host started at " + Time.timeSinceLevelLoad);
    }

    public override void OnStartClient(NetworkClient client)
    {
        Debug.Log(Time.timeSinceLevelLoad + " Client start requested.");
        InvokeRepeating("Connecting", 0f, 1f);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log(Time.timeSinceLevelLoad + " Client is connected to IP: " + conn.address);
        CancelInvoke();
    }

    void Connecting()
    {
        Debug.Log(".");
    }
}
