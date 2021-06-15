using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fitter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rectTrans = GetComponent<RectTransform>();
        rectTrans.sizeDelta = transform.parent.GetComponent<RectTransform>().sizeDelta;
        transform.position = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
