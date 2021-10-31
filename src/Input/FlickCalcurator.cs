using UnityEngine;
using System.Collections;

public class FlickCalcurator : MonoBehaviour
{
	public static Vector3 firstPosition { private set; get; }
	public static Vector3 move { private set; get; } = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			firstPosition=Input.mousePosition;
			return;
		}
		if (Input.GetMouseButton (0)) {
			move=Input.mousePosition-firstPosition;
		}
		if (Input.GetMouseButtonUp (0)) {
			move=Vector3.zero;
			firstPosition=Vector3.zero;
		}
	}
}
