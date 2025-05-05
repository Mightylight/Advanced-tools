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
    [Header("Data Collection")]
    [SerializeField] private string _fileName = "/physics_test_unity.txt";
    
    [SerializeField] private int _dataSize = 5000;
    [Tooltip("The amount of objects after a snapshot is stored in the data array")]
    [SerializeField] private int _samplingInterval = 50;
    
    private string _filePath = Application.streamingAssetsPath;
    private string _seperator = ",";
    private List<string> _dataList = new ();
    private int _objectsCount;
    
    [Space(10)]
    
    [Header("Spawning")]
    [SerializeField] private bool _useContinuousSpawning = true;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private bool _isSpawningEnabled;
    [SerializeField] private float _interval = .5f;
    
    [Space(10)]
    
    [Header("Spawn Area")]
    [SerializeField] private Vector2 _xPlane = new Vector2(-4f,4f);
    [SerializeField] private Vector2 _zPlane = new Vector2(-4f,4f);
    
    [SerializeField] private Vector2 _yzPlane  = new Vector2(-10,0);
    
    private int _objectsPerCycle;

    private List<GameObject> _objects = new();
    
    private float _currentFPS;
    private float _averageDeltaTime = 1f / 60f;
    
    public float CurrentFPS => _currentFPS;
    public int ObjectsCount => _objectsCount;

    private void Start()
    {
        _objectsPerCycle = _samplingInterval;
        if (!Directory.Exists(_filePath))
        {
            Directory.CreateDirectory(_filePath);
        }
        
        //File.Create(_filePath + _fileName).Close();
    }


    private void Update()
    {
        if (_useContinuousSpawning)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                StartCoroutine(ContinuousSpawningCoroutine());
            } 
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                SpawnCycle();
            }
            
            if(Input.GetKeyUp(KeyCode.Minus)) 
            {
                DestroyCycle();
            }
        }
        
        if (Input.GetKeyUp(KeyCode.M))
        {
            print($"Object count {_objectsCount}");
            SaveDataCSV(_dataList);
        }

        UpdateFPS();
    }

    private void UpdateFPS()
    {
        _averageDeltaTime = Mathf.Lerp(_averageDeltaTime, Time.deltaTime, .05f);
        _currentFPS = 1f / _averageDeltaTime;
    }
    
    private void SaveDataCSV(List<string> pList)
    {
        List<string> csvLines = new List<string> { "Index,Value" }; // Header

        for (int i = 0; i < pList.Count; i++)
        {
            csvLines.Add($"{i},{pList[i]}");
        }

        File.WriteAllLines(Path.Combine(_filePath,_fileName), csvLines);
        Debug.Log($"CSV saved to: {_filePath}");
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void SpawnCycle()
    {
        if (!_isSpawningEnabled || _objectsCount >= _dataSize) return;
        for (int i = 0; i < _objectsPerCycle; i++)
        {
            if (_objectsCount % _samplingInterval == 0)
            {
                _dataList.Add(_currentFPS.ToString());
            }
            
            Vector3 position = new (
                Random.Range(_xPlane.x, _xPlane.y),
                Random.Range(_yzPlane.x, _yzPlane.y),
                Random.Range(_zPlane.x, _zPlane.y)
            );
            GameObject spawnedObject = Instantiate(_prefab, position, Quaternion.identity);
            _objects.Add(spawnedObject);
            _objectsCount++;

            if (_objectsCount < _dataSize + 1) continue;
            SaveDataCSV(_dataList);
            _isSpawningEnabled = false;
            break;
        }
    }

    private void DestroyCycle()
    {
        if (_objectsCount <= 0) return;
        for (int i = _objectsCount; i > _objectsCount - _objectsPerCycle; i--)
        {
            _objects[i].SetActive(false);
            _objects.RemoveAt(i);
            _objectsCount--;
        }
    }

    private IEnumerator ContinuousSpawningCoroutine()
    {
        while (_isSpawningEnabled && _objectsCount < _dataSize)
        {
            SpawnCycle();
            yield return new WaitForSeconds(_interval);
        }
    }
}
