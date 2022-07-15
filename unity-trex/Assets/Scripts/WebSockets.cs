using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class WebSockets : MonoBehaviour
{
    string command;
    private MoveLowerJaw jawScript;
    
    // provide access to javascript method
    [DllImport("__Internal")]
    private static extern void WebSocketInit(string url);
    
    // recieve message called from javascript
    void RecieveMessage(string message) {
	command = message;
    }

    void Start() {
	WebSocketInit("ws://20.0.152.39:9010/");
	
        GameObject bottomJaw = GameObject.Find("bottom_jaw");
        if (bottomJaw != null) {
            jawScript = bottomJaw.GetComponent<MoveLowerJaw>();
        }

	// WebSocketInit("ws://trex-controller.azurewebsites.net:8081/");
    }
    
    // Update is called once per frame
    void Update()
    {
     	switch (command) {
	case "open":
	    jawScript.OpenJaw();
	    command = "";
	    break;
	case "close":
	    jawScript.CloseJaw();
	    command = "";
	    break;
	case "left":
	    transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * 10;
	    break;
	case "right":
	    transform.position += new Vector3(1, 0, 0) * Time.deltaTime * 10;
	    break;
	case "up":
	    transform.position += new Vector3(0, 0, 1) * Time.deltaTime * 10;
	    break;
	case "down":
	    transform.position += new Vector3(0, 0, -1) * Time.deltaTime * 10;
	    break;
	}   
    }
}
