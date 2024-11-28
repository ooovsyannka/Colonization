using TMPro;
using UnityEngine;

public class TowerCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _resurceCountText;
    [SerializeField] private Tower _tower;

    private int _resurceCount = 0;

    private void OnEnable()
    {
        _tower.ResourceReceived += ResourceCountUpdate;
    }

    private void OnDisable()
    {
        _tower.ResourceReceived -= ResourceCountUpdate;
    }

    public void ResourceCountUpdate(Resource _)
    {
        _resurceCount++;
        _resurceCountText.text = _resurceCount.ToString();
    }
}