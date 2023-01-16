using UnityEngine;

namespace Invector.vCharacterController
{
    public class vThirdPersonInput : MonoBehaviour
    {
        #region Variables       

        [Header("Controller Input")]
        public string horizontalInput = "Horizontal";
        public string verticallInput = "Vertical";
        public KeyCode jumpInput = KeyCode.Space;
        public KeyCode strafeInput = KeyCode.Tab;
        public KeyCode sprintInput = KeyCode.LeftShift;
        public KeyCode pauseInput = KeyCode.Escape;
        public KeyCode changeWeaponInput = KeyCode.Tab;
        public bool isAtackSword = true;
        public bool isBowShot = true;
        public bool canShoot = false;

        [Header("Camera Input")]
        public string rotateCameraXInput = "Mouse X";
        public string rotateCameraYInput = "Mouse Y";

        [Header("Audio")]
        public AudioSource run;
        private bool HActive; //Verificación si soltó o no la tecla
        private bool VActive; //Verificación si soltó o no la tecla

        [HideInInspector] public vThirdPersonController cc;
        [HideInInspector] public vThirdPersonCamera tpCamera;
        [HideInInspector] public Camera cameraMain;

        #endregion

        protected virtual void Start()
        {
            InitilizeController();
            InitializeTpCamera();
        }

        protected virtual void FixedUpdate()
        {
            cc.UpdateMotor();               // updates the ThirdPersonMotor methods
            cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
            cc.ControlRotationType();       // handle the controller rotation type
        }

        protected virtual void Update()
        {
            InputHandle();                  // update the input methods
            cc.UpdateAnimator();            // updates the Animator Parameters
        }

        public virtual void OnAnimatorMove()
        {
            cc.ControlAnimatorRootMotion(); // handle root motion animations 
        }

        #region Basic Locomotion Inputs

        protected virtual void InitilizeController()
        {
            cc = GetComponent<vThirdPersonController>();

            if (cc != null)
                cc.Init();
        }

        protected virtual void InitializeTpCamera()
        {
            if (tpCamera == null)
            {
                tpCamera = FindObjectOfType<vThirdPersonCamera>();
                if (tpCamera == null)
                    return;
                if (tpCamera)
                {
                    tpCamera.SetMainTarget(this.transform);
                    tpCamera.Init();
                }
            }
        }

        protected virtual void InputHandle()
        {
            MoveInput();
            CameraInput();
            SprintInput();
            StrafeInput();
            JumpInput();
            AtackSwordInput();
            PauseInput();
            ShoowBowInput();
            ChangeWeaponInput();
        }

        public virtual void MoveInput()
        {
            cc.input.x = Input.GetAxis(horizontalInput);
            cc.input.z = Input.GetAxis(verticallInput);
            if (Input.GetButtonDown("Horizontal"))
            { //Si oprime la tecla, reproduzca sonido
                HActive = true; //Valida que si esté presionada
                run.Play();
            }
            if (Input.GetButtonDown("Vertical"))
            { //Si oprime la tecla, reproduzca sonido
                VActive = true; //Valida que si esté presionada
                run.Play();
            }
            if (Input.GetButtonUp("Horizontal"))
            { //Si no oprime la tecla, pause sonido
                HActive = false; //Valida que no esté presionada
                if (VActive == false)
                {
                    run.Pause();
                }
            }
            if (Input.GetButtonUp("Vertical"))
            { //Si no oprime la tecla, pause sonido
                VActive = false; //Valida que no esté presionada
                if (HActive == false)
                {
                    run.Pause();
                }
            }
        }

        protected virtual void CameraInput()
        {
            if (!cameraMain)
            {
                if (!Camera.main) Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
                else
                {
                    cameraMain = Camera.main;
                    cc.rotateTarget = cameraMain.transform;
                }
            }

            if (cameraMain)
            {
                cc.UpdateMoveDirection(cameraMain.transform);
            }

            if (tpCamera == null)
                return;

            var Y = Input.GetAxis(rotateCameraYInput);
            var X = Input.GetAxis(rotateCameraXInput);

            tpCamera.RotateCamera(X, Y);
        }

        protected virtual void StrafeInput()
        {
           /* if (Input.GetKeyDown(strafeInput))
                cc.Strafe();
        */
            }

        protected virtual void SprintInput()
        {
            if (Input.GetKeyDown(sprintInput))
                cc.Sprint(true);
            else if (Input.GetKeyUp(sprintInput))
                cc.Sprint(false);
        }

        /// <summary>
        /// Conditions to trigger the Jump animation & behavior
        /// </summary>
        /// <returns></returns>
        protected virtual bool JumpConditions()
        {
            return cc.isGrounded && cc.GroundAngle() < cc.slopeLimit && !cc.isJumping && !cc.stopMove;
        }

        /// <summary>
        /// Input to trigger the Jump 
        /// </summary>
        protected virtual void JumpInput()
        {
            if (Input.GetKeyDown(jumpInput) && JumpConditions())
                cc.Jump();
        }


        protected virtual void AtackSwordInput()
        {
            if (Input.GetMouseButton(0) && isAtackSword)
            {
                isAtackSword = false;
                cc.AtackSword();
            }
            else
            {
                isAtackSword = true;
            }
        }

        protected virtual void ShoowBowInput()
        {
            if (Input.GetMouseButton(0) && isBowShot)
            {
                isBowShot = false;
                cc.ShootBow();
            }
            else
            {
                isBowShot = true;
            }
        }

        protected virtual void ChangeWeaponInput()
        {
            if (Input.GetKeyDown(changeWeaponInput) )
            {
                cc.ChangeWeapon();
            }
        }

        protected virtual void PauseInput()
        {
            if(Input.GetKeyDown(pauseInput))
                 cc.Pause();

        }





        #endregion       
    }
}