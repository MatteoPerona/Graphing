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
	public List<float> prices;
	public float volAmp;

	public PlayerData (List<float> nW, float i, float b, float lp, List<float> ps, float vA)
	{
		netWorths = nW;
		invested = i;
		balance = b;
		lastPrice = lp;
		prices = ps;
		volAmp = vA;
	}
}
