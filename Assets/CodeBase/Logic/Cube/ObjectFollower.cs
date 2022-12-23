using UnityEngine;

namespace CodeBase.Logic.Cube
{
    public class ObjectFollower : MonoBehaviour
    {
        [SerializeField]
        private float _followSpeed;
    
        [SerializeField]
        private Vector3 _offset;
    
        private Transform _target;

        private bool _targetSet;

        public void SetTarget(Transform target)
        {
            _target = target;
            _targetSet = true;
        }

        private void Update()
        {
            if (_targetSet == false)
                return;
        
            Vector2 lerpedPosition = Vector2.Lerp(transform.position, _target.position + _offset, Time.deltaTime * _followSpeed);
            transform.position = new Vector3(lerpedPosition.x, lerpedPosition.y, _offset.z);
        }
    }
}
