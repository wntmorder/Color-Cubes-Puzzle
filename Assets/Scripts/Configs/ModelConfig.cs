using Diamonds;
using UnityEngine;

[CreateAssetMenu(fileName = "ModelConfig", menuName = "Configs/ModelConfig", order = 1)]
public class ModelConfig : ScriptableObject
{
    [SerializeField] private Type[] configs;
    public Type[] Configs
    {
        get => configs;
        private set => configs = value;
    }
}
