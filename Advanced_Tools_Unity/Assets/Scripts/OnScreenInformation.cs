using System;
using TMPro;
using UnityEngine;

public class OnScreenInformation : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    [SerializeField] private TMP_Text _fpsText;
    [SerializeField] private TMP_Text _objectCountText;
    


    private void Update()
    {
        _fpsText.text = $"FPS: {Mathf.RoundToInt(_spawner.CurrentFPS)}";
        _objectCountText.text = $"Objects: {_spawner.ObjectsCount}";
    }
}
