using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
	SwipeToggler toggler;

	public List<Button> buttons;

	public float animTime;

	// Start is called before the first frame update
	void Start()
	{
		toggler = GetComponent<SwipeToggler>();

		if (buttons == null)
		{
			buttons = new List<Button>();
		}

		foreach (Button b in buttons)
		{
			b.onClick.AddListener(delegate
			{
				Debug.Log("button clicked");
				StartCoroutine(toggler.toggleIn());
			});
		}
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
