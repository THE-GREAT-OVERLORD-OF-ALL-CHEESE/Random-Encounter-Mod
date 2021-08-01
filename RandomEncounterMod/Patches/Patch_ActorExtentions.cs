using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;

[HarmonyPatch(typeof(ActorExtensions), "DebugName")]
class Patch_ActorExtensions_DebugName
{
    [HarmonyPrefix]
    static bool Prefix(ref string __result)
    {
        __result = "";
        return false;
    }
}
