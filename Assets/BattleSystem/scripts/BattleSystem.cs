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
			if(state == BattleState.PLAYERTURN)
            {
				if(playerUnit.speed < enemyUnit.speed)
                {
					PlayerTurn();
                }
                else if(playerPlayed)
                {
					playerPlayed = false;
					state = BattleState.ENEMYTURN;
					StartCoroutine(EnemyTurn());
				}
            }
			else if(state == BattleState.ENEMYTURN)
            {
				if (playerUnit.speed < enemyUnit.speed)
				{
					StartCoroutine(PlayerAttack());
				}
				else
				{
					state = BattleState.PLAYERTURN;
					PlayerTurn();
				}
			}
			else if (state == BattleState.WON)
			{
				SceneManager.LoadScene("Hunters Dev Room");
			}
			else if (state == BattleState.LOST)
			{
				SceneManager.LoadScene("Hunters Dev Room");
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
	}

	IEnumerator EnemyTurn()
	{
		dialogueText.text = enemyUnit.unitName + " attacks!";
		lunapillarAttack.SetActive(true);

		yield return new WaitForSeconds(1f);

		bool isDead = playerUnit.TakeDamage(enemyUnit);
		playerHUD.SetHP(playerUnit.currentHP);

		yield return new WaitForSeconds(1f);

		if (isDead)
		{
			state = BattleState.LOST;
			EndBattle();
		}

		lunapillarAttack.SetActive(false);
	}

	void EndBattle()
	{
		if (state == BattleState.WON)
		{
			dialogueText.text = "You won the battle!";
		}
		else if (state == BattleState.LOST)
		{
			dialogueText.text = "You were defeated.";
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
		StartCoroutine(PlayerHeal());
	}

	private void AttackOneClicked()
    {
		playerUnit.currentMove = attackOne.GetComponentInChildren<Text>().text;
		charge.SetActive(true);
		Debug.Log(playerUnit.currentMove);
	}

	private void AttackTwoClicked()
    {
		playerUnit.currentMove = attackTwo.GetComponentInChildren<Text>().text;
		tornadoPunch.SetActive(true);
		Debug.Log(playerUnit.currentMove);
	}
}
