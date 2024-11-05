using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

namespace fofulab
{
    public class HapticMakerManager : MonoBehaviour
    {
		private static HapticMakerManager instance = null;
		public static HapticMakerManager Instance { get { return instance; }}

		private WebSocket ws;
		private bool wsConnected = false;

		[SerializeField, Range(5, 15)]
		private int fps = 10;
		public int FPS { get { return fps; } }
		[MyReadOnly, SerializeField]
		private HapticMaker[] hapticMakers;
		private Dictionary<string, int> hmDict;

		private void Awake() {
			if (instance == null) {
				instance = this;
			}
			else {
				Destroy(this.gameObject);
			}
			DontDestroyOnLoad(this.gameObject);
		}

		// Start is called before the first frame update
		void Start() {
			ws = new WebSocket("ws://localhost:7786");
			ws.OnOpen += ConnectedHandler;
			ws.OnClose += DisConnectedHandler;
			ws.OnMessage += MessageHandler;
			ws.OnError += ErrorHandler;

			StartCoroutine(ConnectCoroutine());

			hmDict = new Dictionary<string, int>();
			hapticMakers = FindObjectsOfType<HapticMaker>();
			for (int i = 0; i < hapticMakers.Length; i++) {
				hmDict.Add(hapticMakers[i].GetName(), i);
			}

			StartCoroutine(WriteDataCoroutine());
		}

		private void OnApplicationQuit() {
			StopAllCoroutines();
			foreach (HapticMaker hm in hapticMakers) {
				hm.Reset();
				Send(JsonUtility.ToJson(hm.Data));
			}
			ws.Close();
		}

		IEnumerator ConnectCoroutine() {
			while(!wsConnected) {
				ws.Connect();
				if (wsConnected) {
					Debug.Log("Connect to HM websocket success!");
				}
				else {
					Debug.Log("Connect to HM websocket fail, retry in 3 seconds");
				}
				yield return new WaitForSeconds(3f);
			}
		}

		public void Send(string strToSend) {
			if (wsConnected == false || ws == null)
				return;
			ws.Send(strToSend);
		}

		public void ConnectedHandler(object sender, System.EventArgs e) {
			wsConnected = true;
		}
		public void DisConnectedHandler(object sender, CloseEventArgs e) {
			wsConnected = false;
		}

		public void ErrorHandler(object sender, ErrorEventArgs e) {
			Debug.Log(e);
		}

		public void MessageHandler(object sender, MessageEventArgs e) {
			JSONObject data = new JSONObject(e.Data);
			foreach (JSONObject val in data.list) {
				string name = val["name"].str;
				foreach (HapticMaker hm in hapticMakers) {
					if (hm.GetName() == name) {
						UInt32 v = (UInt32)val["value"].n;
						hm.updateInput(v);
						break;
					}
				}
			}
		}

		IEnumerator WriteDataCoroutine() {
			while (true) {
				yield return new WaitForSeconds(1f / fps);
				foreach (HapticMaker hm in hapticMakers) {
					Send(JsonUtility.ToJson(hm.Data));
				}
			}
		}
	}
}