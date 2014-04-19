using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerControlsView : MonoBehaviour {

	LineRenderer lineRenderer;
	// Use this for initialization
	void Awake () {
		lineRenderer = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ClearLineRenderer()
	{
		lineRenderer.SetVertexCount(0);
	}

	void DrawLine(Vector2 a, Vector2 b)
	{
		lineRenderer.SetPosition(0, a);
		lineRenderer.SetPosition(1, b);
	}

	public void DrawPath(Queue<Vector2> nodes)
	{
		lineRenderer.SetColors(Color.blue, Color.blue);
		Queue<Vector2> copy = new Queue<Vector2>(nodes);
		if(nodes.Count > 0)
		{
			lineRenderer.SetVertexCount(nodes.Count+1);
			lineRenderer.SetPosition(0, transform.position);
			int i = 1;
			while(copy.Count > 0)
			{
				lineRenderer.SetPosition(i, copy.Dequeue());
				i++;
			}
		}

	}

	public void SetOrigin(Vector2 origin)
	{
		lineRenderer.SetPosition(0, origin);
	}

	public void DrawToMouse(Vector2 root)
	{
		lineRenderer.SetVertexCount(2);
		lineRenderer.SetPosition(0, root);
		Vector3 mousePos3d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mousePos = new Vector2(mousePos3d.x, mousePos3d.y);
		lineRenderer.SetPosition(1,mousePos);
	}

	public void DrawShot(Vector2 puckPos)
	{
		lineRenderer.SetColors(Color.red, Color.red);
		DrawToMouse(puckPos);
	}
}
