using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TaskView taskViewPrefab;
    [SerializeField] private Transform tasksContainer;
    [SerializeField] private Controller controller;
    [SerializeField] private GameObject levelWinPopUp;
    [SerializeField] private GameObject levelsCompletePopUp;
    [SerializeField] private TextMeshProUGUI levelNumberText;
    private readonly List<TaskView> taskViews = new();
    private readonly float minFadeValue = 0.25f;
    private readonly float maxFadeValue = 1.0f;
    private readonly float duration = 1.15f;
    private readonly Vector3 movePopUpPosition = new(0f, 1800f, 0f);

    private void Start()
    {
        levelManager.LevelChanged += OnLevelChanged;
        StartLevel();
    }

    private void StartLevel()
    {
        if (levelManager.LevelNumber >= levelManager.LevelConfigs.Length - 1)
        {
            MovePopup(levelsCompletePopUp, canvas.transform.position);
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
        popup.transform.DOMove(position, duration);
    }

    private void LevelComplete()
    {
        levelNumberText.DOFade(0f, duration);
        MovePopup(levelWinPopUp, canvas.transform.position);
        controller.gameObject.SetActive(false);

        for (int i = 0; i < tasksContainer.childCount; i++)
        {
            Destroy(tasksContainer.GetChild(i).gameObject);
        }
        taskViews.Clear();
    }

    public void StartNewLevel()
    {
        MovePopup(levelWinPopUp, canvas.transform.position - movePopUpPosition);
        StartLevel();
    }

    public void CloseGame()
    {
        MovePopup(levelsCompletePopUp, canvas.transform.position + movePopUpPosition);
        Application.Quit();
    }

    private void OnDestroy()
    {
        if (levelNumberText != null)
        {
            DOTween.Kill(levelNumberText);
        }

        if (levelWinPopUp != null)
        {
            DOTween.Kill(levelWinPopUp.transform);
        }

        if (levelsCompletePopUp != null)
        {
            DOTween.Kill(levelsCompletePopUp.transform);
        }

        foreach (var taskView in taskViews)
        {
            if (taskView != null)
            {
                DOTween.Kill(taskView);
            }
        }
    }
}
