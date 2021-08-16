using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NetWorthGraph : MonoBehaviour
{
	Brain brain;
	Grapher graph;

	public TMP_Text netWorthText;


	// Start is called before the first frame update
	void Start()
	{
		brain = FindObjectOfType<Brain>().GetComponent<Brain>();
		graph = GetComponent<Grapher>();

		graph.pointCount = brain.netWorths.Count;
		graph.yMax = maxFromList(brain.netWorths);
		netWorthText.text = "$1.00";
	}

	// Update is called once per frame
	void Update()
	{
		
	}


	public void updateGraph()
	{
		netWorthText.text = brain.netWorths[brain.netWorths.Count-1].ToString("C");

		graph.purgePoints();
		List<Vector2> data = new List<Vector2>();
		int p = 0;
		foreach (float f in brain.netWorths)
		{
			data.Add(new Vector2(p, f));
			Debug.Log("Input Vector: "+new Vector2(p, f));
			p++;
		}
		graph.pointCount = brain.netWorths.Count;
		graph.xMax = brain.netWorths.Count;
		graph.yMax = maxFromList(brain.netWorths);
		graph.generateGraph(data);
	}

	float maxFromList(List<float> data)
	{
		float max = 0;
		foreach (float f in data)
		{
			if (f > max)
			{
				max = f;
			}
		}
		return max;
	}
}
