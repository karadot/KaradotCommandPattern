using System.Collections;
using CommandPattern.Commands;
using UnityEngine;

namespace CommandPattern
{
    public class PlayerInput : MonoBehaviour
    {
        private CommandRecorder _recorder;
        [SerializeField] private Unit unitA, unitB;

        private bool _isPlayerOneTurn = true;

        private bool _waitingForFinishCommand = false;

        private Vector3? activeInput;

        private void Start()
        {
            _recorder = new CommandRecorder();
        }

        private void Update()
        {
            if (_waitingForFinishCommand) return;
            if (Input.GetKeyDown(KeyCode.W))
            {
                activeInput = Vector3.forward;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                activeInput = Vector3.back;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                activeInput = Vector3.right;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                activeInput = Vector3.left;
            }

            if (!activeInput.HasValue) return;
            ActiveUnit().StartMove(activeInput.Value);
            StartCoroutine(WaitUntilUnitAvailable(ActiveUnit()));
            _isPlayerOneTurn = !_isPlayerOneTurn;
            activeInput = null;
        }

        IEnumerator WaitUntilUnitAvailable(Unit unit)
        {
            _waitingForFinishCommand = true;
            yield return new WaitUntil(() => unit.CanExecuteCommand);
            _waitingForFinishCommand = false;
            _waitingForFinishCommand = false;
        }

        Unit ActiveUnit()
        {
            return _isPlayerOneTurn ? unitA : unitB;
        }

        #region Recording

        IEnumerator ExecuteFromList()
        {
            var last = _recorder.ExecuteCommandOnList();
            while (last != null)
            {
                var last1 = last;
                yield return new WaitUntil(() => last1.CanExecute);
                yield return new WaitForSeconds(.05f);
                last = _recorder.ExecuteCommandOnList();
            }
        }

        IEnumerator FullUndo()
        {
            var last = _recorder.LastMovementCommand;
            while (last != null)
            {
                _recorder.Undo();
                var last1 = last;
                yield return new WaitUntil(() => last1.CanExecute);
                yield return new WaitForSeconds(.05f);
                last = _recorder.LastMovementCommand;
            }
        }

        #endregion
    }
}