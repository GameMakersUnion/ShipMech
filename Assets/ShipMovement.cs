using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {

	private float forwardSpeed = 0f;
	private float strafeSpeed = 5f;
	private Plane plane;
	private Transform plane2;

	// Use this for initialization
	void Start () {
		plane = new Plane( Vector3.forward, 0 );
		Debug.Log ( "" );
		plane2 = transform.FindChild("PlaneForRaycasting");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3( transform.position.x, transform.position.y, transform.position.z + forwardSpeed * Time.deltaTime );
		//get mouse position on screen
		//... get translated to world coords
		//...find diff btwn ship and mouse position in world
		// move that way
		old ();
		//planes();
	}
	void old()
	{
		Vector2 mousePos = Input.mousePosition;
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3 (mousePos.x,mousePos.y));
		Vector3 currentPos = new Vector3(transform.position.x, transform.position.y, 0);
		transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z);
	}
	void planes()
	{
		Vector2 mousePos = Input.mousePosition;
		float dist;
		Ray ray = Camera.main.ScreenPointToRay(mousePos);
		plane.distance = transform.position.z + 1f;
		if ( plane.Raycast(ray, out dist)){
			Vector3 point = ray.GetPoint(dist);
			//Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3 (mousePos.x,mousePos.y,0));
			Vector3 diff = point - transform.position; 
			diff.z = 0;
			diff.Normalize();
			diff *= strafeSpeed * Time.deltaTime;
			transform.position += diff;
		}
	}
}
