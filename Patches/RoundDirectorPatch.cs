using System.Reflection;
using HarmonyLib;
using UnityEngine;
using BetterLobbies.Utils;
using Photon.Pun;
using Photon.Realtime;
using Steamworks.Data;
using Steamworks;
using System.Threading.Tasks;

namespace BetterLobbies.Patches
{
    [HarmonyPatch(typeof(RoundDirector), "Start")]
    public class RoundDirectorStartPatch
    {
        private static void Postfix()
        {
            if (SemiFunc.IsNotMasterClient()) return;
            
            bool gameOver = (bool)AccessTools.Field(typeof(RunManager), "gameOver").GetValue(RunManager.instance);
            if (RunManager.instance.levelCurrent == RunManager.instance.levelLobby)
            {
                SteamManager.instance.UnlockLobby();
            }
            else
            {
                SteamManager.instance.LockLobby();
            }
        }
    }
}