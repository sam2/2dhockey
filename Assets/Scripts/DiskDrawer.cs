using UnityEngine;

using System.Collections;



[RequireComponent (typeof(Collider))]



public class DiskDrawer: MonoBehaviour {
	
	
	public LineRenderer lineRenderer;
	
	void  Start (){

		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetVertexCount(2);




	}


	
	void  Update (){

	
		
	}
	
	

	
}