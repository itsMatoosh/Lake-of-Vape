using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LANDiscovery : NetworkDiscovery 
{
	public override void OnReceivedBroadcast (string fromAddress, string data)
	{
		MatchMaker.instance.OnLANMatchDiscovered (fromAddress, data);
	}
}
