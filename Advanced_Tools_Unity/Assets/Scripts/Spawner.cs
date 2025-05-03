using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    //Data Saving variables
    [SerializeField] private string _fileName = "physics_test_unity.txt";
    [SerializeField] private string _filePath = Application.streamingAssetsPath;
    [SerializeField] private string _delimiter = ",";

    [SerializeField] private int _dataSize = 5000;
    [Tooltip("The amount of objects after a record is stored in the data array")]
    [SerializeField] private int _samplingRate = 50;

    private List<string> _dataList = new List<string>();
    private int _objectsCount;
    
    //Spawning variables
    [SerializeField] private GameObject _prefab;
    [SerializeField] private bool _isSpawningEnabled;
    [SerializeField] private int _objectsPerCycle = 10;
    [SerializeField] private float _interval = .5f;
    
    [SerializeField] private Vector2 _xPlane = new Vector2(-4f,4f);
    [SerializeField] private Vector2 _zPlane = new Vector2(-4f,4f);
    
    [SerializeField] private Vector2 _yzPlane  = new Vector2(-10,0);

    private float _currentFPS;
    private float _averageDeltaTime = 1f / 60f;

    private void Start()
    {

        if (!Directory.Exists(_filePath))
        {
            Directory.CreateDirectory(_filePath);
        }
        
        File.Create(_filePath + _fileName).Close();
        
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(SpawningCoroutine());
        } 
        else if (Input.GetKeyUp(KeyCode.M))
        {
            PrintData();
            SaveData();
        }
        _averageDeltaTime = Mathf.Lerp(_averageDeltaTime, Time.deltaTime, .05f);
        _currentFPS = 1f / _averageDeltaTime;
    }

    private void SaveData()
    {
        string[] data = new string[_dataList.Count];
        for (int i = 0; i < _dataList.Count; i++)
        {
            string temp = _dataList[i];
            string saveData = (i * _samplingRate) + _delimiter + temp + _delimiter;
            data[i] = saveData;
        }
        
        File.WriteAllLines(Path.Combine(_filePath, _fileName), data);
        print($"Saved to {_filePath}");
    }
    
    private void PrintData()
    {
        print(_objectsCount);
    }


    private IEnumerator SpawningCoroutine()
    {
        while (_isSpawningEnabled && _objectsCount < _dataSize)
        {
            for (int i = 0; i < _objectsPerCycle; i++)
            {
                if (_objectsCount % _samplingRate == 0)
                {
                    _dataList.Add(_currentFPS.ToString());
                }
            
                Vector3 position = new (
                    Random.Range(_xPlane.x, _xPlane.y),
                    Random.Range(_yzPlane.x, _yzPlane.y),
                    Random.Range(_zPlane.x, _zPlane.y)
                );
                Instantiate(_prefab, position, Quaternion.identity);
                _objectsCount++;
            }
            yield return new WaitForSeconds(_interval);
        }
        SaveData();
    }
}
