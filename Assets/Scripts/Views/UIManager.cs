using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TaskView taskViewPrefab;
    [SerializeField] private Transform tasksContainer;
    private readonly List<TaskView> taskViews = new();
    private readonly float minFadeValue = 0.25f;
    private readonly float maxFadeValue = 1.0f;
    private readonly float duration = 0.5f;

    private void Start()
    {
        levelManager.LevelChanged += OnLevelChanged;
        InitializeTaskViews();
        OnLevelChanged(levelManager.LevelNumber);
    }
    private void InitializeTaskViews()
    {
        for (int i = 0; i < levelManager.LevelConfigs[levelManager.LevelNumber].LevelTasks.Length; i++)
        {
            TaskView newTaskView = Instantiate(taskViewPrefab, tasksContainer);
            newTaskView.DisplayTask(levelManager.LevelConfigs[levelManager.LevelNumber].LevelTasks[i].topType, levelManager.LevelConfigs[levelManager.LevelNumber].LevelTasks[i].bottomType);
            taskViews.Add(newTaskView);
            FadeView(i, minFadeValue);
        }
    }
    private void OnLevelChanged(int levelNumber)
    {
        if (levelNumber <= levelManager.LevelConfigs.Length)
        {
            FadeView(levelManager.CurrentTaskIndex, maxFadeValue);
            for (int i = 0; i < taskViews.Count; i++)
            {
                if (i != levelManager.CurrentTaskIndex)
                {
                    FadeView(i, minFadeValue);
                }
            }
        }
        if (levelManager.CurrentTaskIndex >= levelManager.LevelConfigs[levelNumber].LevelTasks.Length)
        {
            LevelComplete();
        }
    }
    private void FadeView(int index, float endFadeValue)
    {
        taskViews[index].GetComponentsInChildren<Image>()[0].DOFade(endFadeValue, duration);
        taskViews[index].GetComponentsInChildren<Image>()[1].DOFade(endFadeValue, duration);
    }
    private void LevelComplete()
    {
        taskViews.Clear();
        text.gameObject.SetActive(true);
    }
}
