using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

/*Arquivo de verificação de trophy
*/

public class Archievements : MonoBehaviour
{
    void Awake()
    {
        //
        // Tell Steamworks to call this function when something happens
        //
        Steamworks.SteamUserStats.OnAchievementProgress += AchievementChanged;
    }

    private void AchievementChanged(Steamworks.Data.Achievement ach, int currentProgress, int progress)
    {
        if (ach.State)
        {
            Debug.Log($"{ach.Name} WAS UNLOCKED!");
        }
    }
}