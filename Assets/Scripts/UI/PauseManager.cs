using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixerSnapshot paused, unpaused;
    [SerializeField]
    private CanvasGroup pauseGroup;
    [SerializeField]
    private CanvasGroup settingGroup;

    //Canvas canvas;
    private bool isPaused = false;

    private Stack<CanvasGroup> canvasGroupStack = new Stack<CanvasGroup>();
    private List<CanvasGroup> canvasGroupList = new List<CanvasGroup>();

    private void Start()
    {
        canvasGroupList.Add(pauseGroup);
        canvasGroupList.Add(settingGroup);
        DesplayMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Esc();
        }
    }

    private void Lowpass()
    {
        if (Time.timeScale == 0)
        {
            paused.TransitionTo(.01f);
        }
        else
        {
            unpaused.TransitionTo(.01f);
        }
    }

    public void Esc()
    {
        if (!isPaused && canvasGroupStack.Count == 0)
        {
            isPaused = !isPaused;
            canvasGroupStack.Push(pauseGroup);
        }
        else
        {
            if (canvasGroupStack.Count > 0)
                canvasGroupStack.Pop();
        }
        if (canvasGroupStack.Count == 0)
        {
            Pause();
        }
        DesplayMenu();
    }

    public void Pause()
    {
        isPaused = !isPaused;
        if (canvasGroupStack.Count > 0)
        {
            canvasGroupStack.Pop();
        }
        DesplayMenu();
    }

    public void Setting()
    {
        canvasGroupStack.Push(settingGroup);
        DesplayMenu();
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void DesplayMenu()
    {
        foreach (var item in canvasGroupList)
        {
            item.alpha = 0;
            item.interactable = false;
            item.blocksRaycasts = false;
        }

        if (canvasGroupStack.Count > 0)
        {
            CanvasGroup cg = canvasGroupStack.Peek();
            cg.alpha = 1;
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }

        Time.timeScale = isPaused ? 0 : 1;

        Lowpass();
    }
}
