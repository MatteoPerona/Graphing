using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
	SwipeToggler toggler;
	// list of buttons that toggle in the control panel

	public float animTime;

	// Start is called before the first frame update
	void Start()
	{
		toggler = GetComponent<SwipeToggler>();

		//delegate opening control panel to buttons in list here 
	}

	private void OnMouseDown()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public IEnumerator fadeAlpha(float start, float end, Image im)
	{
		float time = 0;
		while(time <= animTime)
		{
			float newAlpha = Mathf.Lerp(start, end, time / animTime);
			im.color = new Color(im.color.r, im.color.g, im.color.b, newAlpha);

			yield return null;
			time += Time.deltaTime;
		}
	}
}
