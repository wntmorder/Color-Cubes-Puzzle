using Diamonds;
using UnityEngine;
using UnityEngine.UI;

public class TaskView : MonoBehaviour
{
    [SerializeField] private DiamondsConfig diamondsConfig;
    [SerializeField] private Image topDiamond;
    [SerializeField] private Image bottomDiamond;
    public void DisplayTask(Type topType, Type bottomType)
    {
        topDiamond.color = diamondsConfig.GetDiamondConfig(topType).color;
        bottomDiamond.color = diamondsConfig.GetDiamondConfig(bottomType).color;
    }
}
