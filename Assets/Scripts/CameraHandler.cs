using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _transitionSpeed = 6f;

    private Coroutine _corutine;
    private float _transitionDelta = 0.02f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Room>(out Room room))
        {
            if (_corutine != null)
                StopCoroutine(_corutine);

            _corutine = StartCoroutine(Move(room.transform.position));
        }
    }

    private IEnumerator Move(Vector2 targetPosition)
    {
        Vector2 currentCameraPosition = _camera.transform.position;

        while (currentCameraPosition != targetPosition)
        {
            Vector2 newCameraPosition = Vector2.Lerp(currentCameraPosition, targetPosition, _transitionSpeed * Time.deltaTime);
            _camera.transform.position = new Vector3(newCameraPosition.x, newCameraPosition.y, _camera.transform.position.z);

            if (Mathf.Abs(currentCameraPosition.magnitude - targetPosition.magnitude) < _transitionDelta)
                currentCameraPosition = targetPosition;
            else
                currentCameraPosition = _camera.transform.position;

            yield return null;
        }
    }
}
