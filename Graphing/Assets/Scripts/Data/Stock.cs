using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stock
{
	public float lastPrice;
	string name;
	Color32 color;

	string letters = "abcdefghijklmnopqrstuv";


	public Stock()
	{
		nameGen();
		//color picker
	}


	public void nameGen()
	{
		int numChars = Random.Range(3, 4);
		for (int i = 0; i < numChars; i++)
		{
			name += letters.ToCharArray()[Random.Range(0, 26)];
		}
	}
}
