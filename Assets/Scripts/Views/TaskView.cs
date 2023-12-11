using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TaskView : MonoBehaviour
{
    [SerializeField] private ModelObjectsConfig ModelObjectsConfig;
    [SerializeField] private Image topModelObject;
    [SerializeField] private Image bottomModelObject;
    private readonly float duration = 0.5f;
    public void DisplayTask(ModelObjectType topType, ModelObjectType bottomType)
    {
        topModelObject.DOColor(ModelObjectsConfig.GetModelObjectConfig(topType).color, duration);
        bottomModelObject.DOColor(ModelObjectsConfig.GetModelObjectConfig(bottomType).color, duration);
    }
}
