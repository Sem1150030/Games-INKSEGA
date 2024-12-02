using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
   public static GoldManager Instance;
   
   public delegate void GoldChangeHandler(int amount);
   public event GoldChangeHandler OnGoldChange;
   
   private void Awake()
   {
       if (Instance != null && Instance != this)
       {
           Destroy(this);
       }
       else
       {
           Instance = this;
       }
   }
   
    public void AddGold(int amount)
    {
         OnGoldChange?.Invoke(amount);
    }
}
