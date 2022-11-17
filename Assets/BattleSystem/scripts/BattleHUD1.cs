using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD1 : MonoBehaviour
{

	public Text nameText;
	public Text levelText;
	public Text elementText;
	public Slider hpSlider;

	public Text attackOne;
	public Text attackTwo;

	public void SetHUD(Unit unit)
	{
		nameText.text = unit.unitName;
		levelText.text = "Lvl " + unit.unitLevel;
		elementText.text = unit.element;
		hpSlider.maxValue = unit.maxHP;
		hpSlider.value = unit.currentHP;
		attackOne.text = unit.moves[0];
		attackTwo.text = unit.moves[(unit.moves.Length - 1)];
	}

	public void SetHP(int hp)
	{
		hpSlider.value = hp;
	}

}
