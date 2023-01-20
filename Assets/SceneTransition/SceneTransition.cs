using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Text _loadingPercentage;
    [SerializeField] private Image _loadingProgressBar;
    [SerializeField] private Image _imageFade;

    private static SceneTransition _instance;
    private static bool shouldPlayOpeningAnimation = false;

    private AsyncOperation _loadingSceneOperation;
    private Animator _animator;

    private void Start()
    {
        _instance = this;
        _animator = GetComponent<Animator>();

        if (shouldPlayOpeningAnimation == true)
        {
            shouldPlayOpeningAnimation = false;
            _animator.SetTrigger("sceneOpening");
        }
    }

    private void Update()
    {
        if (_loadingSceneOperation != null)
        {
            _loadingPercentage.text = Mathf.RoundToInt(_loadingSceneOperation.progress * 100) + "%";
            _loadingProgressBar.fillAmount = _loadingSceneOperation.progress;
        }
    }

    public static void SwitchToScene(string sceneName)
    {
        if (_instance._loadingSceneOperation == null)
        {
            //_instance._imageFade.raycastTarget = true;
            _instance._animator.SetTrigger("sceneClosing");
            _instance._loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
            _instance._loadingSceneOperation.allowSceneActivation = false;
        }
    }

    public void OnClosingAnimationOver()
    {
        shouldPlayOpeningAnimation = true;
        _loadingSceneOperation.allowSceneActivation = true;
    }
}
