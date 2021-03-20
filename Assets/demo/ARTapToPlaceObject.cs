using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject placementIndicator;
    
    private ARSessionOrigin arOrigin;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    void Start(){
        arOrigin = FindObjectOfType<ARSessionOrigin>();
    }

    
    void Update(){
        UpdatePlacementPose();
        updatePlacementIndicator();
    }

    private void UpdatePlacementPose(){
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f,0.5f));
        var hits = new List<ARRaycastHit>();
        var rayCastMgr = GetComponent<ARRaycastManager>();
        rayCastMgr.Raycast(screenCenter,hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count >0;
        if (placementPoseIsValid){
            placementPose=hits[0].pose;
        }
    }

    private void updatePlacementIndicator(){
        if (placementPoseIsValid){
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position,placementPose.rotation);
        }else{
            placementIndicator.SetActive(false);
        }
    }
}
