using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawRinkTest : MonoBehaviour {


	EdgeCollider2D edge;
	// Use this for initialization
	void Start () 
	{
	
		edge = GetComponent<EdgeCollider2D>();

		List<Vector2> vert = CreateVertices();
		edge.points = vert.ToArray();

	}

	public float width = 200;
	public float height = 85;
	
	// Update is called once per frame
	void Update () 
	{

	}



	List<Vector2> CreateVertices()
	{

		int curveSegments = 20;
		float curveRadius = 0.065f*width;

		List<Vector2> verts = new List<Vector2>();
		//start top middle


		Vector2 pos = transform.position + new Vector3(0,height/2, 0);

		verts.Add(pos);

		//top left corner
		Vector2 center = new Vector2(-width/2 + curveRadius, -curveRadius);
		foreach(Vector2 v in Arc (center, curveRadius, Mathf.PI/2, Mathf.PI/2,  curveSegments))
		{
			verts.Add(v+pos);
		}

		//bottom left corner
		center =  new Vector2(-width/2 + curveRadius, -height + curveRadius);
		foreach(Vector2 v in Arc (center, curveRadius, Mathf.PI, Mathf.PI/2,  curveSegments))
		{
			verts.Add(v+pos);
		}

		//bottom right corner
		center = new Vector2(width/2 - curveRadius, -height + curveRadius);
		foreach(Vector2 v in Arc (center, curveRadius, Mathf.PI*1.5f, Mathf.PI/2,  curveSegments))
		{
			verts.Add(v+pos);
		}

		//top right coerner
		center = new Vector2(width/2 - curveRadius, -curveRadius);
		foreach(Vector2 v in Arc (center, curveRadius, Mathf.PI*2f, Mathf.PI/2,  curveSegments))
		{
			verts.Add(v+pos);
		}

		verts.Add(pos);
		return verts;
	}
	

	List<Vector2> Arc(Vector2 center, float radius, float startAngle, float arcAngle, int segments)
	{
		List<Vector2> verts = new List<Vector2>();

		float theta = arcAngle / (float)(segments-1);
		float tanFactor = Mathf.Tan(theta);
		float radFactor = Mathf.Cos(theta);

		float x = radius * Mathf.Cos(startAngle);
		float y = radius * Mathf.Sin(startAngle);

		for(int i = 0; i < segments; i++)
		{
			verts.Add(new Vector2(x + center.x,y + center.y));
			float tx = -y;
			float ty = x;

			x+= tx*tanFactor;
			y+= ty*tanFactor;

			x *=radFactor;
			y *=radFactor;
		}
		return verts;
	}
}
