using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stock
{
	public float[] price;
	public float[] revenue;

	public float delta; // the sum of % change revenue over the past 4 quarters and the most recent 2
	public float swingVariable; // delta + 1/4 * complimentary deltas - 1/4 * competitor deltas

	public string name;
	public string type;

	public Color32 color;

	string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";


	public Stock()
	{
		nameGen();
		type = new string[] { "a", "b", "c" }[Random.Range(0, 2)];

		colorGen();

		delta = 0f;
		swingVariable = 0f;

		price = new float[50];
		revenue = new float[10];

		updatePrices(0, 1, true);
		updateRevenue(true);
	}


	public void nameGen()
	{
		name = "";
		int numChars = Random.Range(3, 5);
		for (int i = 0; i < numChars; i++)
		{
			name += letters.ToCharArray()[Random.Range(0, 26)];
		}
	}


	public void colorGen()
	{
		float hue = Random.Range(0.05f, 0.95f);
		if (hue >= 0.15f || hue <= 0.50f)
		{
			hue = new float[] { 0.14f, 0.51f }[Random.Range(0, 2)];
		}
		color = Color.HSVToRGB(Random.Range(0.00f, 1.00f), 0.6f, 0.2f);
		Debug.Log("stock color: " + color.ToString());
		//color = Color.HSVToRGB(Random.Range(0, 360), 0.75f, 0.75f);
	}


	public void updatePrices(float ticks, float volatility = 1, bool innit = false)
	{
		List<float> temp = new List<float>();
		if (innit)
		{
			temp.Add(Random.Range(1f, 100f));

			for (int i = 1; i < price.Length; i++)
			{
				temp.Add(temp[i-1] + temp[i - 1] * Random.Range(-0.01f, 0.01f) * volatility);
			}
		}
		else
		{
			temp.Add(price[price.Length - 1]);
			for (int i = 0; i < ticks; i++)
			{
				temp.Add(temp[i - 1] + temp[i - 1] * Random.Range(-0.01f, 0.01f) * volatility);
			}
		}	
	}


	public void updateRevenue(bool innit = false)
	{
		float[] temp = new float[revenue.Length];
		if (innit)
		{
			float innitial = Mathf.Round(100 * Random.Range(100f, 1000000000f)) / 100;

			for (int i = 1; i < revenue.Length; i++)
			{
				temp[i] = Mathf.Round(100 * innitial * (1 + Random.Range(-1f, 1f) * Random.Range(0.01f, 1f))) / 100;
			}
			revenue = temp;
		}

		else
		{
			for (int i = 0; i < revenue.Length-1; i++)
			{
				temp[i] = revenue[i + 1];
			}
			temp[temp.Length - 1] = Mathf.Round(100 * revenue[revenue.Length - 1] * (1 + Random.Range(-1f, 1f) * Random.Range(0.01f, 1f))) / 100;
			revenue = temp;
		}
	}
}
