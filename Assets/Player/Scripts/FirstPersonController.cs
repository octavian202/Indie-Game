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
        private float currentSpeed;
    
        private float gravity = -9.81f;
        private Vector3 gravityVector = Vector3.zero;

        private float _velocitySmoothness = 0.45f;
        private Vector2 velocity = Vector2.zero;

        #endregion

        #region Setup

        private void Start()
        {
            currentSpeed = walkSpeed;
            controller = GetComponent<CharacterController>();
            //animator = GameObject.Find("Joe").GetComponent<Animator>();
        }

        #endregion
        

        private void Update()
        {
            ChangeSpeed();
            
            Move();
    
            GravityAction();
            
            //ModelAnimation();
            
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
            currentSpeed = Input.GetKey(KeyCode.LeftShift) ? Mathf.Lerp(currentSpeed, runSpeed, _velocitySmoothness) : InterpolateFromMaxToMin(currentSpeed, walkSpeed, _velocitySmoothness);
        }

        private void Move()
        {
            var _movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            velocity = Vector2.Lerp(velocity, _movementInput, _velocitySmoothness);

            var _desiredMoveDirection = currentSpeed * Time.deltaTime * (velocity.x * transform.right + velocity.y * transform.forward);
            controller.Move(_desiredMoveDirection);
        }

        /*
        private void ModelAnimation()
        {
            animator.SetFloat(velocityX, velocity.x);
            animator.SetFloat(velocityY, velocity.y);
        }*/

        #endregion

        #region Help Methods

        private float InterpolateFromMaxToMin(float a, float b, float t)
        {
            var add = (b - a) * t;
            var tmp = a + add;

            return tmp < b ? b : tmp;
        }

        #endregion
        
    }
}
