using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeToggler : MonoBehaviour
{
	RectTransform rt;

	Vector2 inPos;
	Vector2 outPos;

	public float animTime;

	public bool verticalSwipe;
	public bool startsFromBottomorLeft;
	public bool startToggledOff;

	bool toggledOn;

	// Start is called before the first frame update
	void Start()
	{
		if (rt == null)
		{
			rt = GetComponent<RectTransform>();
		}

		inPos = transform.parent.position;

		if (verticalSwipe && startsFromBottomorLeft)
		{
			outPos = new Vector2(inPos.x, inPos.y - rt.sizeDelta.y);
		}
		else if (!verticalSwipe && startsFromBottomorLeft)
		{
			outPos = new Vector2(inPos.x - rt.sizeDelta.x, inPos.y);
		}
		else if (verticalSwipe && !startsFromBottomorLeft)
		{
			outPos = new Vector2(inPos.x, inPos.y + rt.sizeDelta.y);
		}
		else
		{
			outPos = new Vector2(inPos.x + rt.sizeDelta.x, inPos.y);
		}

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
		if (Input.GetKeyDown("a"))
		{
			if (toggledOn)
			{
				StartCoroutine(toggleOut());
			}
			else
			{
				StartCoroutine(toggleIn());
			}
		}
	}

	public IEnumerator toggleIn()
	{
		float time = 0;
		while (time < animTime)
		{
			transform.position = Vector2.Lerp(outPos, inPos, time / animTime);

			yield return null;
			time += Time.deltaTime;
		}
		transform.position = inPos;
		toggledOn = true;
	}

	public IEnumerator toggleOut()
	{
		float time = 0;
		while (time < animTime)
		{
			transform.position = Vector2.Lerp(inPos, outPos, time / animTime);

			yield return null;
			time += Time.deltaTime;
		}
		transform.position = outPos;
		toggledOn = false;
	}
}
