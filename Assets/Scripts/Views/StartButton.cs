using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEditor;

public class StartButton : MonoBehaviour 
{
    private Button button;
    private Vector3 upScale;

    private void Awake()
    {
        upScale = new Vector3(1.1f, 1.1f, 1f);
        button = gameObject.GetComponent<Button>();
    }
    public void LoadScene(SceneAsset scene)
    {
        SceneManager.instance.LoadScene(scene);
    }
    public void PointerEnter() => button.gameObject.transform.DOScale(upScale, 1f);
    public void PointerExit() => button.gameObject.transform.DOScale(Vector3.one, 1f).SetDelay(0.1f);
}
