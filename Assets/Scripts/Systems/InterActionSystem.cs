using UnityEngine;

public class InterActionSystem : Singleton<InterActionSystem>
{
    public bool PlayerIsDragging { get; set; } = false;
    public bool PlayerCanInteract()
    {
        if (!ActionSystem.Instance.IsPerforming) return true;
        else return false;
    }
    public bool PlayerCanHover()
    {
        if (PlayerIsDragging) return false;
        return true;
    }

}
