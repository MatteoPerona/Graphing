using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlotRenderer : Graphic
{
    public Vector2Int gridSize;
    public float thickness;
    public GraphRenderer graph;

    public List<Vector2> points;

    float width;
    float height;
    float unitWidth;
    float unitHeight;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        width = rectTransform.rect.width;
        height = rectTransform.rect.height;

        unitWidth = width / gridSize.x;
        unitHeight = height / gridSize.y;

        if (points.Count < 2) return;

        float angle = 0;
        for (int i = 0; i < points.Count - 1; i++)
        {

            Vector2 point = points[i];
            Vector2 point2 = points[i + 1];

            if (i < points.Count - 1)
            {
                angle = GetAngle(points[i], points[i + 1]) + 90f;
            }

            DrawVerticesForPoint(point, point2, angle, vh);
        }

        for (int i = 0; i < points.Count - 1; i++)
        {
            int index = i * 4;
            vh.AddTriangle(index + 0, index + 1, index + 2);
            vh.AddTriangle(index + 1, index + 2, index + 3);
        }

		for (int i = 1; i < points.Count - 1; i++)
		{
            int[] verts = new int[3];

            float angle0 = GetAngle(points[i - 1], points[i]);
            float angle1 = GetAngle(points[i], points[i + 1]);
            float dAngle = angle1 - angle0;

            List<UIVertex> uiVerts = new List<UIVertex>();
            vh.GetUIVertexStream(uiVerts);

            int offset = i * 4;

            if (dAngle > 0)
			{
                verts = new int[] { offset + 2, offset + 4, 0 };
                DrawVerticesForCorners(uiVerts[verts[0]], uiVerts[verts[1]], angle0, angle1, vh);
                verts[2] = vh.currentVertCount + i - 1;
            }
            else if (dAngle < 0)
			{
                verts = new int[] { offset + 3, offset + 5, 0 };
                DrawVerticesForCorners(uiVerts[verts[0]], uiVerts[verts[1]], angle0, angle1, vh);
                verts[2] = vh.currentVertCount + i - 1;
            }

            Debug.Log(verts[0] + ", " + verts[1] + ", " + verts[2]);
            vh.AddTriangle(verts[0], verts[1], verts[2]);
        }
    }

    void DrawVerticesForCorners(UIVertex v0, UIVertex v1, float angle0, float angle1, VertexHelper vh)
	{
        float m0 = Mathf.Tan(angle0);
        float m1 = Mathf.Tan(angle1);

        float x = (v1.position.y - v0.position.y + m0 * v0.position.x - m1 * v1.position.x) / (m0 + m1);
        float y = m0 * (x + v0.position.x) + v0.position.y;

        UIVertex vertex = UIVertex.simpleVert;
        vertex.position = new Vector3(x, y);
        vh.AddVert(vertex);
	}

    public float GetAngle(Vector2 me, Vector2 target)
    {
        return (float)(Mathf.Atan2(gridSize.y * (target.y - me.y), gridSize.x * (target.x - me.x)) * (180 / Mathf.PI));
    }

    void DrawVerticesForPoint(Vector2 point, Vector2 point2, float angle, VertexHelper vh)
    {
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(-thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(-thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point2.x, unitHeight * point2.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point2.x, unitHeight * point2.y);
        vh.AddVert(vertex);
    }

    private void Update()
	{
		if (graph != null)
		{
			if (gridSize != graph.gridSize)
			{
				gridSize = graph.gridSize;
				SetVerticesDirty();
			}
		}
	}
}
