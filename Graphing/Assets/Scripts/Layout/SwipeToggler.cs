using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[ExecuteInEditMode]
public class SwipeToggler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	RectTransform rt;

	Vector2 inPos;
	Vector2 outPos;

	public float animTime;

	public bool verticalSwipe;
	public bool startsFromBottomorLeft;
	public bool startToggledOff;

	public float percentProtrusion;

	float thresholdUpper;
	float thresholdLower;

	public bool toggledOn;

	Vector2 ogPos;

	bool coroutineCallable;


	// Start is called before the first frame update
	void Start()
	{
		if (rt == null)
		{
			rt = GetComponent<RectTransform>();
		}

		inPos = transform.parent.position;

		coroutineCallable = true;

		findOutPos();

		toggledOn = true;
		if (startToggledOff)
		{
			transform.position = outPos;
			toggledOn = false;
		}
	}

	// Update is called once per frame
	void Update()
	{
	}


	public void OnBeginDrag(PointerEventData data)
	{
		ogPos = data.position;
	}

	public void OnDrag(PointerEventData data)
	{
		Vector3 deltaPos = data.position - ogPos;
		if (verticalSwipe)
		{
			Vector3 newPos = transform.position + new Vector3(0, deltaPos.y, 0);
			if (transform.position.y >= thresholdLower && transform.position.y <= thresholdUpper)
			{
				transform.position = newPos;
			}
		}
		else
		{
			Vector3 newPos = transform.position + new Vector3(deltaPos.x, 0, 0);
			if (transform.position.x >= thresholdLower && transform.position.x <= thresholdUpper)
			{
				transform.position = newPos;
			}
		}
		ogPos = data.position;
	}


	public void OnEndDrag(PointerEventData data)
	{
		float inDist = Vector2.Distance(transform.position, inPos);
		float outDist = Vector2.Distance(transform.position, outPos);
		if (inDist*2 > outDist && coroutineCallable)
		{
			StartCoroutine(toggleOut());
		}
		else if (coroutineCallable)
		{
			StartCoroutine(toggleIn());
		}
	}


	public IEnumerator toggleIn()
	{
		coroutineCallable = false;

		ogPos = transform.position;
		float time = 0;
		while (time < animTime)
		{
			transform.position = Vector2.Lerp(ogPos, inPos, time / animTime);

			yield return null;
			time += Time.deltaTime;
		}
		transform.position = inPos;
		toggledOn = true;

		coroutineCallable = true;
	}

	public IEnumerator toggleOut()
	{
		coroutineCallable = false;

		ogPos = transform.position;
		float time = 0;
		while (time < animTime)
		{
			transform.position = Vector2.Lerp(ogPos, outPos, time / animTime);

			yield return null;
			time += Time.deltaTime;
		}
		transform.position = outPos;
		toggledOn = false;

		coroutineCallable = true;
	}

	void findOutPos()
	{
		if (verticalSwipe && startsFromBottomorLeft)
		{
			outPos = new Vector2(inPos.x, (inPos.y - rt.sizeDelta.y) * (1-percentProtrusion));
			thresholdUpper = inPos.y;
			thresholdLower = outPos.y;
		}
		else if (!verticalSwipe && startsFromBottomorLeft)
		{
			outPos = new Vector2((inPos.x - rt.sizeDelta.x) * (1 - percentProtrusion), inPos.y);
			thresholdUpper = inPos.x;
			thresholdLower = outPos.x;
		}
		else if (verticalSwipe && !startsFromBottomorLeft)
		{
			outPos = new Vector2(inPos.x, (inPos.y + rt.sizeDelta.y) * (1 - percentProtrusion));
			thresholdUpper = outPos.y;
			thresholdLower = inPos.y;
		}
		else
		{
			outPos = new Vector2((inPos.x + rt.sizeDelta.x) * (1 - percentProtrusion), inPos.y);
			thresholdUpper = outPos.x;
			thresholdLower = inPos.x;
		}
	}
}
