using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RectStretcher : MonoBehaviour
{
	public Vector2 stretchPercent;
	public bool vertivalShiftNegative; //If true, the script will compensate vertical delta in negative direction
	public bool horizontalShiftNegative; //If true, the script will compensate horizontal delta in negative direction

	RectTransform rect;

	Vector2 pos;
	Vector2 size;

	Vector2 delta; //Stores the measured change

	// Start is called before the first frame update
	void Start()
	{
		rect = GetComponent<RectTransform>();

		stretchRect();
	}

	// Update is called once per frame
	void OnValidate()
	{
	}


	public void stretchRect()
	{
		pos = rect.position;
		size = rect.sizeDelta;

		delta = (stretchPercent - new Vector2(1, 1)) * size;

		rect.sizeDelta = size * stretchPercent;
		if (vertivalShiftNegative && horizontalShiftNegative)
		{
			rect.position -= new Vector3(pos.x - delta.x, pos.y - delta.y, 0);
		}
		else if (!vertivalShiftNegative && horizontalShiftNegative)
		{
			rect.position -= new Vector3(pos.x - delta.x, pos.y, 0);
		}
		else if (vertivalShiftNegative && !horizontalShiftNegative)
		{
			rect.position -= new Vector3(pos.x, pos.y - delta.y, 0);
		}
		else
		{
			rect.position += new Vector3(pos.x, pos.y, 0);
		}
	}
}
