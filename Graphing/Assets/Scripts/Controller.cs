using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

	public TMP_Text inputText;
	TMP_Text currentInputText;

	Color red = new Color32(235, 64, 52, 255);
	Color green = new Color32(52, 235, 73, 255);
	Color blue = new Color32(52, 128, 235, 255);
	Color orange = new Color32(235, 131, 52, 255);

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
			StartCoroutine(fadeAlpha(1, .2f, GetComponent<Image>()));

			currentInputText = Instantiate(inputText.gameObject, transform.position, Quaternion.identity, transform).GetComponent<TMP_Text>();

			currentTickerSpeed = ticker.tickerSpeed;
			isOrder = false;

			startPos = Input.mousePosition;

			if (startPos.x < size.x*.75f)
			{
				isOrder = true;
			}
			ticker.tickerSpeed = 3f;
		}

		else if (Input.GetMouseButton(0))
		{
			deltaY = (Input.mousePosition.y - startPos.y) / size.y;

			currentInputText.transform.position = (Input.mousePosition + new Vector3(0, 200, 0));

			if (isOrder)
			{
				currentInputText.text = ticker.orderVolumeString(deltaY);
				currentInputText.color = Color.Lerp(red, green, deltaY + 0.5f);
			}
			else
			{
				currentInputText.text = ticker.deltaSpeedString(deltaY, currentTickerSpeed);
				currentInputText.color = Color.Lerp(blue, orange, deltaY + 0.5f);
			}
		}

		else if (Input.GetMouseButtonUp(0))
		{
			StartCoroutine(fadeAlpha(0, .2f, GetComponent<Image>()));

			currentInputText.gameObject.GetComponent<InputText>().DestroyMe();

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


	IEnumerator fadeAlpha(float endAlpha, float duration, Image im)
	{
		Color temp = im.color;
		float startAlpha = im.color.a;

		float time = 0.0f;
		while (time < duration)
		{
			temp.a = Mathf.Lerp(startAlpha, endAlpha, time / duration);
			im.color = temp;

			yield return null;
			time += Time.deltaTime;
		}
		temp.a = endAlpha;
		im.color = temp;
	}
}
