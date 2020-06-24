using System.Collections.Generic;
using UnityEngine;

public class TouchControls : ControlsBase
{
    public class TouchInfo
    {
        public Vector2 touchStartPosition;
        public Vector2 touchEndPosition;
        public Vector2 touchDragVector;
        public bool touchComplete;
    }

    private RectTransform touchMoveRect;
    private RectTransform touchShootRect;
    private RectTransform touchSwapShipRect;
    private RectTransform touchSwapWeaponRect;

    private List<TouchInfo> touchInfos = new List<TouchInfo>();

    public TouchControls(RectTransform touchMoveRect, RectTransform touchShootRect, RectTransform touchSwapShipRect, RectTransform touchSwapWeaponRect)
    {
        this.touchMoveRect = touchMoveRect;
        this.touchShootRect = touchShootRect;
        this.touchSwapShipRect = touchSwapShipRect;
        this.touchSwapWeaponRect = touchSwapWeaponRect;
    }

    public override void ReadInput()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            if (this.touchInfos.Count <= i)
                this.touchInfos.Add(new TouchInfo());

            // Set touch information
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    this.touchInfos[i].touchComplete = false;
                    this.touchInfos[i].touchStartPosition = touch.position;
                    break;
                case TouchPhase.Moved:
                    this.touchInfos[i].touchDragVector = touch.position - this.touchInfos[i].touchStartPosition;
                    break;
                case TouchPhase.Ended:
                    this.touchInfos[i].touchEndPosition = touch.position;
                    this.touchInfos[i].touchDragVector = this.touchInfos[i].touchEndPosition - this.touchInfos[i].touchStartPosition;
                    this.touchInfos[i].touchComplete = true;
                    break;
            }

            // Check which part of the screen the touch input is in
            if (RectTransformUtility.RectangleContainsScreenPoint(touchMoveRect, touch.position, null))
            {
                // Move input
                horizontalInput = this.touchInfos[i].touchComplete ? 0 : this.touchInfos[i].touchDragVector.normalized.x;
                verticalInput = this.touchInfos[i].touchComplete ? 0 : this.touchInfos[i].touchDragVector.normalized.y;
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(touchShootRect, touch.position, null))
            {
                // Shoot input
                shoot = !this.touchInfos[i].touchComplete;
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(touchSwapShipRect, touch.position, null))
            {
                // Swap Ship input
                if (this.touchInfos[i].touchComplete)
                    cycleShip = 1;
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(touchSwapWeaponRect, touch.position, null))
            {
                // Swap Weapon input
                if (this.touchInfos[i].touchComplete)
                    cycleWeapon = 1;
            }

            this.touchInfos[i].touchComplete = false;
        }
    }
}
