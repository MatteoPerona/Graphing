using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputText : MonoBehaviour
{
    TMP_Text text;
    

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyMe()
	{
        StartCoroutine(destroyRoutine(.5f));
	}

    IEnumerator destroyRoutine(float duration)
	{
        float time = 0;
        while (time < duration)
		{
            text.alpha = Mathf.Lerp(1, 0, time/duration);

            yield return null;
            time += Time.deltaTime;
		}
        Destroy(gameObject);
	}
}
