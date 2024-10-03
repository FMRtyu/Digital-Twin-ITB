using DigitalTwinITB.jatinangor;
using UnityEngine;

namespace CameraControl {
	public class CameraMotion : MonoBehaviour {
		[SerializeField] private float _speed = 1f;
		[SerializeField] private float _smoothing = 5f;
		[SerializeField] private Vector2 _range = new (100, 100);

		private bool dragPanMoveActive = false;
		public bool isCCTVSelected;
		private Vector2 lastMousePosition;
		private Vector3 _targetPosition;
		private Vector3 _input;

		private JatinangorController _controller;

		private void Awake() {
			_targetPosition = transform.position;

            _controller = FindAnyObjectByType<JatinangorController>();

        }
			
		private void HandleInput() {
			float x = Input.GetAxisRaw("Horizontal");
			float z = Input.GetAxisRaw("Vertical");

			Vector3 right = transform.right * x;
			Vector3 forward = transform.forward * z;

			_input = (forward + right).normalized;
		}

		private void Move() {
			Vector3 nextTargetPosition = _targetPosition + _input * _speed;
			if (IsInBounds(nextTargetPosition)) _targetPosition = nextTargetPosition;
			transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _smoothing);
		}

		private bool IsInBounds(Vector3 position) {
			return position.x > -_range.x &&
				   position.x < _range.x &&
				   position.z > -_range.y &&
				   position.z < _range.y;
		}
		
		private void Update() {
            if (!isCCTVSelected)
            {
				if (_controller != null)
				{
                    if (_controller.currentCameraState != CameraState.World)
                    {
                        return;
                    }
                }
				HandleInput();
				if (!dragPanMoveActive)
				{
					Move();
				}
				HandleCameraMovementDragPan();
			}
		}

		private void HandleCameraMovementDragPan()
		{
			Vector3 inputDir = new Vector3(0, 0, 0);

			if (Input.GetMouseButtonDown(1))
			{
				dragPanMoveActive = true;
				lastMousePosition = Input.mousePosition;
			}
			if (Input.GetMouseButtonUp(1))
			{
				dragPanMoveActive = false;
			}

			if (dragPanMoveActive)
			{
				Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePosition;

				float dragPanSpeed = 10f;
				inputDir.x = mouseMovementDelta.x * dragPanSpeed;
				inputDir.z = mouseMovementDelta.y * dragPanSpeed;

				lastMousePosition = Input.mousePosition;
			}

			Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

			float moveSpeed = 50f;
			transform.position += moveDir * moveSpeed * Time.deltaTime;
			_targetPosition = transform.position;
		}
		private void OnDrawGizmos() {
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(transform.position, 5f);
			Gizmos.DrawWireCube(Vector3.zero, new Vector3(_range.x * 2f, 5f, _range.y * 2f));
		}
	}
}