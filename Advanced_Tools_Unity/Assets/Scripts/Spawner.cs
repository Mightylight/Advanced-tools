using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    //Data Saving variables
    [SerializeField] private string _fileName = "physics_test.csv";
    [SerializeField] private string _filePath = "Assets/Resources/";
    [SerializeField] private string _delimiter = ",";
    
    
    //Spawning variables
    [SerializeField] private GameObject _prefab;
    [SerializeField] private bool _isSpawningEnabled;
    [SerializeField] private int _objectsPerCycle = 10;
    [SerializeField] private float _interval = .5f;
    
    private List<GameObject> _objects = new();
    
    [SerializeField] private Vector2 _xPlane = new Vector2(-4f,4f);
    [SerializeField] private Vector2 _zPlane = new Vector2(-4f,4f);
    
    [SerializeField] private Vector2 _yzPlane  = new Vector2(-10,0);
    
    
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(SpawningCoroutine());
        } 
        else if (Input.GetKeyUp(KeyCode.M))
        {
            PrintData();
        }
    }
    
    private void PrintData()
    {
        print(_objects.Count);
    }


    private IEnumerator SpawningCoroutine()
    {
        while (_isSpawningEnabled)
        {
            for (int i = 0; i < _objectsPerCycle; i++)
            {
                if (_objects.Count % 50 == 0)
                {
                    //store data
                    
                }
            
                Vector3 position = new (
                    Random.Range(_xPlane.x, _xPlane.y),
                    Random.Range(_yzPlane.x, _yzPlane.y),
                    Random.Range(_zPlane.x, _zPlane.y)
                );
                GameObject obj = Instantiate(_prefab, position, Quaternion.identity);
                _objects.Add(obj);
            }
            yield return new WaitForSeconds(_interval);
        }
    }
}
