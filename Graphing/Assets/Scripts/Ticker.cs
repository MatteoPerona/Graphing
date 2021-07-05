using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ticker : MonoBehaviour
{
	public float tickerSpeed = 1f;

	float price;
	public TMP_Text priceText;
	public float priceDelta;

	Grapher grapher;

	public bool runTicker = true;

	public float rangeMax = 0.5f;
	public float rangeMin = -0.5f;

	public TMP_Text balanceText;
	public float balance = 1;

	public TMP_Text investmentText;
	public float investment;

	// Start is called before the first frame update
	void Start()
    {
		if (grapher == null)
		{
			grapher = GetComponent<Grapher>();
		}

		StartCoroutine(startupRoutine());

		tickerSpeed = 1;
	}

    // Update is called once per frame
    void Update()
    {
		
	}


	public IEnumerator startupRoutine()
	{
		while (!grapher.ready)
		{
			yield return null;
		}

		List<Vector2> data = new List<Vector2>();
		float prevY = 0;
		for (int p = 0; p < grapher.pointCount; p++)
		{
			float x = (p * grapher.xMax) / grapher.pointCount;
			float y = prevY + Random.Range(.5f, -.5f);

			data.Add(new Vector2(x, y));

			prevY = y;
		}

		grapher.generateGraph(data);
		StartCoroutine(ticker());
	}

	IEnumerator ticker()
	{
		float time = 0.0f;

		while (runTicker)
		{
			if (time >= tickerSpeed)
			{
				time = 0.0f;

				List<GameObject> points = grapher.points;
				foreach (GameObject p in points)
				{
					p.transform.position = new Vector3(p.transform.position.x - grapher.pointDistX, p.transform.position.y);
				}

				float oldPrice = price;

				Transform prevPoint = points[points.Count - 1].transform;
				float x = prevPoint.position.x + grapher.pointDistX;
				float y = prevPoint.position.y + Random.Range(rangeMax, rangeMin) / grapher.yMax * grapher.size.y;

				price = Mathf.Round(100 * (priceDelta + (y * grapher.yMax) / grapher.size.y)) / 100;

				if (price <= 0)
				{
					y = prevPoint.position.y;
					price = Mathf.Round(100 * (priceDelta + (y * grapher.yMax) / grapher.size.y)) / 100;
				}
				grapher.plotPoint(x, y, false);

				grapher.removePoint(points[0]);

				grapher.drawTweens();
				
				priceText.text = "$" + price.ToString();

				if (investment > 0)
				{
					investment *= (price / oldPrice);
					investmentText.text = "$" + (Mathf.Round(investment * 100) / 100).ToString();
				}
			}

			yield return null;
			time += Time.deltaTime;

		}
	}


	public void order (float percentage)
	{
		//scale percentage down for better control
		if (percentage >= .75f)
		{
			percentage = 1;
		}
		else if (percentage <= -.75f)
		{
			percentage = -1;
		}
		else
		{
			percentage = percentage / .75f;
		}

		if (percentage > 0)
		{
			float vol = balance * percentage;
			balance -= vol;
			investment += vol;
		}
		else
		{
			percentage = Mathf.Abs(percentage);
			float vol = investment * percentage;
			investment -= vol;
			balance += vol;
		}
		balanceText.text = "$" + (Mathf.Round(balance * 100) / 100).ToString();
		investmentText.text = "$" + (Mathf.Round(investment * 100) / 100).ToString();
	}

	public string orderVolumeString(float percentage)
	{
		float vol;

		//scale percentage down for better control
		if (percentage >= .75f)
		{
			percentage = 1;
		}
		else if (percentage <= -.75f)
		{
			percentage = -1;
		}
		else
		{
			percentage = percentage / .75f;
		}

		if (percentage > 0)
		{
			vol = balance * percentage;
			//Debug.Log("+$" + vol.ToString());
			return "+$" + (Mathf.Round(vol * 100) / 100).ToString();
		}
		
		percentage = Mathf.Abs(percentage);
		vol = investment * percentage;
		//Debug.Log("-$" + vol.ToString());
		return "-$" + (Mathf.Round(vol * 100) / 100).ToString();
	}

	public void updateTickerSpeed(float percentage)
	{
		float delta = percentage * -1.5f;
		float temp = delta + tickerSpeed;
		if (delta + tickerSpeed > 1)
		{
			tickerSpeed = 1;
		}
		else if (delta + tickerSpeed < .1f)
		{
			tickerSpeed = .1f;
		}
		else
		{
			tickerSpeed += delta;
		}
	}

	public string deltaSpeedString(float percentage, float currentTickerSpeed)
	{
		float delta = percentage * -1.5f + currentTickerSpeed;
		if (delta > 1)
		{
			delta = 1;
		}
		else if (delta < .1f)
		{
			delta = .1f;
		}
		return (Mathf.Round(delta * 100) / 100).ToString() + " s";
	}
}
