namespace BetterLobbies
{
    using BepInEx;
    using BepInEx.Configuration;
    using BepInEx.Logging;
    using HarmonyLib;
    using Patches;
    using Utils;
    
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInProcess("REPO.exe")]
    [BepInIncompatibility("zelofi.MorePlayers")]
    public class Plugin : BaseUnityPlugin
    {
        private static ManualLogSource BLLog;
        private static Harmony _harmony;

        private void Awake()
        {
            BLLog = BepInEx.Logging.Logger.CreateLogSource($"{MyPluginInfo.PLUGIN_NAME}");
            // Plugin startup logic
            BLLog.LogInfo($"{MyPluginInfo.PLUGIN_GUID} has loaded successfully.");

            ConfigManager.Initialize(Config);
            BLLog.LogInfo($"Config has loaded successfully.");

        _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        _harmony.PatchAll(typeof(LockLobbyPatch));
        _harmony.PatchAll(typeof(UnlockLobbyPatch));
        _harmony.PatchAll(typeof(HostLobbyPatch));
        _harmony.PatchAll(typeof(ChangeLevelPatch));
        }
    }
}