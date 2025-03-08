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
    using BepInEx.Logging;
    
    [HarmonyPatch(typeof(NetworkConnect), "TryJoiningRoom")]
    public class TryJoiningRoomPatch
    {
        static bool Prefix(ref string roomname)
        {
            if(string.IsNullOrEmpty(roomname))
            {
                // BLLog.LogError("RoomName is null or empty, using previous method!");
                return true;
            }
                
            if(ConfigManager.configLobbySize.Value == 0)
            {
                // mls.LogError("The MaxPlayers config is null or empty, using previous method!");
                return true;
            }

            if(NetworkConnect.instance != null)
            {
                PhotonNetwork.JoinOrCreateRoom(roomname, new RoomOptions
                {
                    MaxPlayers = ConfigManager.configLobbySize.Value
                }, TypedLobby.Default, null);

                return false;
            }
            else
            {
                // mls.LogError("NetworkConnect instance is null, using previous method!");
                return true;
            }
        }
    }
}