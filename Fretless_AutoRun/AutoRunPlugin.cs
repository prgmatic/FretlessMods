using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace Fretless_AutoRun;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class AutoRunPlugin : BaseUnityPlugin
{
    private new static ManualLogSource Logger;
        
    private void Awake()
    {
        Logger = base.Logger;
        Harmony.CreateAndPatchAll(typeof(AutoRunPlugin));
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }

    [HarmonyPatch(typeof(PlayerCharacterMovement), "HandleRunPressed")]
    [HarmonyPrefix]                      
    private static bool HandleRunPressedPatch(PlayerCharacterMovement __instance, bool pressed)
    {
        if (__instance.IsAutoMoving)
            return false;
        
        __instance.CurrentMovementType = pressed ? CharacterMovement.MovementTypes.Walking : 
                                                   CharacterMovement.MovementTypes.Running; 
        return false; // Returning false in prefix patches skips running the original code
    }
}
