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

	public Vector2 size;
	public Vector2 max;
	public Vector2 min;

	Vector2 refPos;

	public float pointDistX;

	public bool constantWindowSize = true;

	public List<GameObject> points;
	List<GameObject> units;

	public bool ready = false;


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

		updateGraphPos();
	}


	void Update()
	{
		
	}


	void updateGraphPos()
	{
		refPos = transform.position;

		size = GetComponent<RectTransform>().sizeDelta;
		max = new Vector2(refPos.x + size.x / 2, refPos.y + size.y / 2);
		min = new Vector2(refPos.x - size.x / 2, refPos.y - size.y / 2);

		pointDistX = size.x / pointCount;

		ready = true;
	}


	public void generateGraph(List<Vector2> data)
	{
		updateGraphPos();

		foreach (Vector2 v in data)
		{
			plotPoint(v.x, v.y);
		}

		drawTweens();
	}


	public void removePoint(GameObject point)
	{
		points.Remove(point);
		point.GetComponent<Point>().DestroyMe();
	}

	public void purgePoints()
	{
		foreach (GameObject p in points)
		{
			p.GetComponent<Point>().DestroyMe();
		}
		points.Clear();
	}


	public void plotPoint(float x, float y, bool scaleCoords = true)
	{
		float xVal = x;
		float yVal = y;
		if (scaleCoords)
		{
			xVal = min.x + size.x * (x / xMax);
			yVal = min.y + size.y * (y / yMax);
		}
		
		GameObject newPoint = Instantiate(point, new Vector2(xVal, yVal), Quaternion.identity, transform);

		if (gameObject.name == "Net Worth Graph")
		{
			Debug.Log("output vector: " + new Vector2(xVal, yVal));
			Debug.Log(newPoint.transform.position);
		}

		points.Add(newPoint);
		//Debug.Log(xVal + ", " + yVal);

		// updates window size
		if (constantWindowSize && yVal > max.y || yVal < min.y)
		{
			updateWindowPos(yVal);
		}
	}


	float equation(float x)
	{
		float y = x/Random.Range(2, 4);
		return y;
	}


	public void drawTweens()
	{
		List<GameObject> temp = new List<GameObject>();

		// create new segments 
		float meshWidth = point.GetComponent<RectTransform>().sizeDelta.x;

		for (int i = 0; i < points.Count - 2; i++)
		{
			Vector2 p1 = points[i].transform.position;
			Vector2 p2 = points[i + 1].transform.position;

			float distance = Mathf.Sqrt(Mathf.Pow(p2.x - p1.x, 2) + Mathf.Pow(p2.y - p1.y, 2)); // pythag
			Vector2 midPoint = new Vector2((p2.x + p1.x) / 2, (p2.y + p1.y) / 2); // (avg x, avg y)
			float angle = Mathf.Atan((p2.y - p1.y) / (p2.x - p1.x)) * 180 / Mathf.PI; // Arctan(dy/dx) * 180 / pi <- convert rad to deg

			GameObject newUnit = Instantiate(lineUnit, midPoint, Quaternion.Euler(new Vector3(0, 0, angle)), transform);
			newUnit.GetComponent<RectTransform>().sizeDelta = new Vector2(distance, meshWidth);

			temp.Add(newUnit);
		}

		// clear old line segments
		foreach (GameObject u in units)
		{
			u.GetComponent<Unit>().DestroyMe();
		}
		units.Clear();

		units = temp;
	}


	void updateWindowPos(float extreme)
	{
		float yDelta = extreme - max.y;
		
		if (yDelta < 0)
		{
			yDelta = extreme - min.y;
		}

		try
		{
			Ticker ticker = GetComponent<Ticker>();
			if (ticker.priceDelta > 0 || yDelta > 0)
			{
				ticker.priceDelta += yDelta * yMax / size.y;
			}
		}
		catch
		{
		}
		

		foreach (GameObject p in points)
		{
			Vector2 pointPos = p.transform.position;
			p.transform.position = new Vector2(pointPos.x, pointPos.y - yDelta);
		}

	}


	void reScaleWindow()
	{

	}


	IEnumerator smoothMove (Transform obj, Vector2 pos1, Vector2 pos2, float duration)
	{
		float time = 0.0f;
		while (time < duration)
		{
			obj.position = Vector2.Lerp(pos1, pos2, time / duration);
			yield return null;
			time += Time.deltaTime;
		}
		obj.position = pos2;
	}
}
