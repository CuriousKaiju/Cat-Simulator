using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PointerIcon : MonoBehaviour {

    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private float _rotationTime;
    public PawPointer _pawPointer;
    public Transform _cameraTarget;
    public Player _player;
    bool _isShown = true;

    private void Awake() {
        _image.enabled = false;
        _isShown = false;
    }

    public void SetIconPosition(Vector3 position, Quaternion rotation) {
        transform.position = position;
        transform.rotation = rotation;
    }

    public void Show() {
        if (_isShown) return;
        _isShown = true;
        StopAllCoroutines();
        StartCoroutine(ShowProcess());
    }

    public void Hide()
    {
        if (!_isShown) return;
        _isShown = false;

        StopAllCoroutines();
        StartCoroutine(HideProcess());
    }
    
    public void RotateCameraToTheWayPointer()
    {
        _player.SetControllStatus(false);
        _cameraTarget.DOLookAt(_pawPointer.transform.position, _rotationTime).OnComplete(() =>
        {
            _player.SetControllStatus(true);
        });
    }
    IEnumerator ShowProcess() {
        _image.enabled = true;
        transform.localScale = Vector3.zero;
        for (float t = 0; t < 1f; t += Time.deltaTime * 4f) {
            transform.localScale = Vector3.one * t;
            yield return null;
        }
        transform.localScale = Vector3.one;
    }

    IEnumerator HideProcess() {

        for (float t = 0; t < 1f; t += Time.deltaTime * 4f) {
            transform.localScale = Vector3.one * (1f - t);
            yield return null;
        }
        _image.enabled = false;
    }

}
