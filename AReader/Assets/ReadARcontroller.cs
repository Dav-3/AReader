using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit;
using HoloToolkit.Unity.InputModule;
using UnityEngine.UI;

public class ReadARcontroller: MonoBehaviour {

	public int ticks=0;
	public int textPanelNr=0;
	public GameObject thePanel;
	public BaseRayStabilizer Stabilizer = null;
	private Text theText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		ticks++;
		if (ticks > 180) {
			
			ticks = 0;

			// find out location of where the gaze cursor focuses
			int layerMask = 1 << LayerMask.NameToLayer("SpatialSurface");

			//construct a Ray using the position and forward direction of the User’s head
			Ray GazeRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

			//Raycast using constructed Ray from Gaze and store collisions in array hits
			RaycastHit[] hits = Physics.RaycastAll(GazeRay, float.MaxValue, layerMask);

			if (hits.Length > 0)
			{
				foreach (RaycastHit hit in hits)
				{
					
					Debug.Log(string.Format("Hit Object **\"**{0}**\"** at position **\"**{1}**\"**", hit.collider.gameObject, hit.point));

					// create a new instance of the textPanel prefab
					thePanel = Instantiate(thePanel, hit.point, Quaternion.identity);
					textPanelNr++;
					
					theText = thePanel.GetComponentInChildren<Text>();
					theText.text = "test " + textPanelNr.ToString();

				}
			}
		

			//RaycastHit hitInfo;
//			bool hitSomething = Physics.Raycast (
//				                       Camera.main.transform.position,
//				                       Camera.main.transform.forward,
//				                       out hitInfo,
//				                       20.0f,
//				                       Physics.AllLayers);
//
//			if (hitSomething)
//			{
//				// If the Raycast has succeeded and hit a hologram
//				// hitInfo's point represents the position being gazed at
//				// hitInfo's collider GameObject represents the hologram being gazed at
//
//
//				// create a new instance of the textPanel prefab
//				thePanel = Instantiate(thePanel, hitInfo.point, Quaternion.identity);
//				textPanelNr++;
//
//				theText = thePanel.GetComponentInChildren<Text>();
//				theText.text = "test " + textPanelNr.ToString();
//
//			} // if it hit something


		} // if ticks > 180

	} // Update()

} // class
