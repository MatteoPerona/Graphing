using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapher : MonoBehaviour
{
	public GameObject point;
	public int pointCount;

	public GameObject lineUnit;

	public float yMax = 9;
	public float xMax = 16;

	public float rangeMin = -0.5f;
	public float rangeMax = 0.5f;

	Vector2 size;
	Vector2 max;
	Vector2 min;

	float pointDistX;

	public bool runTicker = true;

	List<GameObject> points;
	List<GameObject> units;


	void Start()
	{
		if (points == null)
		{
			points = new List<GameObject>();
		}

		if (units == null)
		{
			units = new List<GameObject>();
		}

		size = GetComponent<RectTransform>().sizeDelta;
		max = new Vector2(size.x/2, size.y / 2);
		min = new Vector2(-size.x / 2, -size.y / 2);

		generateGraph();
		drawTweens();
		StartCoroutine(ticker());
	}


	void Update()
	{
		
	}


	void generateGraph()
	{
		float prevY = 0;
		for (int p = 0; p < pointCount; p++)
		{
			float x = (p * xMax) / pointCount;
			float y = prevY + Random.Range(rangeMax, rangeMin);

			plotPoint(x, y);

			prevY = y;
		}

		pointDistX = Mathf.Abs(points[0].transform.position.x - points[1].transform.position.x);
	}


	IEnumerator ticker ()
	{
		float time = 0.0f;

		while (runTicker)
		{
			if (time >= .2f)
			{
				time = 0.0f;

				foreach (GameObject p in points)
				{
					p.transform.position = new Vector3(p.transform.position.x - pointDistX, p.transform.position.y);
				}

				Transform prevPoint = points[points.Count-1].transform;
				float x = prevPoint.position.x + pointDistX;
				float y = prevPoint.position.y + Random.Range(rangeMax, rangeMin) / yMax * size.y;
				plotPoint(x, y, false);

				removePoint(points[0]);

				drawTweens();
			}

			yield return null;
			time += Time.deltaTime;
			
		}
	}


	void removePoint(GameObject point)
	{
		points.Remove(point);
		point.GetComponent<Point>().DestroyMe();
	}


	void plotPoint(float x, float y, bool scaleCoords = true)
	{
		float xVal = x;
		float yVal = y;
		if (scaleCoords)
		{
			xVal = size.x * (x / xMax);
			yVal = size.y * (y / yMax);
		}
		else
		{

		}

		GameObject newPoint = Instantiate(point, new Vector2 (xVal, yVal), Quaternion.identity, transform);
		points.Add(newPoint);
	}


	float equation(float x)
	{
		float y = x/Random.Range(2, 4);
		return y;
	}

	void drawTweens()
	{
		// clear old line segments
		foreach (GameObject u in units)
		{
			u.GetComponent<Unit>().DestroyMe();
		}
		units.Clear();

		// create new segments 
		float meshWidth = point.GetComponent<RectTransform>().sizeDelta.x/2;
		
		for (int i = 0; i < points.Count-2; i++)
		{
			Vector2 p1 = points[i].transform.position;
			Vector2 p2 = points[i + 1].transform.position;

			float distance = Mathf.Sqrt(Mathf.Pow(p2.x - p1.x, 2) + Mathf.Pow(p2.y - p1.y, 2)); // pythag
			Vector2 midPoint = new Vector2 ((p2.x + p1.x) / 2, (p2.y + p1.y) / 2); // (avg x, avg y)
			float angle = Mathf.Atan((p2.y - p1.y) / (p2.x - p1.x)) * 180 / Mathf.PI; // Arctan(dy/dx) * 180 / pi <- convert rad to deg

			GameObject newUnit = Instantiate(lineUnit, midPoint, Quaternion.Euler(new Vector3 (0, 0, angle)), transform);
			newUnit.GetComponent<RectTransform>().sizeDelta = new Vector2 (distance, meshWidth);

			units.Add(newUnit);
		}
	}


}
