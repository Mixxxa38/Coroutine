using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class SpawnCubes : MonoBehaviour
{

    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private float _spawnInterval = 0.04f;
    [SerializeField] private float _changeColorDelay = 0.2f;
    [SerializeField] private float _recoloringTime = 0.2f;

    private int _rows = 20;
    private int _column = 20;
    private readonly List<GameObject> _listOfCubes = new();
    private WaitForSeconds _waitBeforeNewSpawn;

    private void Start()
    {
        _waitBeforeNewSpawn = new WaitForSeconds(_spawnInterval);
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
                var position = new Vector3(startX - x * stepX, startY - y * stepY, startZ);
                _listOfCubes.Add(Instantiate(_cubePrefab,position,Quaternion.identity));
                yield return _waitBeforeNewSpawn;
            }
        }
    }
    [UsedImplicitly]
    public void OnClickButtonChangeColors()
    {
        StartCoroutine(ChangeCubesColors(_recoloringTime));
    }
    
   private IEnumerator ChangeCubesColors(float recoloringTime)
   {
       var randomColor = Random.ColorHSV(0f, 1f, 0.8f, 1f, 1, 1);
       foreach (var cubes in _listOfCubes)
       {
           var cubeRenderer = cubes.GetComponent<Renderer>();
           StartCoroutine(ChangeCubeColor(cubeRenderer,recoloringTime, randomColor));
           yield return new WaitForSeconds(_changeColorDelay);
       }
   }

   private IEnumerator ChangeCubeColor(Renderer cubeRenderer, float recoloringTime, Color finalColor)
   {
       var startColor = cubeRenderer.material.color;
       var currentTime = 0f;

       while (currentTime < recoloringTime)
       {
           var currentColor = Color.Lerp(startColor, finalColor, currentTime / recoloringTime);
           cubeRenderer.material.color = currentColor;
           currentTime += Time.deltaTime;

           yield return null;
       }
       cubeRenderer.material.color = finalColor;
   } 
}
  

