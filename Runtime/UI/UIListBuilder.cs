using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HouraiTeahouse.UI {

/// <summary>
/// A behaviour that builds lists of UI displays.
/// </summary>
/// <typeparam name="T">the type of the display type.k</typeparam>
/// <typeparam name="TParam">the type of the parameters when creating the displays</typeparam>
public abstract class UIListBuilder<T, TParam> : MonoBehaviour where T : Component {

    /// <summary>
    /// The container that houses the elements.
    /// </summary>
    public RectTransform Container;
    public T Prefab;

    /// <summary>
    /// How many displays in the list are currently displayed.
    /// </summary>
    public int ActiveCount { get; private set; }

    List<T> _displays;

    /// <summary>
    /// Unity Callback. Called on object instantiation.
    /// </summary>
    protected virtual void Awake() {
        _displays = new List<T>();
        if (Container == null) {
            Container = transform as RectTransform;
        }
    }

    /// <summary>
    /// Unity Callback. Called when the behaviour is enabled.
    /// </summary>
    protected virtual void OnEnable() {
        for (var i = 0; i < _displays.Count; i++) {
            if (_displays[i] == null) continue;
            _displays[i].gameObject.SetActive(i <= ActiveCount);
        }
    }

    /// <summary>
    /// Unity Callback. Called when the behaviour is disabled.
    /// </summary>
    protected virtual void OnDisable() {
        for (var i = 0; i < _displays.Count; i++) {
            if (_displays[i] == null) continue;
            _displays[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Unity Callback. Called when the behaviour is destroyed.
    /// </summary>
    protected virtual void OnDestory() {
        foreach (var display in _displays) {
            if (display != null) Destroy(display.gameObject);
        }
    }

    public virtual void BuildList(IEnumerable<TParam> displayParams) { 
        int idx = 0;
        foreach (var elementParams in displayParams) {
            T display;
            if (idx < _displays.Count) {
                display = _displays[idx];
            } else {
                display = CreateElement();
                _displays.Add(display);
            } 
            display.gameObject.SetActive(true);
            UpdateDisplay(display, elementParams);
            idx++;
        }
        ActiveCount = idx;
        Debug.LogWarning(_displays);
        for (; idx < _displays.Count; idx++) {
            _displays[idx].gameObject.SetActive(false);
        }
    } 

    protected abstract void UpdateDisplay(T display, TParam displayParams);

    protected virtual T CreateElement() {
        var display = Instantiate(Prefab);
        display.transform.SetParent(Container, false);
        return display;
    }

}

}
