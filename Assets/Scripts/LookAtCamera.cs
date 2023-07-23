using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioListener))]
public class LookAtCamera : MonoBehaviour
{
    [SerializeField]
    private AudioListener _camera;

    private void Update()
    {
        transform.forward = _camera.transform.position-transform.position;
    }
}
