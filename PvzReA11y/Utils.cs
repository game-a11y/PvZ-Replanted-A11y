using System.Diagnostics;
using System.Reflection;

namespace PvzReA11y;

public static class Utils
{

    public static MethodInfo GetMethod(string MethodName, BindingFlags bindingAttributes = BindingFlags.NonPublic | BindingFlags.Static)
    {
        StackTrace stackTrace = new StackTrace();
        Type callingType = stackTrace.GetFrame(1).GetMethod().DeclaringType;
        return callingType.GetMethod(MethodName, bindingAttributes);
    }

}
