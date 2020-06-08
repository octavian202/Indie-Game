using System;
using UnityEngine;

namespace Player
{
    public class FirstPersonController : MonoBehaviour
    {
        #region Variabile

        private CharacterController controller;
        //private Animator animator;

        private int velocityX = Animator.StringToHash("velocityX");
        private int velocityY = Animator.StringToHash("velocityY");

        public float runSpeed = 20f;
        public float walkSpeed = 12f;
        private float currentSpeed = 0f;
    
        private float gravity = -9.81f;
        private Vector3 gravityVector = Vector3.zero;

        [HideInInspector] public Vector2 movementInput = Vector2.zero;
        public float velocitySmoothness = 0.075f;

        #endregion
        
        private void Start()
        {
            controller = GetComponent<CharacterController>();
            //animator = GameObject.Find("Joe").GetComponent<Animator>();
        }

        private void Update()
        {
            ChangeSpeed();

            var velocity = Vector2.Lerp(movementInput,
                new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), velocitySmoothness);

            movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            var desiredMoveDirection = currentSpeed * Time.deltaTime * (movementInput.x * transform.right + movementInput.y * transform.forward);

            /*animator.SetFloat(velocityX, velocity.x);
            animator.SetFloat(velocityY, velocity.y);*/
    
            controller.Move(desiredMoveDirection);
    
            GravityAction();
            
        }

        #region Private Methods

        private void GravityAction()
        {
            if (controller.isGrounded)
            {
                gravityVector = Vector3.zero;
                return;
            }
    
            gravityVector.y += gravity * Time.deltaTime * Time.deltaTime;
            controller.Move(gravityVector);
        }

        private void ChangeSpeed()
        {
            currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        }

        #endregion
        
    }
}
