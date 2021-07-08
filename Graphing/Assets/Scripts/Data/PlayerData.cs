using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
	public List<float> netWorths;
	public float invested;
	public float balance;
	public float lastPrice;

	public PlayerData (List<float> nW, float i, float b, float lp)
	{
		netWorths = nW;
		invested = i;
		balance = b;
		lastPrice = lp;
	}
}
