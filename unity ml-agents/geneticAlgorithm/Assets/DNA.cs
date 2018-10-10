using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour {
    //gene for colour and size
    public float r;
    public float g;
    public float b;
    public float s;
    // if we shoot the person(sprite) he dies
    bool dead = false;
    // counter for how long the person lives
    public float timeToDie =0;
    // helper functions to turn sprite off when clicked.
    SpriteRenderer sRenderer;
    Collider2D sCollider;
    //when we click at person prefabs they should disappear
    private void OnMouseDown()  {
        dead = true;
        timeToDie = PopulationManager.elapsed;
        Debug.Log("Dead at: " + timeToDie);
        sRenderer.enabled = false;
        sCollider.enabled = false;
    }
    // Use this for initialization for rendering of person prefab
    void Start () {
        sRenderer = GetComponent<SpriteRenderer>();
        sCollider = GetComponent<Collider2D>();
        sRenderer.color = new Color(r, g, b);
        //size factor 
        transform.localScale = new Vector3(s, s, s);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
