using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{
    private UIManager uiManager;
    private Button button;
    private Vector3 upScale;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        upScale = new Vector3(1.1f, 1.1f, 1f);
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(uiManager.StartNewLevel);
    }
    public void PointerEnter() => button.gameObject.transform.DOScale(upScale, 1f);
    public void PointerExit() => button.gameObject.transform.DOScale(Vector3.one, 1f).SetDelay(0.1f);
}
