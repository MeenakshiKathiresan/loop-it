using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	[SerializeField]
	public Level[] levels;

	[SerializeField]
	Button startButton;

	private int score;

	float startTime;
	

	public int currentLevel;

	[SerializeField]
	Image bonusFill;

	[SerializeField]
	Text scoreText;

	[SerializeField]
	Text bonusScore;

	[SerializeField]
	Image menuScreen;

	[SerializeField]
	Button playButton;

	[SerializeField]
	Button closeButton;

	[SerializeField]
	GameObject redLight;

	[SerializeField]
	public GameObject yellowLight;

	[SerializeField]
	Text feedback;

	[SerializeField]
	public AudioSource bgmPlayer, sfxPlayer;

	[SerializeField]
	public AudioClip bgm, success, failure, hit, tap, click;



	// Start is called before the first frame update
	void Awake()
	{
		Debug.Log("GM on awake");
		instance = this;
	}


	private void OnDisable()
	{
		startButton.onClick.RemoveAllListeners();
		playButton.onClick.RemoveAllListeners();
		closeButton.onClick.RemoveAllListeners();
	}


	private void Start()
	{
		startTime = Time.time;
		startButton.onClick.AddListener(() => { levels[currentLevel].ball.move = true;
			sfxPlayer.clip = click;
			sfxPlayer.Play();
			levels[currentLevel].currentBonus = (Time.time - startTime - levels[currentLevel].alreadyUsedTime) * levels[currentLevel].bonus / levels[currentLevel].totalTime;
			levels[currentLevel].alreadyUsedTime = Time.time - startTime;
		});
		
		playButton.onClick.AddListener(() => { EnableCurrentLevel(); menuScreen.gameObject.SetActive(false); });
		closeButton.onClick.AddListener(() => { Application.Quit(); });
		yellowLight.SetActive(false);
		redLight.SetActive(false);
		bgmPlayer.Play();
	}

	
	// Update is called once per frame
	void Update()
	{
		if (Time.time - startTime < levels[currentLevel].totalTime && !levels[currentLevel].ball.move)
		{
			bonusFill.fillAmount =   (levels[currentLevel].totalTime - (Time.time - startTime - levels[currentLevel].alreadyUsedTime)) / levels[currentLevel].totalTime;
		}
	}

	public void SetBonus()
	{
		
	
	}

	public IEnumerator GameSuccess()
	{
		sfxPlayer.clip = success;
		sfxPlayer.Play();

		score += levels[currentLevel].levelScore + (int)levels[currentLevel].currentBonus;
		scoreText.text = score.ToString();
		feedback.text = "AH YES!";
		yield return new WaitForSeconds(5);
		startTime = Time.time;
		if (currentLevel < levels.Length - 1)
		{
			currentLevel++;
			EnableCurrentLevel();
		}
		else
		{
			currentLevel = 0;
			BackToMainMenu();
		}
	}

	public IEnumerator GameFailure()
	{
		sfxPlayer.clip = failure;
		sfxPlayer.Play();

		StartCoroutine(TurnOnLight(redLight,2));
		feedback.text = "OH NO!";
		yield return new WaitForSeconds(3);
		levels[currentLevel].Reset();
		startTime = Time.time;
		feedback.text = "LEVEL " + (currentLevel+1);
	}

	void EnableCurrentLevel()
	{
		for (int i = 0; i < levels.Length; i++)
		{
			levels[i].gameObject.SetActive(false);
		}
		levels[currentLevel].gameObject.SetActive(true);
		levels[currentLevel].LevelStart();
		feedback.text = "LEVEL " + (currentLevel + 1).ToString();
	}

	public void Reset()
	{

	}

	void BackToMainMenu()
	{
		for (int i = 0; i < levels.Length; i++)
		{
			levels[i].gameObject.SetActive(false);
			levels[i].Reset();
			levels[i].alreadyUsedTime = 0;
		}
		menuScreen.gameObject.SetActive(true);
	}

	public IEnumerator TurnOnLight(GameObject light, float duration)
	{
		light.SetActive(true);
		yield return new WaitForSeconds(duration);
		light.SetActive(false);
	}
}


