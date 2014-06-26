using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {

	enum LaserState{
		prepped,
		firing,
		cooldown,
	}

	private float forwardSpeed = 10f;
	private float strafeSpeed = 5f;
	private Plane plane, plane2;

	private float laserFiringDuration = 0.2f;
	private float laserCooldownDuration = 0.2f;
	private float laserLength = 3f;
	private float laserTimeAccum = 0f;
	private LaserState laserState = LaserState.prepped;

	private Vector3 laserTarget;
	public GameObject laser;
	private LineRenderer laserRenderer;
	public int score;
	public int health;

	float effectivePosition;
	// Use this for initialization
	void Start () {
		effectivePosition = transform.position.z;
		plane = new Plane( Vector3.back, 0 );
		plane2 = new Plane( Vector3.back, 0 );
		laserRenderer = laser.GetComponent<LineRenderer>();

	}


	// Update is called once per frame
	void Update () {
		effectivePosition += forwardSpeed * Time.deltaTime;

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
			transform.LookAt(point);
			if (laserState == LaserState.prepped && Input.GetMouseButtonDown(0)){
				laserState = LaserState.firing;
				laserTimeAccum = 0f;
				laserTarget = point;
				RaycastHit hit;
				Ray collisionRay = Camera.main.ScreenPointToRay (Input.mousePosition);
				Debug.Log ("Try to hit");
				if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.NameToLayer("player"))){
					Debug.Log ("We hit");
					 if (hit.collider.transform.tag == "enemy"){
						Destroy(hit.collider.gameObject);
						score+=100;
					}
					if (hit.collider.transform.tag == "civilian"){
						Destroy(hit.collider.gameObject);
						score-=1000;
						damage();
					}
					else{
						Debug.Log("Hit a Wall");
					}

				}

			}

		}

		if (laserState != LaserState.prepped)
		{
			laserTimeAccum += Time.deltaTime;
			Vector3 diff = (laserTarget - transform.position).normalized;
			Vector3 final = diff * laserLength;
			laserRenderer.SetPosition(0, transform.position + diff * laserTimeAccum * 10);
			laserRenderer.SetPosition(1, transform.position + final + diff * laserTimeAccum * 10);
			if (laserState == LaserState.firing)
			{
				if (laserTimeAccum > laserFiringDuration)
				{
					laserState = LaserState.cooldown;
				}
			}
			else if (laserState == LaserState.cooldown)
			{
				if (laserTimeAccum > laserCooldownDuration + laserFiringDuration)
				{
					laserState = LaserState.prepped;
				}
			}
		}

		if(invulnerable){
			invTimer--;
			if(invTimer <0){
				invulnerable = false;
				rigidbody.velocity = new Vector3(0,0,0);
			}
		} else{
			transform.position = new Vector3( transform.position.x, transform.position.y, effectivePosition);
		}
	}

	bool invulnerable = false;
	int invTimer = 0;
	public int InvulnerabilityTime;

	void OnCollisionEnter(Collision c){
		damage();
	}

	void damage(){
		if (!invulnerable){
			health-=1;
			invulnerable = true;
			invTimer = InvulnerabilityTime;
		}
	}
}
