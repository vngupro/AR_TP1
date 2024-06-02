/*
 * Copyright 2021 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ReticleBehaviour : MonoBehaviour
{
    public GameObject Child;
    public DrivingSurfaceManager DrivingSurfaceManager;

    public ARPlane CurrentPlane;

    // Start is called before the first frame update
    private void Start()
    {
        Child = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        CurrentPlane = null;

        //Determine the center of the screen using Camera's ViewToScreenPoint. 
        // Perform raycast using the center of the viewport.
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        
        //Use this point to conduct a raycast
        var hits = new List<ARRaycastHit>();
        DrivingSurfaceManager.RaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinBounds);


        //Determine the intersection point of interest by querying the hits list. 
        //Prioritize the locked plane contained in DrivingSurfaceManager, and if it does not exist, 
        //use the first plane hit.
        ARRaycastHit? hit = null;
        if (hits.Count > 0)
        {
            // If you don't have a locked plane already...
            var lockedPlane = DrivingSurfaceManager.LockedPlane;
            hit = lockedPlane == null
                // ... use the first hit in `hits`.
                ? hits[0]
                // Otherwise use the locked plane, if it's there.
                : hits.SingleOrDefault(x => x.trackableId == lockedPlane.trackableId);
        }

        //If hit contains a result, move this GameObject's transform to the hit position.
        if (hit.HasValue)
        {
            CurrentPlane = DrivingSurfaceManager.PlaneManager.GetPlane(hit.Value.trackableId);
            // Move this reticle to the location of the hit.
            transform.position = hit.Value.pose.position;
        }

        Child.SetActive(CurrentPlane != null);
    }
}
