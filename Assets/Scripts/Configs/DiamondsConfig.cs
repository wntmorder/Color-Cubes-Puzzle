using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DiamondsConfig", menuName = "Configs/DiamondsConfig", order = 1)]
public class DiamondsConfig : ScriptableObject
{
    [SerializeField] private DiamondConfig[] configs;

    private Dictionary<Diamonds.Type, DiamondConfig> configsByType;

    public int Count => configsByType.Keys.Count;

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
        if (configsByType.ContainsKey(type))
        {
            return configsByType[type];
        }
        //Debug.Log($"Type [{type}] is not set");
        return configsByType[Diamonds.Type.Yellow];
    }
    public DiamondConfig GetDiamondConfig(int typeNumber)
    {
        typeNumber = Mathf.Clamp(typeNumber, 0, Count);
        return GetDiamondConfig((Diamonds.Type)typeNumber);
    }
}
