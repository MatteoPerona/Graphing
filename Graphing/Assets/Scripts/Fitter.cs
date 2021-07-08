using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fitter : MonoBehaviour
{
    public bool useParentAsReference = false;
    public bool childrenControlHeight = false;
    public bool childrenControlWidth = false;

    // Start is called before the first frame update
    void Awake()
    {
        Rect safeArea = Screen.safeArea;
        RectTransform rectTrans = GetComponent<RectTransform>();
        rectTrans.sizeDelta = safeArea.size;

        if (useParentAsReference)
        {
            Vector2 parentSize = transform.parent.GetComponent<RectTransform>().sizeDelta;
            rectTrans.sizeDelta = new Vector2(parentSize.x, parentSize.y);
        }

        transform.position = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
