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
    public class LobbyManager
    {
        public static void SetupLobby(SteamManager lobby, bool joinable)
        {
            var currentLobby = (Lobby)AccessTools.Field(typeof(SteamManager), "currentLobby").GetValue(lobby);
            switch (ConfigManager.configLobbyPrivacy.Value)
            {
                case "Public":
                    currentLobby.SetPublic();
                    break;
                case "FriendsOnly":
                    currentLobby.SetFriendsOnly();
                    break;
                case "Private":
                    currentLobby.SetPrivate();
                    break;
                default:
                    currentLobby.SetFriendsOnly();
                    break;
            }

            if (joinable)
            {
                currentLobby.SetJoinable(true);
            }
            else
            {
                currentLobby.SetJoinable(false);
            }
        }
    }
    
    [HarmonyPatch(typeof(SteamManager), "LockLobby")]
    public class LockLobbyPatch
    {
        private static bool Prefix(SteamManager __instance)
        {
            // FieldInfo currentLobby = typeof(SteamManager).GetField("currentLobby", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            Debug.Log("Steam: Locking lobby...");
            LobbyManager.SetupLobby(__instance, false);
            return true;
        }
    }

    [HarmonyPatch(typeof(SteamManager), "UnlockLobby")]
    public class UnlockLobbyPatch
    {
        private static bool Prefix(SteamManager __instance)
        {
            // FieldInfo currentLobby = typeof(SteamManager).GetField("currentLobby", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            Debug.Log("Steam: Unlocking lobby...");
            LobbyManager.SetupLobby(__instance, true);
            return true;
        }
    }
    
    [HarmonyPatch(typeof(SteamManager), "HostLobby")]
    public class HostLobbyPatch()
    {
        static async void HostLobbyAsync(SteamManager __instance)
        {
            Debug.Log("Steam: Hosting lobby...");
            Lobby? lobby = await SteamMatchmaking.CreateLobbyAsync(ConfigManager.configLobbySize.Value);

            if (!lobby.HasValue)
            {
                Debug.LogError("Lobby created but not correctly instantiated.");
                return;
            }
            
            __instance.UnlockLobby();
        }
    }
}