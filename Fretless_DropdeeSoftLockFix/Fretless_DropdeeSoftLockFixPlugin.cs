using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace Fretless_DropdeeSoftLockFix;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Fretless_DropdeeSoftLockFixPlugin : BaseUnityPlugin
{
    private new static ManualLogSource Logger;
        
    private void Awake()
    {
        Logger = base.Logger;
        Harmony.CreateAndPatchAll(typeof(Fretless_DropdeeSoftLockFixPlugin));
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }
    
    [HarmonyPatch(typeof(CaptainCutscene2_TimelineCtrl), "GameInitializationManager_OnAfterLoad")]
    [HarmonyPrefix]
    private static bool Cutscene(ControllerStateHandler __instance)
    {
        var questManager = GenericSingletonClass<QuestManager>.Instance;
        Logger.LogInfo(questManager.QuestIsActive(QuestID.FindBassCamp_TalkToStrum));
        Logger.LogInfo(questManager.QuestComplete(QuestID.FindBassCamp_TalkToStrum));
        return !questManager.QuestIsActive(QuestID.FindBassCamp_TalkToStrum) &&
               !questManager.QuestComplete(QuestID.FindBassCamp_TalkToStrum);
    } 
}
