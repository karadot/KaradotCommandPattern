using System.Collections;
using CommandPattern.Commands;
using UnityEngine;

namespace CommandPattern
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Unit unitA, unitB;

        private bool _isPlayerOneTurn = true;

        private bool _waitingForFinishMovement = false;

        private Vector3? _movement;

        private void Update()
        {
            if (_waitingForFinishMovement) return;
            if (Input.GetKeyDown(KeyCode.W))
            {
                _movement = Vector3.forward;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                _movement = Vector3.back;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _movement = Vector3.right;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                _movement = Vector3.left;
            }

            if (!_movement.HasValue) return;
            ActiveUnit().StartMove(_movement.Value);
            StartCoroutine(WaitUntilUnitAvailable(ActiveUnit()));
            _isPlayerOneTurn = !_isPlayerOneTurn;
            _movement = null;
        }

        IEnumerator WaitUntilUnitAvailable(Unit unit)
        {
            _waitingForFinishMovement = true;
            yield return new WaitUntil(() => unit.CanMove);
            _waitingForFinishMovement = false;
        }

        Unit ActiveUnit()
        {
            return _isPlayerOneTurn ? unitA : unitB;
        }
    }
}