using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerNameTxt;
    [SerializeField] private TextMeshProUGUI _remainingStaitsTxt;
    [SerializeField] private TextMeshProUGUI _maxCapacity;

    public string PlayerNameTxt{ get => _playerNameTxt.text; set => _playerNameTxt.text = value; }
    public string RemainingStaitsTxt { get => _remainingStaitsTxt.text; set => _remainingStaitsTxt.text = value; }
    public string MaxCapacity { get => _maxCapacity.text; set => _maxCapacity.text = value; }

    private void Reset()
    {
        ResolveReferences();
    }
    private void Awake()
    {
        ResolveReferences();
    }
    private void ResolveReferences()
    {
        if (!_playerNameTxt)
            _playerNameTxt = transform.Find("PlayerName").GetComponent<TextMeshProUGUI>();
        
        if (!_remainingStaitsTxt)
            _remainingStaitsTxt = transform.Find("RemainingStairsDisplay").GetComponent<TextMeshProUGUI>();
        
        if (!_maxCapacity)
            _maxCapacity = transform.Find("CapacityPointsDisplay").GetComponent<TextMeshProUGUI>();
    }

}
