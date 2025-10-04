using UnityEngine;

namespace Assets.Code.GamePlay.HUD
{

public class UIBillboard : MonoBehaviour
{
    [SerializeField] private Camera _cachedCamera;
    [SerializeField] private bool _invertDirection = true;
    
    
    private void Start()
    {
        if (!_cachedCamera)
        {
            _cachedCamera = Camera.main;
        }
    }
    
    private void Update()
    {
        
        Vector3 directionToCamera = _cachedCamera.transform.position - transform.position;
        
        if (_invertDirection)
        {
            directionToCamera = -directionToCamera;
        }
        
        transform.rotation = Quaternion.LookRotation(directionToCamera);
    }
}
}