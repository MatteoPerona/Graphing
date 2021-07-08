using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
	public List<float> netWorths;
	public float invested;
	public float balance;

	public PlayerData (List<float> nWs, float i, float b)
	{
		netWorths = nWs;
		invested = i;
		balance = b;
	}
}
