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
    [HarmonyPatch(typeof(RunManager), "ChangeLevel")]
    public class ChangeLevelPatch
    {
        private static void Postfix(SteamManager __instance, RunManager ___instance, RunManager.ChangeLevelType _changeLevelType)
        {
            bool gameOver = (RunManager)AccessTools.Field(typeof(RunManager), "gameOver").GetValue(__instance);
            if (gameOver && ___instance.levelCurrent != ___instance.levelArena)
            {
                if (___instance.levelCurrent == ___instance.levelLobby)
                {
                    LobbyManager.SetupLobby(__instance, true);
                }
                else
                {
                    LobbyManager.SetupLobby(__instance, false);
                }
            }
        }
    }
}