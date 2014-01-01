using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour
{
	public float scrollSpeed;
	public float scrollSpeed2;
	
	void FixedUpdate() {
		float offset = Time.time * scrollSpeed;
		float offset2 = Time.time * scrollSpeed2;
		renderer.material.mainTextureOffset = new Vector2(offset2, -offset);
	}
}