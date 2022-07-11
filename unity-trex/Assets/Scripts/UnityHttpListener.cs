using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Net;
using System.Threading;

public class UnityHttpListener : MonoBehaviour
{

	private HttpListener listener;
	private Thread listenerThread;
    private MoveLowerJaw jawScript;

	void Start ()
	{
		listener = new HttpListener ();
		listener.Prefixes.Add ("http://localhost:4444/");
		listener.Prefixes.Add ("http://127.0.0.1:4444/");
		listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
		listener.Start ();

		listenerThread = new Thread (startListener);
		listenerThread.Start ();
		Debug.Log ("Server Started");

        GameObject bottomJaw = GameObject.Find("bottom_jaw");
        if (bottomJaw != null) {
            jawScript = bottomJaw.GetComponent<MoveLowerJaw>();
        }
	}

	void Update ()
	{		
	}

	private void startListener ()
	{
		while (true) {               
			var result = listener.BeginGetContext (ListenerCallback, listener);
			result.AsyncWaitHandle.WaitOne ();
		}
	}

	private void ListenerCallback (IAsyncResult result)
	{				
		var context = listener.EndGetContext (result);		

		Debug.Log ("Method: " + context.Request.HttpMethod);
		Debug.Log ("LocalUrl: " + context.Request.Url.LocalPath);


		if (context.Request.QueryString.AllKeys.Length > 0)
			foreach (var key in context.Request.QueryString.AllKeys) {
				Debug.Log ("Key: " + key + ", Value: " + context.Request.QueryString.GetValues (key) [0]);
			}

		if (context.Request.HttpMethod == "POST") {	
            if (context.Request.Url.LocalPath == "/jaw/close") {
                Debug.Log("close jaw");
                jawScript.CloseJaw();
            } else if (context.Request.Url.LocalPath == "/jaw/open") {
                Debug.Log("open jaw");
                jawScript.OpenJaw();
            }
		//	Thread.Sleep (1000);
			var data_text = new StreamReader (context.Request.InputStream, 
				                context.Request.ContentEncoding).ReadToEnd ();
			Debug.Log (data_text);
		}

		context.Response.Close ();
	}

}