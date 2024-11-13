using TMPro;
using UnityEngine;

public class TowerCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _resurceCountText;

    private int _resurceCount = 0;

    public void CountUpdate()
    {
        _resurceCount++;
        _resurceCountText.text = _resurceCount.ToString();
    }
}