using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public string unitName;
	public int unitLevel;

	public int maxHP;
	public int currentHP;

	public int attack;
	public int defence;
	public int speed;
	public string element;
	public string[] moves;
	public string[] moveElements;
	public string currentMove;


	//calc damage

	public bool TakeDamage(Unit enemy)
	{
		int dmg = (((((2 * enemy.unitLevel) /5) + 2 * enemy.attack /defence)/ 50) + 2);

		if (enemy.currentMove.Equals(enemy.moves[0]))
		{
			if (enemy.moveElements[0] == "Light" && element == "Dark")
			{
				dmg = dmg * 2;
			}
		}
		else if (enemy.currentMove.Equals(enemy.moves[1]))
		{
			if (enemy.moveElements[1] == "Light" && element == "Dark")
			{
				dmg = dmg * 2;
			}
		}

				currentHP -= dmg;

		if (currentHP <= 0)
			return true;
		else
			return false;
	}

	public void Heal(int amount)
	{
		currentHP += amount;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}

}
