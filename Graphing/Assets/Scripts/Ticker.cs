using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ticker : MonoBehaviour
{
	float tickerSpeed = 3f;

	public TMP_Text priceText;
	public float priceDelta;

	Grapher grapher;

	public bool runTicker = true;

	public float rangeMax = 0.5f;
	public float rangeMin = -0.5f;

	public TMP_Text balanceText;
	public float balance = 1;

	// Start is called before the first frame update
	void Start()
    {
		if (grapher == null)
		{
			grapher = GetComponent<Grapher>();
		}

		StartCoroutine(startupRoutine());
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey("space"))
		{
			tickerSpeed = .1f;
		}
		else
		{
			tickerSpeed = 1f;
		}
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

			Debug.Log(x + " " + y);

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

				Transform prevPoint = points[points.Count - 1].transform;
				float x = prevPoint.position.x + grapher.pointDistX;
				float y = prevPoint.position.y + Random.Range(rangeMax, rangeMin) / grapher.yMax * grapher.size.y;
				if (y < 0 && priceDelta <= 0)
				{
					y = prevPoint.position.y;
				}
				grapher.plotPoint(x, y, false);

				grapher.removePoint(points[0]);

				grapher.drawTweens();

				float priceFloat = Mathf.Round(100 * (priceDelta + (y * grapher.yMax) / grapher.size.y)) / 100;
				priceText.text = "$" + priceFloat.ToString();
			}

			yield return null;
			time += Time.deltaTime;

		}
	}
}
