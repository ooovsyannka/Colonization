using TMPro;
using UnityEngine;

public class TowerCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _resurceCountText;
    [SerializeField] private TowerCollision _towerCollision;

    private void OnEnable()
    {
        _towerCollision.ResurceDetected += ResourceCountUpdate;
    }

    private void OnDisable()
    {
        _towerCollision.ResurceDetected -= ResourceCountUpdate;
    }

    private int _resurceCount = 0;
 
    public void ResourceCountUpdate(Resource resource)
    {
        _resurceCount++;
        _resurceCountText.text = _resurceCount.ToString();
    }
}