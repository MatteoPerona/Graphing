using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetWorthGraph : MonoBehaviour
{
	Brain brain;
	Grapher graph;

	// Start is called before the first frame update
	void Start()
	{
		brain = FindObjectOfType<Brain>().GetComponent<Brain>();
		graph = GetComponent<Grapher>();

		graph.pointCount = brain.netWorths.Count;
		graph.yMax = maxFromList(brain.netWorths);
	}

	// Update is called once per frame
	void Update()
	{
		
	}


	public void updateGraph()
	{
		List<Vector2> data = new List<Vector2>();
		int p = 0;
		foreach (float f in brain.netWorths)
		{
			data.Add(new Vector2(p, f));
			Debug.Log(new Vector2(p, f));
			p++;
		}
		graph.pointCount = brain.netWorths.Count;
		graph.yMax = maxFromList(brain.netWorths);
		graph.generateGraph(data);
		// need to adjust locations where points are instantiated
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
