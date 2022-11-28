using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairTarget : MonoBehaviour
{
    [SerializeField] private LayerMask ignore;
    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hitfor;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ray.origin = mainCamera.transform.position;
        ray.direction = mainCamera.transform.forward;


        transform.position = Physics.Raycast(ray, out hitfor, ignore) ? hitfor.point : ray.GetPoint(250f);
    }
}
