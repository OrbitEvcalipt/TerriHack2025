using System;
using UnityEngine;

namespace FunnyBlox
{
    [System.Serializable]
    public class Anchor
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector3 _localOffset;
        [SerializeField] private Vector2 _pixelOffset;

        public Vector3 GetPoint(RectTransform canvasRectTransform, Camera camera = null)
        {
            var point = default(Vector3);
            TryGetPoint(canvasRectTransform, ref point, camera);
            return point;
        }

        public bool TryGetPoint(RectTransform canvasRectTransform, ref Vector3 point, Camera camera = null)
        {
            if (camera == null)
                camera = Camera.main;

            if (camera == null)
                return false;

            var worldPoint = _transform != null ? _transform.TransformPoint(_localOffset) : _localOffset;
            var viewportPoint = GetViewportPoint(camera, worldPoint);

            var canvasRect = canvasRectTransform.rect;
            var canvasX = canvasRect.xMin + canvasRect.width * viewportPoint.x + _pixelOffset.x;
            var canvasY = canvasRect.yMin + canvasRect.height * viewportPoint.y + _pixelOffset.y;

            point = canvasRectTransform.TransformPoint(canvasX, canvasY, 0.0f);

            return InvaidViewportPoint(camera, viewportPoint) != true;
        }

        private Vector3 GetViewportPoint(Camera camera, Vector3 point)
        {
            point = RectTransformUtility.WorldToScreenPoint(camera, point);
            point.z = 0.5f;

            return camera.ScreenToViewportPoint(point);
        }

        private static bool InvaidViewportPoint(Camera camera, Vector3 point)
        {
            if (point.x < -10.0f) return true;
            if (point.x > 10.0f) return true;
            if (point.y < -10.0f) return true;
            if (point.y > 10.0f) return true;
            if (point.z < camera.nearClipPlane) return true;

            return false;
        }

        public void SetOffset(Vector2 pixelOffset) => _pixelOffset = pixelOffset;
        
        public void SetTarget(Transform target) => _transform = target;
    }

    [ExecuteInEditMode]
    public class AnchorToTransform : MonoBehaviour
    {
        private static Vector3 _defaultPoint = new(100000.0f, 100000.0f);

        [SerializeField] private Anchor _anchor = new();

        private Camera _cameraWorld;
        private RectTransform _rectTransformCached;
        private Canvas _canvasCached;
        private RectTransform _canvasRectTransform;

        public Camera WorldCamera { get => _cameraWorld; set => _cameraWorld = value; }

        private void UpdatePoint()
        {
            if (_canvasCached == null)
                return;

            var point = default(Vector3);

            SetPosition(_rectTransformCached, _anchor.TryGetPoint(_canvasRectTransform, ref point, _cameraWorld) ? point : _defaultPoint);
        }

        private void LateUpdate() => UpdatePoint();

        private static void SetPosition(RectTransform rectTransform, Vector3 position)
        {
            rectTransform.position = position;
            rectTransform.anchoredPosition3D = rectTransform.anchoredPosition3D;
        }

        private void Start() => Setup(Camera.main);

        public void Setup(Camera camera)
        {
            _cameraWorld = camera;
            _canvasCached = gameObject.GetComponentInParent<Canvas>();
            _canvasRectTransform = _canvasCached.GetComponent<RectTransform>();
            _rectTransformCached = GetComponent<RectTransform>();
        }

        public void SetPixelOffset(Vector2 pixelOffset) => _anchor.SetOffset(pixelOffset);
        public void SetTarget(Transform target) => _anchor.SetTarget(target);
    }
}