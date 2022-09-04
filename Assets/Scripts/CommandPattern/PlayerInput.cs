using System;
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

        private CommandRecorder _recorder;

        private void Awake()
        {
            _recorder = new CommandRecorder();
        }

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

            if (Input.GetKeyDown(KeyCode.Z))
            {
                StartCoroutine(_recorder.FullUndo());
            }

            if (!_movement.HasValue) return;
            SimpleMovementCommand movementCommand = new SimpleMovementCommand(ActiveUnit(),_movement.Value);
            _recorder.ExecuteCommand(movementCommand);
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