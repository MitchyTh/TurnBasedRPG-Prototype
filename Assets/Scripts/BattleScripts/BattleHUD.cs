using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI hpDigit;
    public Slider hpSlider;

    public void SetHUD(Unit unit){
        nameText.text = unit.unitName;
        hpDigit.text = unit.currentHP.ToString();
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }

    public void SetHP(int hp){
        if(hp <= 0)
        {
            hp = 0;
        }
        hpSlider.value = hp;
        hpDigit.text = hp.ToString();
    }
}
