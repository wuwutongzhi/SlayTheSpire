using System.Collections.Generic;
using UnityEngine;

public class StatusEffectsUI : MonoBehaviour
{
    [SerializeField] private StatusEffectUI statusEffectUI;
    [SerializeField] Sprite armorSprite, burnSprite;
    private Dictionary<StatusEffectType, StatusEffectUI> statusEffectUIs = new();
    public void UpdateStatusEffectUI(StatusEffectType statusEffectType, int stackCount)
    {
        if(stackCount == 0)
        {
            if (statusEffectUIs.ContainsKey(statusEffectType))
            {
                StatusEffectUI statusEffectUI = statusEffectUIs[statusEffectType];
                statusEffectUIs.Remove(statusEffectType);
                Destroy(statusEffectUI.gameObject);
            }
        }
        else
        {
            if (!statusEffectUIs.ContainsKey(statusEffectType))
            {
                StatusEffectUI newStatusEffectUI = Instantiate(statusEffectUI, transform);
                statusEffectUIs.Add(statusEffectType, newStatusEffectUI);
            }
            Sprite sprite = statusEffectType switch
            {
                StatusEffectType.ARMOR => armorSprite,
                StatusEffectType.BURN => burnSprite,
                _ => null
            };
            statusEffectUIs[statusEffectType].Set(sprite, stackCount);
        }
    }
}
