using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TaskView taskView;
    private void Start()
    {
        levelManager.LevelChanged += OnLevelChanged;
        taskView.gameObject.SetActive(true);
        OnLevelChanged(0);
    }
    private void OnLevelChanged(int levelNumber)
    {
        if (levelNumber < levelManager.LevelConfigs.Length)
        {
            DisplayTask(levelManager.LevelConfigs[levelNumber].LevelTasks[levelManager.CurrentTaskIndex]);
        }
        if (levelNumber == levelManager.LevelConfigs.Length)
        {
            LevelComplete();
        }
    }
    private void DisplayTask(LevelTask task)
    {
        taskView.DisplayTask(task.topType, task.bottomType);
    }
    private void LevelComplete()
    {
        text.gameObject.SetActive(true);
        taskView.gameObject.SetActive(false);
    }
}
