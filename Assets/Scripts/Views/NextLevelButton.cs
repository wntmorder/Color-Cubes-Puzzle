using UnityEngine;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{
    private UIManager uiManager;
    private Button button;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(uiManager.StartNewLevel);
    }
}
