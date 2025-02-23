using System;

public class Delegation
{
   public static Action<BugsStat> BugsStatChange;
   public static void CallBugsStatChange(BugsStat statName)
   {
          BugsStatChange?.Invoke(statName);
   }

   public static Action<int> SpawnBugs;
   public static void CallSpawnBugs(int num)
   {
          SpawnBugs?.Invoke(num);
   }

   public static Action ShowTips;
   public static void CallTipsShow()
   {
        ShowTips?.Invoke();
   }

}
