using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{	

	Pad[] paddles;

	public Pad shouldBeWrongPad;

	public int wrongPaddleState;

	[HideInInspector]
	public int loopCount = -1;

	public Ball ball;

	public int levelScore;
	public float bonus;
	[HideInInspector]
	public float currentBonus;
	public float alreadyUsedTime;

	public float totalTime;

	void Start()
    {
        
    }

	private void Awake()
	{		
		paddles = GetComponentsInChildren<Pad>();
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	public void Reset()
	{
	
		for(int i =0; i < paddles.Length; i++)
		{
			paddles[i].State = Random.Range(0, 2);
		}

		shouldBeWrongPad.State = wrongPaddleState;
		loopCount = -1;
		ball.transform.position = ball.startPosition;
		ball.move = false;
		ball.SetDirection(ball.startDirection);
		//currentBonus = bonus;	

	}

	public void LevelStart()
	{
		currentBonus = bonus;
		GameManager.instance.SetBonus();
		if(GetComponent<Tutorial>() == null)
		Reset();
	}
}
