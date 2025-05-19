using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour
{
    private Image _image;

    private Transform _camera;
    private void Awake()
    {
        Init();
    }
    private void LateUpdate()
    {
        SetUIForwardVector(_camera.forward);
    }

    private void Init() {
        _image = GetComponentInChildren<Image>();
        _camera = Camera.main.transform;
    }

    public void SetImageFillAmount(float curValue) {
        _image.fillAmount = curValue;
    }

    public void SetUIForwardVector(Vector3 targetVector) {
        transform.forward = targetVector;
    }
}
