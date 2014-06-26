using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {
	
	public float ActivationDistance = 10f;
	public GameObject spaceship;
	public Vector2 destination = new Vector2(0, 0);
	public float SpinSpeed = 10f;
	public float transpeed = 1f;
	
	private bool isActivated = false;
	private Vector2 directionVector;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!isActivated){
			float distance = transform.position.z-spaceship.transform.position.z;
			if (distance<ActivationDistance)
			{isActivated=true;
			directionVector = new Vector2(destination.x - transform.position.x, destination.y - transform.position.y);
			directionVector.Normalize();
			directionVector*=transpeed;
			}
		}
		else{
			transform.Rotate(0, 0, Time.deltaTime*SpinSpeed);
			transform.position = new Vector3(transform.position.x+directionVector.x, transform.position.y+directionVector.y, transform.position.z);
		}
	}
}
