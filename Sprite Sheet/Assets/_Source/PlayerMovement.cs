using UnityEngine;
using UnityEngine.UI;

namespace _Source
{
    public class PlayerMovement : MonoBehaviour
    {
        private static readonly int Jump1 = Animator.StringToHash("JumpTrigger");

        [Header("Jump")] 
        [SerializeField] private float jumpForce;
        [SerializeField] private float horizontalForce;
        [SerializeField] private Slider slider;
        [Header("Animation")] 
        [SerializeField] private SpriteRenderer sr;
        [SerializeField] private Animator animator;

        [Header("Ground check")] 
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private LayerMask groundCheckMask;

        private Rigidbody2D _rb;
        private bool _isGrounded;
        private bool _isFacingRight;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            CheckGround();
            horizontalForce = slider.value;
        }

        private void CheckGround()
        {
            _isGrounded = Physics2D.OverlapCircle(
                groundCheck.position,
                groundCheckRadius,
                groundCheckMask);
        }

        public void Rotate(Vector2 direction)
        {
            if (direction.x == 0) return;
            bool wantToFaceRight = direction.x > 0;
            if (_isFacingRight == wantToFaceRight) return;

            _isFacingRight = wantToFaceRight;
            sr.flipX = !_isFacingRight;
        }

        public void Jump()
        {
            if (!_isGrounded) return;

            animator.SetTrigger(Jump1);

            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);

            float horizontalDirection = _isFacingRight ? horizontalForce : -horizontalForce;
            _rb.AddForce(new Vector2(horizontalDirection, 0), ForceMode2D.Impulse);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
        }
#endif
    }
}