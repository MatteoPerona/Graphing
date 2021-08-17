using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RectMapper : MonoBehaviour
{
	public RectTransform inputRect;
	RectTransform outRect;

	Vector3 ogPos;

	[Range(0, 1)]
	public float xPadding = 1;
	[Range(0, 1)]
	public float yPadding = 1;

	Vector2 ogPadding;

	// Start is called before the first frame update
	void Start()
	{
		if (outRect == null)
		{
			outRect = GetComponent<RectTransform>();
		}
		ogPos = inputRect.position;
		mapRects();

		ogPadding = new Vector2(xPadding, yPadding);
	}

	// Update is called once per frame
	void Update()
	{
		if (inputRect.position != ogPos)
		{
			mapRects();
			ogPos = inputRect.position;
		}

		if (ogPadding.x != xPadding || ogPadding.y != yPadding)
		{
			ogPadding = new Vector2(xPadding, yPadding);
			mapRects();
		}
	}


	void mapRects()
	{
		outRect.sizeDelta = new Vector2(inputRect.sizeDelta.x * xPadding, inputRect.sizeDelta.y * yPadding);
		outRect.position = inputRect.position;
		outRect.rotation = inputRect.rotation;
	}
}
