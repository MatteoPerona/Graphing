using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public GameObject point;

    public int xMax;
    public int yMax;

    private Vector2 min;
    private Vector2 max;


    void Start()
    {
        transform.localPosition = new Vector2(0, 0);
        gameObject.GetComponent<RectTransform>().sizeDelta = transform.parent.GetComponent<RectTransform>().sizeDelta;

        min = transform.localPosition;
        Instantiate(point, min, Quaternion.identity, transform);

        max = gameObject.GetComponent<RectTransform>().sizeDelta;
        Instantiate(point, max, Quaternion.identity, transform);

        StartCoroutine(plot(1));
    }

    void Update()
    {
        
    }

    public IEnumerator plot(float duration)
	{
        float time = 0;
        while(time < duration)
		{
            Vector2 current = Vector2.Lerp(min, max, time / duration);
            if ((time/duration*10)%1 == 0)
                Instantiate(point, current, Quaternion.identity, transform);

            yield return null;
            time += Time.deltaTime;
		}
	}
}
