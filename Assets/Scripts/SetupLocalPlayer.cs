using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class SetupLocalPlayer : NetworkBehaviour {

    public Text nameLabel;
    public Text namePrefab;
    public Transform namePosition;

    [SyncVar(hook = "OnSetScale")]
    private Vector3 localScale;

    [Command]
    public void CmdSetScale(Vector3 scale)
    {
        this.localScale = scale;
    }

    private void OnSetScale(Vector3 scale)
    {
        this.localScale = scale;
        this.transform.localScale = scale;
    }

    private void Start()
    {
        if (isLocalPlayer)
        {
            GetComponent<PlayerController>().enabled = true;
        }
        else
        {
            GetComponent<PlayerController>().enabled = false;
        }

        GameObject canvas = GameObject.FindWithTag("MainCanvas");
        nameLabel = Instantiate(namePrefab, Vector2.zero, Quaternion.identity) as Text;
        nameLabel.transform.SetParent(canvas.transform, false);
    }

    private void Update()
    {
        CmdSetScale(this.transform.localScale);
        Vector2 nameLabelPosition = namePosition.position;
        nameLabel.transform.position = nameLabelPosition;
    }

    private void OnDestroy()
    {
        if (nameLabel != null)
            Destroy(nameLabel.gameObject);
    }
}
