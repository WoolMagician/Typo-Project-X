﻿using System;
using Typo.Utilities.Cameras.Components;
using UnityEngine;

namespace Typo.Utilities.Cameras
{
    /// <summary>
    /// A basic camera controller.
    /// </summary>
    [Serializable]
    public class BasicCameraController : CameraController
    {
        /// <summary>
        /// The distance that the camera wants to position itself at from the target.
        /// </summary>
        [Header("Settings")]
        [Tooltip("The distance that the camera wants to position itself at from the target.")]
        public float DesiredDistance = 20f;

        /// <summary>
        /// The minimum that zooming will let you zoom in to.
        /// </summary>
        [Tooltip("The minimum that zooming will let you zoom in to.")]
        public float MinZoomDistance = 0f;

        /// <summary>
        /// The maximum that zooming will let you zoom out to.
        /// </summary>
        [Tooltip("The maximum that zooming will let you zoom out to.")]
        public float MaxZoomDistance = 50f;

        /// <summary>
        /// When the CameraController starts, horizontal rotation will be set to this value.
        /// </summary>
        [Tooltip("When the CameraController starts, horizontal rotation will be set to this value.")]
        public float InitialHorizontalRotation = 0f;

        /// <summary>
        /// When the CameraController starts, vertical rotation will be set to this value.
        /// </summary>
        [Tooltip("When the CameraController starts, vertical rotation will be set to this value.")]
        public float InitialVerticalRotation = 35f;

        #region Components
        /// <summary>
        /// TargetComponent
        /// </summary>
        [Header("Components")]
        public TargetComponent Target;
        /// <summary>
        /// RotationComponent
        /// </summary>
        public RotationComponent Rotation;
        /// <summary>
        /// ZoomComponent
        /// </summary>
        public ZoomComponent Zoom;
        /// <summary>
        /// ViewCollisionComponent
        /// </summary>
        public ViewCollisionComponent ViewCollision;
        /// <summary>
        /// InputComponent
        /// </summary>
        public InputComponent Input;
        /// <summary>
        /// EasyUnityInputComponent
        /// </summary>
        public EasyUnityInputComponent EasyUnityInput;
        /// <summary>
        /// CursorComponent
        /// </summary>
        public CursorComponent Cursor;
        ///// <summary>
        ///// HeadbobComponent
        ///// </summary>
        //public HeadbobComponent Headbob;
        /// <summary>
        /// ScreenShakeComponent
        /// </summary>
        public ScreenShakeComponent ScreenShake;

        public FloatEventChannelSO VerticalRotationChannelSO;
        public FloatEventChannelSO HorizontalRotationChannelSO;
        public Vector2EventChannel AnimationChannelSO;

        #endregion

        [System.Serializable]
        public class WindowComponent : Typo.Utilities.Cameras.Components.CameraComponent
        {
            public bool enabled = false;
            public bool showDebug = false;

            [Tooltip("Top curb bound value")]
            public float Top = 200;

            [Tooltip("Top curb bound value")]
            public float Bottom = 10;

            [Tooltip("Top curb bound value")]
            public float Left = 20;

            [Tooltip("Top curb bound value")]
            public float Right = 20;

            [Tooltip("Smoothing factor for camera movements")]
            public float curbSmoothing = 0.1f;

            public bool enablePlatformSnapping = false;

            public float platformSnappingSmoothing = 0.1f;

        }


        [System.Serializable]
        public class SplineFollowComponent : Typo.Utilities.Cameras.Components.CameraComponent
        {
            private CameraSpline _targetSpline;

            public CameraSpline targetSpline
            {
                get
                {
                    return  _targetSpline;
                }
                set
                {
                    _targetSpline = value;
                }
            }

            public bool enabled = false;

            public bool followSpline
            {
                get
                {
                    return enabled && (followOnXAxis || followOnYAxis || followOnZAxis);
                }
            }

            public bool followOnXAxis = false;
            public bool followOnYAxis = false;
            public bool followOnZAxis = false;
        }

        /// <summary>
        /// The previous distance the camera was at during the last update.
        /// </summary>
        private float _previousDistance;
        public Vector3 GetCameraZoomDistance()
        {
            float desired = DesiredDistance; // Where we want the camera to be
            float calculated = ViewCollision.CalculateMaximumDistanceFromTarget(Target.GetTarget(), Mathf.Max(desired, _previousDistance)); // The maximum distance we calculated we can be based off collision and preference
            float zoom = Zoom.CalculateDistanceFromTarget(_previousDistance, calculated, desired); // Where we want to be for the sake of zooming

            return CameraTransform.Forward * zoom;

        }
        public Vector3 GetCameraUpdatedPosition()
        {
            Vector3 target = Target.GetTarget();
            float desired = DesiredDistance; // Where we want the camera to be
            float calculated = ViewCollision.CalculateMaximumDistanceFromTarget(target, Mathf.Max(desired, _previousDistance)); // The maximum distance we calculated we can be based off collision and preference
            float zoom = Zoom.CalculateDistanceFromTarget(_previousDistance, calculated, desired); // Where we want to be for the sake of zooming
            Vector3 zoomDistance = CameraTransform.Forward * zoom;

            return target - zoomDistance;

        }

        protected override void AddCameraComponents()
        {
            AddCameraComponent(Rotation);
            AddCameraComponent(Zoom);
            AddCameraComponent(Target);
            AddCameraComponent(ViewCollision);
            AddCameraComponent(Input);
            AddCameraComponent(cameraWindow);
            AddCameraComponent(splineFollow);
            AddCameraComponent(EasyUnityInput);
            AddCameraComponent(Cursor);
            //AddCameraComponent(Headbob);
            AddCameraComponent(ScreenShake);
        }

        public Vector3 iniRot = Vector3.zero;
        public bool canUpdateCameraPosition = true;
        public bool smoothCameraPosition = false;
        public float actualSplinePoint = 0f;
        public float smoothFactor = 0.1f;

        public SplineFollowComponent splineFollow = new SplineFollowComponent();

        public WindowComponent cameraWindow = new WindowComponent();

        protected override void Awake()
        {
            base.Awake();

            iniRot = transform.eulerAngles;

            // Set initial rotation and distance
            Rotation.Rotate(InitialHorizontalRotation, InitialVerticalRotation);
            _previousDistance = DesiredDistance;

            // Then let update handle everything
            UpdateCamera();
            CameraTransform.ApplyTo(Camera);

            //VerticalRotationChannelSO.OnEventRaised += HandleVerticalRotationChannelSO;
            //HorizontalRotationChannelSO.OnEventRaised += HandleHorizontalRotationChannelSO;
            AnimationChannelSO.OnEventRaised += HandleAnimationChannelSO;

        }

        //private void HandleVerticalRotationChannelSO(float value)
        //{

        //}

        //private void HandleHorizontalRotationChannelSO(float value)
        //{

        //}

        

        private void HandleAnimationChannelSO(Vector2 value)
        {
            //if (value.x == 0)
            //{
            //    Rotation.AutoRotateTo(0, Rotation.VerticalRotation, 0.25f);
            //}
            //else if (value.x == 1)
            //{
            //    Rotation.AutoRotateTo(12, Rotation.VerticalRotation, 0.25f);
            //}
            //else if (value.x == -1)
            //{
            //    Rotation.AutoRotateTo(-12, Rotation.VerticalRotation, 0.25f);
            //}

            Rotation.AutoRotateTo(value.x * 12, (value.y == 0 ? 12 : (value.y == -1 ? value.y * 2 : value.y * 22)), 0.25f);

            //if (value.y == 0)
            //{
            //    Rotation.AutoRotateTo(Rotation.HorizontalRotation, 0, 0.25f);
            //}
            //else if (value.y == 1)
            //{
            //    Rotation.AutoRotateTo(Rotation.HorizontalRotation, 6, 0.25f);
            //}
            //else if (value.y == -1)
            //{
            //    Rotation.AutoRotateTo(Rotation.HorizontalRotation, -6, 0.25f);
            //}
        }

        void Update()
        {
            UpdateCamera();
            CameraTransform.ApplyTo(Camera);
        }

        RaycastHit[] hits;
        BoxCollider camInnerBoundsCollider;
        Vector2 targetScreenPos;
        float deltaX;
        float deltaY;

        public override void UpdateCamera()
        {
            // Get Input
            EasyUnityInput.AppendInput();
            InputValues input = Input.ProcessedInput;
            Input.ClearInput();

            Rotation.UpdateAutoRotate(true, Target.Target);
            Rotation.UpdateSmartFollow();

            //if (input.Horizontal.HasValue)
            //{
            //    Rotation.RotateHorizontally(input.Horizontal.Value);
            //}
            //if (input.Vertical.HasValue)
            //{
            //    Rotation.RotateVertically(input.Vertical.Value);
            //}

            Rotation.CheckRotationDegreesEvents();

            // Apply target offset modifications
            //Vector3 headbobOffset = Headbob.GetHeadbobModifier(_previousDistance);
            //Target.AddWorldSpaceOffset(headbobOffset);
            Vector3 screenShakeOffset = ScreenShake.GetShaking();
            Target.AddWorldSpaceOffset(screenShakeOffset);

            Vector3 target = Target.GetTarget();

            // Handle Cursor
            Cursor.SetCursorLock();

            // Hanlde Zooming
            if (input.ZoomIn.HasValue)
            {
                DesiredDistance = Mathf.Max(DesiredDistance + input.ZoomIn.Value, 0);
                DesiredDistance = Mathf.Max(DesiredDistance, MinZoomDistance);
            }
            if (input.ZoomOut.HasValue)
            {
                DesiredDistance = Mathf.Min(DesiredDistance + input.ZoomOut.Value, MaxZoomDistance);
            }

            // Set Camera Position
            float desired = DesiredDistance; // Where we want the camera to be
            float calculated = ViewCollision.CalculateMaximumDistanceFromTarget(target, Mathf.Max(desired, _previousDistance)); // The maximum distance we calculated we can be based off collision and preference
            float zoom = Zoom.CalculateDistanceFromTarget(_previousDistance, calculated, desired); // Where we want to be for the sake of zooming

            Vector3 zoomDistance = CameraTransform.Forward * zoom;

            if (canUpdateCameraPosition)
            {
                if (smoothCameraPosition && !splineFollow.followSpline & !cameraWindow.enabled)
                {
                    CameraTransform.Position = Vector3.Lerp(CameraTransform.Position, target - zoomDistance, 0.1f); // No buffer if the buffer would zoom us in past 0.
                }
                else if (splineFollow.followSpline)
                {
                    if (splineFollow.targetSpline != null)
                    {
                        float percent;
                        Vector3 point1 = splineFollow.targetSpline.GetPoint(0);
                        Vector3 point2 = splineFollow.targetSpline.GetPoint(1);
                        Vector3 targetToFollow = new Vector3(Target.Target.position.x, 0, Target.Target.position.z);

                        point1 = new Vector3(point1.x, 0, point1.z);
                        point2 = new Vector3(point2.x, 0, point2.z);

                        float minDistance = 0;
                        float maxDistance = Vector3.Distance(point1, point2);

                        float distance = Vector3.Distance(targetToFollow, point2);

                        Vector3 targetPos = target - zoomDistance;

                        //Calculate new position based on target X, camera Y and Z
                        percent = Mathf.InverseLerp(maxDistance, minDistance, distance) + splineFollow.targetSpline.compensationConstant;

                        //Update axis for spline follow
                        if (splineFollow.followOnXAxis)
                        {
                            targetPos.x = splineFollow.targetSpline.GetPoint(percent).x;
                        }

                        if (splineFollow.followOnYAxis)
                        {
                            targetPos.y = splineFollow.targetSpline.GetPoint(percent).y;
                        }

                        if (splineFollow.followOnZAxis)
                        {
                            targetPos.z = splineFollow.targetSpline.GetPoint(percent).z;
                        }

                        if (cameraWindow.enabled)
                        {
                            CameraTransform.Position = CurbCamera(targetPos);
                        }
                        else
                        {
                            CameraTransform.Position = targetPos;
                        }
                    }
                }
                else if (cameraWindow.enabled && !splineFollow.followSpline)
                {
                    CameraTransform.Position = CurbCamera(target - zoomDistance);
                }
                else
                {
                    CameraTransform.Position = target - zoomDistance;
                }
            }
            float actual = Vector3.Distance(CameraTransform.Position, target); 

            _previousDistance = actual;
            Target.ClearAdditionalOffsets();
        }

        private Vector3 CurbCamera(Vector3 referenceTargetPosition)
        {
            Vector3 closestPoint;
            Vector3 newTransformPosition = CameraTransform.Position;
            Vector3 nextFrameTargetPosition;


            //Get target screen position
            targetScreenPos = Camera.WorldToScreenPoint(Target.GetTarget());

            // (HORIZONTAL)
            if (targetScreenPos.x >= (Screen.width / 2) + cameraWindow.Right)
            {
                deltaX = targetScreenPos.x - ((Screen.width / 2) + cameraWindow.Right);
                newTransformPosition = new Vector3(Target.Target.position.x + deltaX, CameraTransform.Position.y + deltaY, referenceTargetPosition.z);
            }
            else if (targetScreenPos.x <= (Screen.width / 2) - cameraWindow.Left)
            {
                deltaX = targetScreenPos.x - ((Screen.width / 2) - cameraWindow.Left);
                newTransformPosition = new Vector3(Target.Target.position.x + deltaX, CameraTransform.Position.y + deltaY, referenceTargetPosition.z);
            }
            else
            {
                deltaX = 0;
            }
            // 20,20,200,10

            // (VERTICAL)
            if (targetScreenPos.y >= (Screen.height / 2) + cameraWindow.Top)
            {
                deltaY = targetScreenPos.y - ((Screen.height / 2) + cameraWindow.Top);
                newTransformPosition = new Vector3(Target.Target.position.x + deltaX, CameraTransform.Position.y + deltaY, referenceTargetPosition.z);
            }
            else if (targetScreenPos.y <= (Screen.height / 2) + cameraWindow.Bottom)
            {
                deltaY = targetScreenPos.y - ((Screen.height / 2) + cameraWindow.Bottom);
                newTransformPosition = new Vector3(Target.Target.position.x + deltaX, CameraTransform.Position.y + deltaY, referenceTargetPosition.z);
            }
            else
            {
                deltaY = 0;
            }
            //Calculate position at next frame with curb specific lerping
            nextFrameTargetPosition = Vector3.Lerp(CameraTransform.Position, newTransformPosition, cameraWindow.curbSmoothing * Time.deltaTime);


            //Remove lerped Z and put non-lerped value
            nextFrameTargetPosition.z = Mathf.Lerp (nextFrameTargetPosition.z, referenceTargetPosition.z, 5f * Time.deltaTime);
            nextFrameTargetPosition.x = Mathf.Lerp(nextFrameTargetPosition.x, newTransformPosition.x, 0.005f * Time.deltaTime);

            if (splineFollow.followOnXAxis)
            {
                //Remove lerped X and put non-lerped value
                nextFrameTargetPosition.x = referenceTargetPosition.x;
            }

            if (splineFollow.followOnYAxis)
            {
                //Remove lerped Y and put non-lerped value
                nextFrameTargetPosition.y = referenceTargetPosition.y;
            }

            if(splineFollow.targetSpline != null)
            {
                //Calculate closestPoint for outer bounds check
                closestPoint = splineFollow.targetSpline.GetComponent<BoxCollider>().ClosestPoint(nextFrameTargetPosition);

                //Re-calculate frame position with final lerp values
                nextFrameTargetPosition = Vector3.Lerp(nextFrameTargetPosition, new Vector3(referenceTargetPosition.x, nextFrameTargetPosition.y, referenceTargetPosition.z), 5f * Time.deltaTime);

            }
            else
            {
                closestPoint = nextFrameTargetPosition; 
            }

            //Check if camera is still inside of bounds
            if ((closestPoint == nextFrameTargetPosition))
            {
                //Return non-constrained position
                return nextFrameTargetPosition;
            }
            else
            {
                //Return bound-constrained position
                return closestPoint;
            }
        }

        private void OnGUI()
        {
            if(cameraWindow.showDebug)
            {
                DrawCameraInnerBounds(new Rect((Screen.width / 2) - cameraWindow.Left, (Screen.height / 2) - cameraWindow.Top, cameraWindow.Right + cameraWindow.Left, cameraWindow.Top + cameraWindow.Bottom), Color.red, Color.cyan);
            }
        }

        void DrawCameraInnerBounds(Rect position, Color backgroundColor, Color boundsColor)
        {
            
            Texture2D texture = new Texture2D(1, 1);
            backgroundColor.a = 0.5f;
            boundsColor.a = 0.7f;

            texture.SetPixel(0, 0, backgroundColor);
            texture.Apply();
            GUI.skin.box.normal.background = texture;
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), GUIContent.none);

            texture.SetPixel(0, 0, boundsColor);
            texture.Apply();
            GUI.skin.box.normal.background = texture;
            GUI.Box(position, GUIContent.none);
        }

        #region Camera Culling Mask
        public void ShowCullingLayer(string layerName)
        {
            this.Camera.cullingMask |= 1 << LayerMask.NameToLayer(layerName);
        }

        // Turn off the bit using an AND operation with the complement of the shifted int:
        public void HideCullingLayer(string layerName)
        {
            this.Camera.cullingMask &= ~(1 << LayerMask.NameToLayer(layerName));
        }

        // Toggle the bit using a XOR operation:
        public void ToggleCullingLayer(string layerName)
        {
            this.Camera.cullingMask ^= 1 << LayerMask.NameToLayer(layerName);
        }
        #endregion
    }
}
