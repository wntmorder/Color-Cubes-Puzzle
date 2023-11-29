using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DiamondsConfig", menuName = "Configs/DiamondsConfig", order = 1)]
public class DiamondsConfig : ScriptableObject
{
    [SerializeField] private DiamondConfig[] configs;
    private Dictionary<Diamonds.Type, DiamondConfig> configsByType;
    private void OnValidate()
    {
        configsByType = new();
        foreach (DiamondConfig config in configs)
        {
            configsByType[config.type] = config;
        }
    }
    public DiamondConfig GetDiamondConfig(Diamonds.Type type)
    {
        return configsByType.ContainsKey(type) ? configsByType[type] : configsByType[Diamonds.Type.Yellow];
    }
}
