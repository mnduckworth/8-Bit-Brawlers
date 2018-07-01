using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class Player : NetworkBehaviour {

    private Vector2 inputValue;
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }

        if (CrossPlatformInputManager.GetButton("Fire1"))
        {

        }

        transform.Translate(inputValue);
	}

    void Run()
    {
        inputValue.x = CrossPlatformInputManager.GetAxis("Horizontal");
        inputValue.y = CrossPlatformInputManager.GetAxis("Vertical");
    }
}
