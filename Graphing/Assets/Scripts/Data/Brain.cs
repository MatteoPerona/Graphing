using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
	public List<float> netWorths;

	Ticker ticker;
	Grapher graph;

	float time = 0;

	public bool loadSave;


	// Start is called before the first frame update
	void Start()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;

		if (netWorths == null)
		{
			netWorths = new List<float>();
		}

		if (ticker == null)
		{
			ticker = FindObjectOfType<Ticker>();
		}
		if (graph == null)
		{
			graph = FindObjectOfType<Grapher>();
		}
	}


	// Update is called once per frame
	void Update()
	{
		time += Time.deltaTime;
		if (time == 60)
		{
			netWorths.Add(ticker.balance + ticker.investment);
			time = 0;
		}

		if (netWorths.Count == 100)
		{
			List<float> temp = new List<float>();
			for (int i = 0; i < netWorths.Count/2; i++)
			{
				int bal = (int) Mathf.Round((netWorths[i] + netWorths[i + 1]) / 2);
				temp.Add(bal);
			}
			netWorths = temp;
		}
	}


	void OnApplicationFocus(bool focus)
	{
		if (focus)
		{
			load();
		}
		else
		{
			save();
		}
	}


	void OnApplicationQuit()
	{
		save();
	}


	void save()
	{
		PlayerData data = new PlayerData(netWorths, ticker.investment, ticker.balance, ticker.price, ticker.prices, ticker.volumeAmp);
		SaveData.SaveGame(data);
	}
	
	void load()
	{
		PlayerData data = SaveData.LoadGame();
		Debug.Log("netWorths" + data.netWorths + ", balance " + data.balance + ", invested " + data.invested + ", lastPrice " + data.lastPrice);
		if (loadSave)
		{
			netWorths = data.netWorths;

			if (ticker == null)
			{
				ticker = FindObjectOfType<Ticker>();
			}
			ticker.balance = data.balance;
			ticker.investment = data.invested;
			ticker.price = data.lastPrice;
			ticker.updatePlayerData();

			//figure out how to load graph here 
			if (graph == null)
			{
				graph = FindObjectOfType<Grapher>();
			}
			
		}
	}
}
