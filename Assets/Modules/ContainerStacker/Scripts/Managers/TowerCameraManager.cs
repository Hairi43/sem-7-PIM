using UnityEngine;

public class TowerCameraManager : MonoBehaviour
{
    public float moveDuration = 1f;
    public BoxCollider2D box;

    private Vector3 _targetPosition;
    private Vector3 _startPosition;
    private float _elapsed = 0f;
    private bool _isMoving = false;

    float GetContainerHeight(BoxCollider2D container)
    {    
        var rawHeight = container.size.y;
        var scaleY = container.transform.lossyScale.y;
        var height = rawHeight * scaleY;
        return height;
    }
    
    public void MoveUpByHeight()
    {
        if (_isMoving)
        {
            // Chain additional moves on top of the current target position
            _startPosition = transform.position;
            _targetPosition = _targetPosition + Vector3.up * GetContainerHeight(box);
        }
        else
        {
            _startPosition = transform.position;
            _targetPosition = _startPosition + Vector3.up * GetContainerHeight(box);
        }

        _elapsed = 0f;
        _isMoving = true;
    }

    void Update()
    {
        if (_isMoving)
        {
            _elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(_elapsed / moveDuration);
            transform.position = Vector3.Lerp(_startPosition, _targetPosition, t);

            if (t >= 1f)
            {
                _isMoving = false;
            }
        }
    }
}
