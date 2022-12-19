using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableHandler : MonoBehaviour
{
    private CharacterStats charStats = null;
    private void Awake()
    {
        charStats = CharacterStats.Instance;
    }
    public void ConsumeItem(Item item)
    {
        charStats.AddStatAdditive(item.Stats);
        StatsUI.Instance.UpdateStatsUI();
        Tooltip.HideTooltip();
    }
}

public interface IConsumable
{
    void Consume();
}
