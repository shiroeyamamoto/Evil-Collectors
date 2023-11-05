using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPItem : ConsumableItem2 {
   public override bool IsCanUse(Player player)
   {
      if (player.CurrentInfo.health > 0)
      {
         player.IncreaceHp(value);
         return true;
      }

      return false;
   }
}
