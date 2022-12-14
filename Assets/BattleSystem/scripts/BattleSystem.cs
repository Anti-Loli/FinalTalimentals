using System.Collections;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
	//prefabs used for the player and enemy
	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	//transforms to spawn the player and enemy in the scene
	public Transform playerBattleStation;
	public Transform enemyBattleStation;

	//unit objects used for the player and enemy information
	Unit playerUnit;
	Unit enemyUnit;

	public Text dialogueText;//text in the top of the screen

	//The HUDs for both enemy and player that includes health bars and attack buttons
	public BattleHUD1 playerHUD;
	public BattleHUD1 enemyHUD;

	public BattleState state;

	public GameObject attackMenu;//menu that stores the attack buttons

	//attack buttons for the player
	public Button attackOne;
	public Button attackTwo;

	//sprites used to represent attacks
	public GameObject tornadoPunch;
	public GameObject charge;
	public GameObject lunapillarAttack;

	//sounds used for the attack and potion buttons
	public AudioSource attackOneSound;
	public AudioSource attackTwoSound;
	public AudioSource potionSound;

	//sound for the enemy attack
	public AudioSource enemyAttackSound;

	private bool playerPlayed;//boolean used to avoid enemy killing player if the spam E

	private void Awake()
    {
		//listeners for when the players attack buttons are clicked
		attackOne.onClick.AddListener(AttackOneClicked);
		attackTwo.onClick.AddListener(AttackTwoClicked);
	}

    void Start()
	{
		playerPlayed = false;
		state = BattleState.START;
		StartCoroutine(SetupBattle());
	}

    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.E))
		{
			 if (state == BattleState.WON)
			{
				SceneManager.LoadScene("Cave 1");
			}
			else if (state == BattleState.LOST)
			{
				SceneManager.LoadScene("Cave 1");
			}

		}
	}
    IEnumerator SetupBattle()
	{
		GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerGO.GetComponent<Unit>();

		GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyGO.GetComponent<Unit>();

		dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);

		yield return new WaitForSeconds(2f);

		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	IEnumerator PlayerAttack()
	{
		bool isDead = enemyUnit.TakeDamage(playerUnit);

		enemyHUD.SetHP(enemyUnit.currentHP);
		dialogueText.text = "The attack is successful!";

		yield return new WaitForSeconds(2f);

		charge.SetActive(false);
		tornadoPunch.SetActive(false);

		playerPlayed = true;

		if (isDead)
		{
			state = BattleState.WON;
			EndBattle();
		}
		else if (playerUnit.speed < enemyUnit.speed)
		{
			PlayerTurn();
		}
		else if (playerPlayed)
		{
			playerPlayed = false;
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	IEnumerator EnemyTurn()
	{
		dialogueText.text = enemyUnit.unitName + " attacks!";
		lunapillarAttack.SetActive(true);
		enemyAttackSound.Play();

		yield return new WaitForSeconds(1f);

		bool isDead = playerUnit.TakeDamage(enemyUnit);
		playerHUD.SetHP(playerUnit.currentHP);

		yield return new WaitForSeconds(1f);

		lunapillarAttack.SetActive(false);

		if (isDead)
		{
			state = BattleState.LOST;
			EndBattle();
		}
		else if (playerUnit.speed < enemyUnit.speed)
		{
			StartCoroutine(PlayerAttack());
		}
		else
		{
			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}
		
	}

	void EndBattle()
	{
		if (state == BattleState.WON)
		{
			dialogueText.text = "You won the battle! Press E to end battle.";
		}
		else if (state == BattleState.LOST)
		{
			dialogueText.text = "You were defeated. Press E to end battle.";
		}
	}

	void PlayerTurn()
	{
		dialogueText.text = "Choose an action:";
		attackMenu.SetActive(true);
	}

	IEnumerator PlayerHeal()
	{
		playerUnit.Heal(5);

		playerHUD.SetHP(playerUnit.currentHP);
		dialogueText.text = "You feel renewed strength!";

		yield return new WaitForSeconds(2f);

		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
		
	}

	public void OnAttackButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		attackMenu.SetActive(false);
		//compares the speeds of the player and enemy and decides which one goes first 
		if (playerUnit.speed > enemyUnit.speed)
        {
			StartCoroutine(PlayerAttack());
		}
		else if(playerUnit.speed < enemyUnit.speed)
        {
			StartCoroutine(EnemyTurn());
		}
	}

	public void OnHealButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		attackMenu.SetActive(false);
		potionSound.Play();
		StartCoroutine(PlayerHeal());
	}

	//these last two methods spawn the art for the players attacks
	private void AttackOneClicked()
    {
		playerUnit.currentMove = attackOne.GetComponentInChildren<Text>().text;
		charge.SetActive(true);
		attackOneSound.Play();
		Debug.Log(playerUnit.currentMove);
	}

	private void AttackTwoClicked()
    {
		playerUnit.currentMove = attackTwo.GetComponentInChildren<Text>().text;
		tornadoPunch.SetActive(true);
		attackTwoSound.Play();
		Debug.Log(playerUnit.currentMove);
	}
}
