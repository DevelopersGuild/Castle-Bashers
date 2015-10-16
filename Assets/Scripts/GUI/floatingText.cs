using UnityEngine;
using System.Collections;

public class floatingText : MonoBehaviour {
    TextMesh textMesh;
    public float speed = 0.2f;
    public float duration = 2;
    private float initTime = 0;
	// Use this for initialization
	void Start () {
        textMesh = GetComponent<TextMesh>();
        initTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        textMesh.transform.position += new Vector3(0, speed, 0);
        textMesh.color += new Color(0f, 0f, 0f, 0.05f);
        if(Time.time - initTime >= duration)
        {
            Destroy(gameObject);
        }
	}
}
