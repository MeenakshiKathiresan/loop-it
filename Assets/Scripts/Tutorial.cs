using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Tutorial : MonoBehaviour
{
	[SerializeField]
	SpriteRenderer glowBox;

	[SerializeField]
	Text instruction;

	[SerializeField]
	Pad paddleToTap;

	[SerializeField]
	int paddleCorrectState;

	[SerializeField]
	SpriteRenderer arrowSprite;


	private void OnEnable()
	{
		StartCoroutine(OnTutorialStart());

	}

	IEnumerator OnTutorialStart()
	{
		instruction.text = "Tap on the required paddles to move the ball in an endless loop.";
		instruction.DOFade(1, 0.3f);
		yield return new WaitForSeconds(5);
		instruction.DOFade(0, 0.2f);
		yield return new WaitForSeconds(1);

		instruction.text = "Tap on the highlighted box.";
		glowBox.DOFade(1, 0.5f).SetLoops(-1, LoopType.Yoyo);
		instruction.DOFade(1, 0.3f);
		yield return new WaitUntil(() => paddleToTap.State == paddleCorrectState);
		glowBox.DOKill();
		instruction.DOFade(0, 0.2f);
		yield return new WaitForSeconds(1);
				
		instruction.text = "Tap on GO!";
		arrowSprite.DOFade(1, 0.25f);
		instruction.DOFade(1, 0.3f);
		yield return new WaitUntil(() => GameManager.instance.levels[0].ball.move == true);
		instruction.DOFade(0, 0.2f);
		arrowSprite.DOFade(0, 0.2f);


	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
