using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutController : MonoBehaviour
{
	public List<GameObject> childOrder;
	public List<float> occupancy; // percent occupancy of the parent for each child
	public bool controlsYAxis;
	public bool autoFit;


	// Start is called before the first frame update
	void Awake()
	{
		if (childOrder == null)
		{
			childOrder = new List<GameObject>();
		}
		if (occupancy == null)
		{
			occupancy = new List<float>();
		}

		if(autoFit)
		{
			fitToScreen(false);
		}

		// checking that all inputted values add up to 1
		sumToOne(occupancy);
		refreshLayout();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void refreshLayout() // updates sizes & positions of children
	{
		Vector2 size = GetComponent<RectTransform>().sizeDelta;
		float focus = size.x;
		if (controlsYAxis)
		{
			focus = size.y;
		}

		float buffer = 0;
		for (int i = 0; i < childOrder.Count; i++)
		{
			RectTransform currentRect = childOrder[i].GetComponent<RectTransform>();
			currentRect.anchorMax = new Vector2(.5f, .5f);
			currentRect.anchorMin = new Vector2 (.5f, .5f);
			currentRect.sizeDelta = new Vector2(focus * occupancy[i], size.y);
			if (controlsYAxis)
			{
				currentRect.sizeDelta = new Vector2(size.x, focus * occupancy[i]);

				childOrder[i].transform.localPosition = new Vector2(0, size.y / 2 - buffer - currentRect.sizeDelta.y / 2);
				buffer += currentRect.sizeDelta.y;
			}	
			else
			{
				childOrder[i].transform.localPosition = new Vector2(size.x / 2 - buffer - currentRect.sizeDelta.y / 2, 0);
				buffer += currentRect.sizeDelta.x;
			}
		}
	}


	bool sumToOne(List<float> occupancy)
	{
		float sum = 0;
		foreach (float p in occupancy)
		{
			sum += p;
		}

		if (sum > 1)
		{
			Debug.LogError("The sum of floats in Occupancy is greater than 1.");
			return false;
		}
		else if (sum < .99f)
		{
			Debug.LogError("The sum of floats in Occupancy is less than 1.");
			return false;
		}

		return true;
	}


	void fitToScreen(bool refParent)
	{
		Rect safeArea = Screen.safeArea;
		RectTransform rectTrans = GetComponent<RectTransform>();
		rectTrans.sizeDelta = safeArea.size;

		if (refParent)
		{
			Vector2 parentSize = transform.parent.GetComponent<RectTransform>().sizeDelta;
			rectTrans.sizeDelta = new Vector2(parentSize.x, parentSize.y);
		}

		transform.position = transform.parent.position;
	}
}
