using Diamonds;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private DiamondsConfig diamondsConfig;
    [SerializeField] private Image prefab;
    [SerializeField] private Transform parent;
    [SerializeField] private TextMeshProUGUI text;
    public Transform ParentTransform => parent;
    public void DisplayTask(LevelTask task)
    {
        CreateAndPlaceTask(task.topType);
        CreateAndPlaceTask(task.bottomType);
    }
    private void CreateAndPlaceTask(Type type)
    {
        Image currentTask = Instantiate(prefab, parent);
        currentTask.transform.Rotate(new Vector3(0f, 0f, 45f));
        currentTask.color = diamondsConfig.GetDiamondConfig(type).color;

        text.gameObject.SetActive(false);
    }
    public void LevelComplete() => text.gameObject.SetActive(true);
}
