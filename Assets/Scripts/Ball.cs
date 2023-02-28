using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Ball : MonoBehaviour
{
	direction currentDirection;

	[SerializeField]
	public direction startDirection;

	[SerializeField]
	float speed = 5;

	public Vector2 startPosition;

	Vector2 currentDirectionVector;

	public bool move = false;

	
	// Start is called before the first frame update
	void OnEnable()
	{
		startPosition = transform.position;
		SetDirection(startDirection);
	}

	// Update is called once per frame
	void Update()
	{
		if (move)
			transform.Translate(currentDirectionVector * speed * Time.deltaTime);
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.transform.parent != null && collision.transform.parent.GetComponent<Pad>() != null)
		{
			GameManager.instance.sfxPlayer.clip = GameManager.instance.hit;
			GameManager.instance.sfxPlayer.Play();

			Pad pad = collision.transform.parent.GetComponent<Pad>();
			direction prevDirection = currentDirection;

			if (pad.isFirstPaddle)
			{
				int loopCount = ++GameManager.instance.levels[GameManager.instance.currentLevel].loopCount;
				if (currentDirection == startDirection && loopCount == 1)
				{
					StartCoroutine(GameManager.instance.GameSuccess());
				}
			}

			if (pad.State == 0)
			{
				switch (currentDirection)
				{
					case direction.right: SetDirection(direction.down); break;
					case direction.left: SetDirection(direction.up); break;
					case direction.up: SetDirection(direction.left); break;
					case direction.down: SetDirection(direction.right); break;
				}
			}
			else
			{
				switch (currentDirection)
				{
					case direction.right: SetDirection(direction.up); break;
					case direction.left: SetDirection(direction.down); break;
					case direction.up: SetDirection(direction.right); break;
					case direction.down: SetDirection(direction.left); break;
				}
			}
			pad.PaddleAnimation(prevDirection);
			//ball squish and squash			
			//ballSprite.transform.DOScaleY(0.55f, 0.05f).SetLoops(2, LoopType.Yoyo);
			StartCoroutine(GameManager.instance.TurnOnLight(GameManager.instance.yellowLight,0.2f));
		}
		if (collision.transform.GetComponent<Boundary>() != null)
		{
			StartCoroutine(GameManager.instance.GameFailure());
		}


	}

	public void SetDirection(direction current)
	{
		currentDirection = current;
		switch (currentDirection)
		{
			case direction.right: currentDirectionVector = Vector2.right; break;
			case direction.left: currentDirectionVector = Vector2.left; break;
			case direction.up: currentDirectionVector = Vector2.up; break;
			case direction.down: currentDirectionVector = Vector2.down; break;
		}
	}
	public Vector2 GetDirection(direction direction)
	{
		switch (direction)
		{
			case direction.right: return Vector2.right; 
			case direction.left: return Vector2.left;
			case direction.up: return Vector2.up; 
			case direction.down:return Vector2.down; 
		}
		return Vector2.right;
	}

}

public enum direction { left, right, up, down }
