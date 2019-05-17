using UnityEngine;
using System.Collections;

public class ScrrollConveyor : MonoBehaviour {
    float time = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime * -0.1f;
        time %= 1f;
        GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(0f, time);
    }
}
