using UnityEngine;
using System.Collections;

public class scoreGUI : MonoBehaviour {
    ShipMovementScript shipscript;
    GUIText guiText;
	// Use this for initialization
	void Start () {
        shipscript = GameObject.FindObjectOfType<ShipMovementScript>();
        guiText = GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 pos = Camera.main.WorldToScreenPoint(transform.parent.position);
        transform.position = pos.normalized;

	}
    void OnGUI()
    {
        int score = 0;
        if (shipscript != null) score = shipscript.score;
        guiText.text = score.ToString();
    }
}
