using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine.UIElements;

namespace Fretless_EnableDebugMenu;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class EnableDebugMenuPlugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
        
    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Harmony.CreateAndPatchAll(typeof(EnableDebugMenuPlugin));
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }

    [HarmonyPatch(typeof(PauseMenuUI), "Start")]
    [HarmonyPrefix]
    private static bool PauseMenuPatch(UnityEngine.UI.Button ___openDebugMenuButton)
    {
        ___openDebugMenuButton.gameObject.SetActive(true);
        return true;
    }
}
