using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	Vector2 startPos;
	Vector2 endPos;
	float deltaY;

	Vector2 size;

	Ticker ticker;
	Grapher Grapher;

	bool isOrder = false;
	float currentTickerSpeed;

	// Start is called before the first frame update
	void Start()
	{
		size = GetComponent<RectTransform>().sizeDelta;

		if (ticker == null)
		{
			ticker = FindObjectOfType<Ticker>();
		}

		if (Grapher == null)
		{
			Grapher = FindObjectOfType<Grapher>();
		}
	}


	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			currentTickerSpeed = ticker.tickerSpeed;
			isOrder = false;

			startPos = Input.mousePosition;
			Debug.Log(startPos.x);
			Debug.Log(size.x * .75f);

			if (startPos.x < size.x*.75f)
			{
				isOrder = true;
			}
			ticker.tickerSpeed = 3f;
		}

		if (Input.GetMouseButton(0))
		{
			deltaY = (Input.mousePosition.y - startPos.y) / size.y;

		}

		else if (Input.GetMouseButtonUp(0))
		{
			endPos = Input.mousePosition;
			deltaY = (endPos.y - startPos.y) / size.y;

			ticker.tickerSpeed = currentTickerSpeed;

			if (isOrder)
			{
				ticker.order(deltaY);
			}
			else
			{
				ticker.updateTickerSpeed(deltaY);
			}
		}
		
	}
}
