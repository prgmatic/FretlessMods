using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace Fretless_RemoveArtificialLoadTime;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class RemoveArtificialLoadTimePlugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
        
    private void Awake()
    {
        Logger = base.Logger;
        Harmony.CreateAndPatchAll(typeof(RemoveArtificialLoadTimePlugin));
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }
    
    [HarmonyPatch(typeof(LoadScreenManager), "Awake")]
    [HarmonyPrefix]
    private static bool AwakePatch(ref float ___LoadingScreenWaitTime)
    {
        var oldWaitTime = ___LoadingScreenWaitTime;
        ___LoadingScreenWaitTime = 0f;
        Logger.LogInfo($"Change Load Wait Time: {oldWaitTime}->{___LoadingScreenWaitTime}");
        return true;
    }
}
