using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {

	private float forwardSpeed = 10f;
	private float strafeSpeed = 5f;
	private Plane plane, plane2;

	// Use this for initialization
	void Start () {
		plane = new Plane( Vector3.back, 0 );
		plane2 = new Plane( Vector3.back, 0 );
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3( transform.position.x, transform.position.y, transform.position.z + forwardSpeed * Time.deltaTime );
		Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + forwardSpeed * Time.deltaTime );
		//get mouse position on screen
		//... get translated to world coords
		//...find diff btwn ship and mouse position in world
		// move that way
		planes();
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
			if(diff.magnitude > strafeSpeed * Time.deltaTime){
				diff.Normalize();
				diff *= strafeSpeed * Time.deltaTime;
				 transform.position += diff;
			}
		}
		plane2.distance = transform.position.z + 10f;
		if (plane2.Raycast (ray, out dist))
		{
			Vector3 point = ray.GetPoint(dist);
			Debug.Log(point);
			transform.LookAt(point);
		}

	}
}
