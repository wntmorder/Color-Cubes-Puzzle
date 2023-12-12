using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private TaskView taskViewPrefab;
    [SerializeField] private Transform tasksContainer;
    [SerializeField] private Controller controller;
    [SerializeField] private GameObject levelWinPopUp;
    [SerializeField] private GameObject levelsCompletePopUp;
    [SerializeField] private TextMeshProUGUI levelNumberText;
    private readonly List<TaskView> taskViews = new();
    private readonly float minFadeValue = 0.25f;
    private readonly float maxFadeValue = 1.0f;
    private readonly float duration = 0.5f;
    private readonly float movePopUpDuration = 1.5f;
    private readonly Vector3 defaultPosition = new Vector3(240f, 400f, 0f);

    private void Start()
    {
        levelManager.LevelChanged += OnLevelChanged;
        StartLevel();
    }
    public void StartLevel()
    {
        MovePopup(levelWinPopUp, new Vector3(240f, -800f, 0f));
        if (levelManager.LevelNumber >= levelManager.LevelConfigs.Length - 1)
        {
            MovePopup(levelsCompletePopUp, defaultPosition);
            return;
        }
        levelManager.StartNewLevel();
        levelNumberText.text = (levelManager.LevelNumber + 1).ToString();
        levelNumberText.DOFade(maxFadeValue, duration);
        InitializeTaskViews();
        OnLevelChanged(levelManager.LevelNumber);
        controller.gameObject.SetActive(true);
    }
    private void InitializeTaskViews()
    {
        for (int i = 0; i < levelManager.LevelConfigs[levelManager.LevelNumber].LevelTasks.Length; i++)
        {
            TaskView newTaskView = Instantiate(taskViewPrefab, tasksContainer);
            newTaskView.DisplayTask(levelManager.LevelConfigs[levelManager.LevelNumber].LevelTasks[i].topType,
                                    levelManager.LevelConfigs[levelManager.LevelNumber].LevelTasks[i].bottomType);
            taskViews.Add(newTaskView);
            FadeView(i, minFadeValue);
        }
    }
    private void OnLevelChanged(int levelNumber)
    {
        if (levelManager.CurrentTaskIndex >= levelManager.LevelConfigs[levelNumber].LevelTasks.Length)
        {
            LevelComplete();
            return;
        }

        FadeView(levelManager.CurrentTaskIndex, maxFadeValue);
        for (int i = 0; i < taskViews.Count; i++)
        {
            if (i != levelManager.CurrentTaskIndex)
            {
                FadeView(i, minFadeValue);
            }
        }
    }
    private void FadeView(int index, float endFadeValue)
    {
        taskViews[index].GetComponentsInChildren<Image>()[0].DOFade(endFadeValue, duration);
        taskViews[index].GetComponentsInChildren<Image>()[1].DOFade(endFadeValue, duration);
    }
    private void MovePopup(GameObject popup, Vector3 position)
    {
        popup.transform.DOMove(position, movePopUpDuration);
    }
    private void LevelComplete()
    {
        levelNumberText.DOFade(0f, duration);
        MovePopup(levelWinPopUp, defaultPosition);
        controller.gameObject.SetActive(false);
        for (int i = 0; i < tasksContainer.childCount; i++)
        {
            Destroy(tasksContainer.GetChild(i).gameObject);
        }
        taskViews.Clear();
    }
    public void CloseGame()
    {
        MovePopup(levelsCompletePopUp, new Vector3(240f, 1200f, 0f));
        Application.Quit();
    }
}
