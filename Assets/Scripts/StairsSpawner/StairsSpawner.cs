using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _greenStairsPrefab;
    [SerializeField] private GameObject _blueStairsPrefab;

    [Space(10)]
    [SerializeField] private Transform _greenStairsSpawnPoint;
    [SerializeField] private Transform _blueStairsSpawnPoint;

    private Transform _greenStairsHolder, _blueStairsHolder;

    [Space(10)]
    [SerializeField] private List<StairEntity> _greenStairs;
    [SerializeField] private List<StairEntity> _blueStairs;

    [Space(10)]
    [SerializeField] private Transform _greenPlayerBlocker;
    [SerializeField] private Transform _bluePlayerBlocker;

    private void Reset()
    {
        ResolveReferences();
    }
    private void Awake()
    {
        ResolveReferences(()=>
        {
            _greenStairs = SpawnStairs(_greenStairsPrefab);

            _blueStairs = SpawnStairs(_blueStairsPrefab);
        });
    }

    private void ResolveReferences(Action OnReferencesResolved = null)
    {
        if (!_greenStairsHolder)
        {
            var holder = transform.Find("GreenStairsHolder");
            if (!holder)
            {
                _greenStairsHolder = new GameObject("GreenStairsHolder").transform;
                _greenStairsHolder.parent = transform;
            }
            else
                _greenStairsHolder = holder;
        }

        if (!_blueStairsHolder)
        {
            var holder = transform.Find("BlueStairsHolder");
            if (!holder)
            {
                _blueStairsHolder = new GameObject("BlueStairsHolder").transform;
                _blueStairsHolder.parent = transform;
            }
            else
                _blueStairsHolder = holder;
        }

        if (!_greenPlayerBlocker)
            _greenPlayerBlocker = transform.Find("GreenPlayerBlocker");

        if (!_bluePlayerBlocker)
            _bluePlayerBlocker = transform.Find("BluePlayerBlocker");


        OnReferencesResolved?.Invoke();
    }

    private List<StairEntity> SpawnStairs(GameObject stairPrefab)
    {
        Transform spawnPoint = (stairPrefab == _greenStairsPrefab) ? _greenStairsSpawnPoint : _blueStairsSpawnPoint;

        Transform holder = (stairPrefab == _greenStairsPrefab) ? _greenStairsHolder : _blueStairsHolder;


        List<StairEntity> stairs = new List<StairEntity>();

        var x = spawnPoint.position.x;
        var y = spawnPoint.position.y;

        for (int i = 0; i < MatchSettings.MatchLenght; i++)
        {
            x += 1.350f;
            y += 0.450f;

            var pos = new Vector3(x, y, spawnPoint.position.z);
            GameObject stairInstance = Instantiate(stairPrefab,pos,spawnPoint.transform.rotation,holder);
            var rend = stairInstance.GetComponent<MeshRenderer>();
            rend.enabled = false;
            var stairEntity = stairInstance.GetComponent<StairEntity>();
            stairEntity.rendrer = rend;
            stairs.Add(stairEntity);

        }
        return stairs;
    }

    public void UpdateBlockerPosition(PlayerStats playerStats)
    {
        Transform blocker = null;
        List<StairEntity> stairs = new List<StairEntity>();

        if (playerStats.PlayerType == PlayerType.Player1)
        {
            blocker = _greenPlayerBlocker;
            stairs = _greenStairs;
        }

        if (playerStats.PlayerType == PlayerType.Player2)
        {
            blocker = _bluePlayerBlocker;
            stairs = _blueStairs;
        }
            

        if(blocker != null)
        {
            if(stairs.Count > playerStats.TotalPointsCollected)
            {
                var lastStair_asPer_totalCollectedFruits = stairs[playerStats.TotalPointsCollected];

                var blockerPos = new Vector3(lastStair_asPer_totalCollectedFruits.transform.position.x + 0.38f,        lastStair_asPer_totalCollectedFruits.transform.position.y + 0.7f, lastStair_asPer_totalCollectedFruits.transform.position.z);

                blocker.transform.position = blockerPos;
            }
           
        }
    }

    

}
