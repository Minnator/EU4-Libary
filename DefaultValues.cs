using System.Diagnostics;

namespace EU4_Parse_Lib;
internal static class DefaultValues
{

    public static void DefaulMapModesKeys()
    {
        Vars.MapModeKeyMap = new()
        {
            { Keys.D1, null },
            { Keys.D2, null },
            { Keys.D3, null },
            { Keys.D4, null },
            { Keys.D5, null },
            { Keys.D6, null },
            { Keys.D7, null },
            { Keys.D8, null },
            { Keys.D9, null },
            { Keys.D0, null },
        };
    }

}
