using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using BepInEx.Configuration;

namespace BetterLobbies.Utils
{
    public static class ConfigManager
    {
        public static ConfigEntry<int> configLobbySize { get; private set; }
        public static ConfigEntry<string> configLobbyPrivacy { get; private set; }
        public static ConfigEntry<bool> configLobbyModeration { get; private set; }
        public static ConfigEntry<string> configHealthRespawnMethod { get; private set; }
        public static ConfigEntry<int> configPHealthRespawnValue { get; private set; }
        public static ConfigEntry<string> configDisplayName { get; private set; }
        
        // Experimental
        public static ConfigEntry<bool> configLateJoin { get; private set; }
        
        public static void Initialize(ConfigFile config)
        {
            configLobbySize = config.Bind("Lobby", "MaximumLobbySize", 6,
                new ConfigDescription(
                    "The maximum number of players that can join your lobby. Vanilla is 6. Lobbies larger than twenty may suffer performance issues.",
                    new AcceptableValueRange<int>(1, 100)
                ));
            
            configLobbyPrivacy = config.Bind("Lobby", "LobbyPrivacy", "FriendsOnly",
                new ConfigDescription(
                    "Change the privacy level of the lobby. Vanilla is FriendsOnly.",
                    new AcceptableValueList<string>("Private", "FriendsOnly, Public")
                ));
            
            configLobbyModeration = config.Bind("Moderation", "ModerationInLobbiesEnabled", true,
                new ConfigDescription(
                    "Kick and ban players in your lobby."
                ));
            
            configDisplayName = config.Bind("Game", "SetDisplayName", "",
                new ConfigDescription(
                    "Set your display name in the game."
                ));
            
            configHealthRespawnMethod = config.Bind("Game", "HealthOnRespawnMethod", "vanilla",
                new ConfigDescription(
                    "Which method should be used for calculating the health on respawn?",
                    new AcceptableValueList<string>("vanilla", "integer, percentage")
                ));
            
            configPHealthRespawnValue = config.Bind("Game", "HealthOnRespawnValue", 1,
                new ConfigDescription(
                    "How much health the player should receive on respawn. Setting to vanilla ignores this value.",
                    new AcceptableValueRange<int>(1, 100)
                ));
            
            configLateJoin = config.Bind("Experimental", "LateJoiningEnabled", false,
                new ConfigDescription(
                    "Allows players to join your lobby whilst you're in the truck (post-shop, pre-round start)."
                ));
        }
    }
}