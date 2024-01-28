using UnityEngine;

public static class CanvasGroupHideShow
{
    public static void HideGroup(CanvasGroup groud)
    {
        groud.alpha = 0;
        groud.blocksRaycasts = false;
    }

    public static void ShowGroup(CanvasGroup groud)
    {
        groud.alpha = 1;
        groud.blocksRaycasts = true;
    }
}

