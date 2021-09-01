using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Fitter : MonoBehaviour
{
    public bool useParentAsReference = false;
    public bool childrenControlHeight = false;
    public bool childrenControlWidth = false;
    public bool fitBoxCollider = false;
    public bool fitBoxCollider2D = false;

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

        if (fitBoxCollider)
		{
            BoxCollider collider = GetComponent<BoxCollider>();
            collider.size = new Vector3(rectTrans.sizeDelta.x, rectTrans.sizeDelta.y, 0);
            collider.center = new Vector3(-1 * rectTrans.sizeDelta.x / 2, -1 * rectTrans.sizeDelta.y / 2, 0);
		}

        if (fitBoxCollider2D)
        {
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.size = rectTrans.sizeDelta;
        }
    }

	private void OnValidate()
	{
	}

	// Update is called once per frame
	void Update() 
    {
        
    }
}
