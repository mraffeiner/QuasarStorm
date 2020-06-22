using System.Runtime.InteropServices;

public static class MobileDetection
{
    [DllImport("__Internal")]
    private static extern bool IsMobile();

    public static bool isMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
             return IsMobile();
#endif
        return false;
    }
}
