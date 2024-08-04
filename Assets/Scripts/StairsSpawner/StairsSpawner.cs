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

    public List<StairEntity> BlueStairs { get => _blueStairs;}

    private void Reset()
    {
        ResolveReferences();
    }
    public void SpawmBothStairs()
    {
        ResolveReferences(() =>
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
            //x += 1.350f;
            //y += 0.450f;

            x += 1.33f;
            y += 0.42f;

            var pos = new Vector3(x, y, spawnPoint.position.z);
            GameObject stairInstance = Instantiate(stairPrefab,pos,spawnPoint.transform.rotation,holder);
            var rend = stairInstance.GetComponentInChildren<MeshRenderer>();
            rend.enabled = false;
            var stairEntity = stairInstance.GetComponentInChildren<StairEntity>();
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

        if (playerStats.PlayerType == PlayerType.Bot)
        {
            if(_bluePlayerBlocker.gameObject.activeInHierarchy)
                _bluePlayerBlocker.gameObject.SetActive(false);

            return;
        }


        if (blocker != null)
        {
            if (!blocker.gameObject.activeInHierarchy)
                blocker.gameObject.SetActive(true);
            var lastStairIndex = stairs.Count - 1;
            var currentStairIndex = (int)MathF.Min(lastStairIndex, playerStats.TotalPointsCollected);

            if(currentStairIndex == lastStairIndex)
            {
                blocker.gameObject.SetActive(false);
                return;
            }
            var lastStair_asPer_totalCollectedFruits = stairs[currentStairIndex];
            var offset = new Vector3(.38f, .7f, 0);
            var blockerPos = lastStair_asPer_totalCollectedFruits.transform.position + offset;
            blocker.transform.position = blockerPos;
        }
    }

    

}
