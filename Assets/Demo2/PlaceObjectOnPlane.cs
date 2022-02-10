using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaceObjectOnPlane : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager RCM;

    static List<ARRaycastHit> Hits = new List<ARRaycastHit>();
    static List<ARRaycastHit> HitsForPlacement = new List<ARRaycastHit>();

    [SerializeField]
    GameObject Object;
    public GameObject DirectionIndicator;
    Vector3 ObjectLocation;
    Vector3 CameraLocation;
    Vector3 CameraDirection;
    Vector3 DownOffsetNav;
    Vector3 DirectionRay;
    int count = 0;

    private bool placementPoseIsValid = true;
    void Start()
    {
        Object.SetActive(false);
        DirectionIndicator.SetActive(false);
    }
    void Update()
    {
        if (count == 0)
        {
            Touch touch = Input.GetTouch(index: 0);

            if (touch.phase == TouchPhase.Began)
            {
                if (RCM.Raycast(touch.position, Hits, trackableTypes: UnityEngine.XR.ARSubsystems.TrackableType.Planes))
                {

                    Pose hitPose = Hits[0].pose;
                    ObjectLocation = hitPose.position;
                    count = 1;

                    Object.SetActive(true);
                    Object.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                }
            }
        }

        else
        {
            CameraLocation = Camera.current.transform.position;
            CameraDirection = Camera.current.transform.forward;
            DirectionRay = (ObjectLocation - CameraLocation).normalized;
            DirectionIndicator.SetActive(true);
            DirectionIndicator.transform.SetPositionAndRotation(CameraLocation + 0.5f * CameraDirection, Quaternion.LookRotation(DirectionRay));
        }
    }
}
