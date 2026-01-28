using UnityEngine;
using UnityEngine.InputSystem;

namespace _Source
{
    public class InputListener : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        private InputSystem_Actions _inputSystemActions;
        private void Awake()
        {
            _inputSystemActions = new InputSystem_Actions();
        }

        private void OnEnable()
        {
            Bind();
            _inputSystemActions.Enable();
        }

        private void Bind()
        {
            _inputSystemActions.Player.Jump.performed += OnJump;
        }

        private void FixedUpdate()
        {
            Rotate();
        }

        private void Rotate()
        {
            Vector2 direction = _inputSystemActions.Player.Move.ReadValue<Vector2>();
            playerMovement.Rotate(direction); 
        }

        private void OnJump(InputAction.CallbackContext obj)
        {
            playerMovement.Jump();
        }

        private void Expose()
        {
            _inputSystemActions.Player.Jump.performed -= OnJump;
        }

        private void OnDisable()
        {
            _inputSystemActions.Disable();
            Expose();
        }
    }
}
