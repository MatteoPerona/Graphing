using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphRenderer : Graphic
{
	public Vector2Int gridSize = new Vector2Int(1, 1);
	public float thickness = 10f;

	float height;
	float width;
	float cellWidth;
	float cellHeight;

	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();

		height = rectTransform.rect.height;
		width = rectTransform.rect.width;

		cellWidth = width / (float)gridSize.x;
		cellHeight = height / (float)gridSize.y;

		int count = 0;

		for (int y=0; y<gridSize.y; y++)
		{
			for (int x=0; x<gridSize.x; x++)
			{
				DrawCell(x, y, count, vh);
				count++;
				
			}
		}
	}

	public void DrawCell(int x, int y, int index, VertexHelper vh)
	{
		float xPos = cellWidth * x;
		float yPos = cellHeight * y;


		UIVertex vertex = UIVertex.simpleVert;
		vertex.color = color;

		vertex.position = new Vector3(xPos, yPos);
		vh.AddVert(vertex);

		vertex.position = new Vector3(xPos, yPos + cellHeight);
		vh.AddVert(vertex);

		vertex.position = new Vector3(xPos + cellWidth, yPos + cellHeight);
		vh.AddVert(vertex);

		vertex.position = new Vector3(xPos + cellWidth, yPos);
		vh.AddVert(vertex);

		float widthSqr = thickness * thickness;
		float distanceSqr = widthSqr / 2f;
		float distance = Mathf.Sqrt(distanceSqr);

		vertex.position = new Vector3(xPos + distance, yPos + distance);
		vh.AddVert(vertex);

		vertex.position = new Vector3(xPos + distance, yPos + (cellHeight - distance));
		vh.AddVert(vertex);

		vertex.position = new Vector3(xPos + (cellWidth - distance), yPos + (cellHeight - distance));
		vh.AddVert(vertex);

		vertex.position = new Vector3(xPos + (cellWidth - distance), yPos + distance);
		vh.AddVert(vertex);

		int offest = index * 8;

		//outer
		for (int i = 0; i < 4; i++)
		{
			int j = i + 1;
			int k = i + 5;
			if (k > 7)
			{
				k = k - 8;
			}
			vh.AddTriangle(offest + i, offest + j, offest + k);
		}

		//inner
		for (int i = 0; i < 4; i++)
		{
			int j = i + 4;
			int k = i + 5;
			if (k > 7)
			{
				k = k - 4;
			}
			vh.AddTriangle(offest + i, offest + j, offest + k);
		}
	}
}
