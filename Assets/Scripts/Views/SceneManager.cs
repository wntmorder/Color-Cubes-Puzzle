using UnityEditor;
using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private UnityEngine.UI.Slider loadingBar;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(SceneAsset scene)
    {
        StartCoroutine(LoadSceneAsync(scene));
    }

    private IEnumerator LoadSceneAsync(SceneAsset scene)
    {
        loadingScreen.SetActive(true);
        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene.name);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress);
            loadingBar.value = progress;
            yield return null;
        }
    }
}
