using System;
using System.Collections.Generic;
using UnityEngine;

//事件处理器，用于处理事件的注册和调用
public class EventsNotifier : SingletonMonobehaviour<EventsNotifier>{
    #region AfterSceneLoadEvent
    public delegate void AfterSceneLoadDelegate();

    private event AfterSceneLoadDelegate AfterSceneLoadEvent;
    
    public void CallAfterSceneLoadEvent()
    {
        if(AfterSceneLoadEvent != null)
        {
            AfterSceneLoadEvent.Invoke();
        }
    }
    #endregion
    #region BeforeFadeOutEvent
    public delegate void BeforeFadeOutDelegate();

    private event BeforeFadeOutDelegate BeforeFadeOutEvent;

    public void CallBeforeFadeOutEvent()
    {
        if(BeforeFadeOutEvent != null)
        {
            BeforeFadeOutEvent();
        }
    }
    #endregion
    #region BeforeSceneUnloadEvent
    public delegate void BeforeSceneUnloadDelegate();

    private event BeforeSceneUnloadDelegate BeforeSceneUnloadEvent;

    public void CallBeforeSceneUnloadEvent()
    {
        if(BeforeSceneUnloadEvent != null)
        {
            BeforeSceneUnloadEvent();
        }
    }
    #endregion
    #region AfterFadeInEvent
    public delegate void AfterFadeInDelegate();

    private event AfterFadeInDelegate AfterFadeInEvent;

    public void CallAfterFadeInEvent()
    {
        if(AfterFadeInEvent != null)
        {
            AfterFadeInEvent();
        }
    }
    #endregion
    
    public delegate void ButtonClickEventDelegate_SceneChange(string sceneName);
    public event ButtonClickEventDelegate_SceneChange OnClickEvent_SceneChange;

    public void CallForButtonClickEvent_SceneChange(string sceneName)
    {
        if(OnClickEvent_SceneChange != null)
        {
            OnClickEvent_SceneChange(sceneName);
        }
    }

    public delegate void ButtonClickEventDelegate_VolumeChange(string para);

    public event ButtonClickEventDelegate_VolumeChange OnClickEvent_VolumeChange;

    public void CallForButtonClickEvent_VolumnChange(string para)
    {
        if(OnClickEvent_VolumeChange != null)
        {
            OnClickEvent_VolumeChange(para);
        }
    }
}