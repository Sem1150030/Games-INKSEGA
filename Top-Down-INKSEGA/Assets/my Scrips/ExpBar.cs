using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public Slider slider;
    
    public void UpdateExpBar(int currentExp, int maxExp)
    {
        slider.value = currentExp;
    }
    
    public void SetMaxExp(int health)
    {
        slider.maxValue = health;
    }
}
