using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubes : MonoBehaviour
{

    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private float _spawnInterval = 0.04f;
    [SerializeField] private float _changeColorDelay = 0.2f;

    private int _rows = 20;
    private int _column = 20;
    private List<GameObject> _listOfCubes = new List<GameObject>();

    private void Awake()
    {
        StartCoroutine(CubesSpawn());
    }

    private IEnumerator CubesSpawn()
    {
        var startX = 640;
        var stepX = 5;
        var startY = 205;
        var stepY = 5;
        var startZ = 90;

        for (int y = 0; y < _rows; y++)
        {
            for (int x = 0; x < _column; x++)
            {
                var spawn = Instantiate(_cubePrefab);
                _listOfCubes.Add(spawn);
                var position = new Vector3(startX - x * stepX, startY - y * stepY, startZ);
                spawn.transform.position = position;
                yield return new WaitForSeconds(_spawnInterval);
            }
        }
    }

    public void OnClickButton()
    {
        StartCoroutine(ChangeColor());
    }

    private IEnumerator ChangeColor()
    {
        var randomColor = Random.ColorHSV(0f, 1f, 0.8f, 1f, 1, 1);
        foreach (var cubes in _listOfCubes)
        {
            cubes.GetComponent<Renderer>().material.color = randomColor;
            yield return new WaitForSeconds(_changeColorDelay);
        }
    }
}
  

