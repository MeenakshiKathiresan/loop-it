using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : MonoBehaviour
{
	//can be 0 or 1 
	[SerializeField]
	private int state = 0;
	[SerializeField]
	public int State {

		get{

			return state;
		}

		set
		{
			state = value;
			transform.eulerAngles = new Vector3(0, 0, state * 90);
		}
	}

	[SerializeField]
	Collider2D padCollider;


	[SerializeField]
	public bool isFirstPaddle = false;

	[SerializeField]
	Animator animator;

	
    // Start is called before the first frame update
    void Start()
    {
		transform.eulerAngles = new Vector3(0, 0, state * 90);
		Debug.Log("start");
	}

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnMouseDown()
	{
		if (!GameManager.instance.levels[GameManager.instance.currentLevel].ball.move)
		{
			GameManager.instance.sfxPlayer.clip = GameManager.instance.tap;
			GameManager.instance.sfxPlayer.Play();

			state = state == 1 ? 0 : 1;
			transform.eulerAngles = new Vector3(0, 0, state * 90);
		}
	}

	public void PaddleAnimation(direction ballDirection)
	{
		if(state == 0)
		{
			if(ballDirection == direction.right || ballDirection == direction.up)
			{
				animator.Play("bounceUp");
			}
			else
			{
				animator.Play("bounceDown");
			}
		}
		else
		{
			if (ballDirection == direction.left || ballDirection == direction.up)
			{
				animator.Play("bounceUp");
			}
			else
			{
				animator.Play("bounceDown");
			}

		}
		
	}



}
