using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    [SerializeField] private Transform _endGoal;
   [SerializeField] private Transform _platform;
    [SerializeField] private Transform _connector;
    [SerializeField] private GameObject _targetEffect;
    [SerializeField] private GameObject _targetExplosionEffect;
    [SerializeField] private GameObject _flaresOnWin;

    private float _scaleMultiplayer = 100f;

    private void Reset()
    {
        ResolveReferences();
    }
    private void Awake()
    {
        ResolveReferences();
        ManageEffectOnPlayerReached();
    }
    private void ResolveReferences()
    {
        if (!_endGoal)
            _endGoal = transform.parent;

        if (!_platform)
            _platform = _endGoal.Find("Platform");

        if (!_connector)
            _connector = _endGoal.Find("PlatformConnector");

        if (!_targetEffect)
            _targetEffect = _endGoal.Find("WinEffects/TargetEffect").gameObject;
        
        if (!_targetExplosionEffect)
            _targetExplosionEffect = _endGoal.Find("WinEffects/TargetExplosionEffect").gameObject;

        if (!_flaresOnWin)
            _flaresOnWin = _endGoal.Find("WinEffects/FlaresOnWin").gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ManageEffectOnPlayerReached(true);
            GameManager.OnPlayerReachedAtTargetPoint?.Invoke(other.gameObject,transform);
        }
            
    }

    private void ManageEffectOnPlayerReached(bool isPlayerReached = false)
    {
        _targetExplosionEffect.SetActive(isPlayerReached);
        _flaresOnWin.SetActive(isPlayerReached);
        _targetEffect.SetActive(!isPlayerReached);
    }

    public void SetEndGoalPosition(Transform lastStair)
    {
        var lastStairPos = lastStair.position;
        var lastStairScale = lastStair.localScale / 2;
        var platformScale = (_platform.localScale / 2) * _scaleMultiplayer;

        var connecterScaleX = _connector.localScale.x * _scaleMultiplayer;
        var posX = lastStairPos.x + lastStairScale.x + platformScale.x + connecterScaleX - .1f;

        var posY = lastStairPos.y - platformScale.y + lastStairScale.y;

        _endGoal.position = new Vector3(posX, posY, _endGoal.transform.position.z);
    }


    //public StairsSpawner spawner;

    //[ContextMenu("TestEndGoalPosition")]
    //private void TestEndGoal()
    //{
    //    SetEndGoalPosition(spawner.BlueStairs[spawner.BlueStairs.Count - 1].transform);
    //}
    
}
