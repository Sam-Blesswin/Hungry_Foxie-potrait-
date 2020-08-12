using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class buttoninput : MonoBehaviour
{
	float dirX;
	public float moveSpeed = 5f;
	public float xrange = 3f;
	private Vector3 desireddir;

	private void Start()
	{
		desireddir = transform.position;
	}

	void Update()
	{
		dirX = CrossPlatformInputManager.GetAxis("Horizontal")*moveSpeed*Time.deltaTime;
		//transform.position = new Vector3(transform.position.x+dirX, 0f, 0f);

		desireddir.x += dirX;
		transform.position = Vector3.MoveTowards(transform.position, desireddir, moveSpeed * Time.deltaTime);
		desireddir.x = Mathf.Clamp(desireddir.x, -1 * xrange, xrange);
	}

}
