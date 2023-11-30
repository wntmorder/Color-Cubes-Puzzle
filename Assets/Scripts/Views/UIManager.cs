using Diamonds;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private DiamondsConfig diamondsConfig;
    [SerializeField] private Image prefab;
    [SerializeField] private Transform parent;
    public void DisplayTasks(LevelTask task)
    {
        CreateAndPlaceTask(task.topType, new Vector2(-Screen.width * 0.1f, Screen.height * 0.25f));
        CreateAndPlaceTask(task.bottomType, new Vector2(Screen.width * 0.1f, Screen.height * 0.25f));
    }
    private void CreateAndPlaceTask(Type type, Vector2 position)
    {
        Image currentTask = Instantiate(prefab, parent);
        currentTask.transform.localPosition = position;
        currentTask.transform.Rotate(new Vector3(0f, 0f, 45f));
        currentTask.GetComponent<Image>().color = diamondsConfig.GetDiamondConfig(type).color;
    }
}
