using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private GameObject pShip;
    private ShipController sc;

    private Text tPlanetTarget;
    private Text tPlanetDistance;
    private int selectedPlanet = 0;

    void Start()
    {
        pShip = GameObject.FindGameObjectWithTag("Player");
        sc = pShip.GetComponent<ShipController>();
        var texts = GetComponentsInChildren<Text>();
        foreach (var item in texts)
        {
            if (item.name == "TextTarget")
                tPlanetTarget = item;
            if (item.name == "TextDistance")
                tPlanetDistance = item;
        }

        tPlanetTarget.text = GSpace.planets[selectedPlanet].GetComponent<Planet>().planetName;

    }

    private void OnGUI()
    {
        ShowDistance();
    }
    public void OnClickNextPlanet()
    {
        if (selectedPlanet + 1 <= GSpace.planetCount)
            selectedPlanet++;
        else
            selectedPlanet = 0;
        tPlanetTarget.text = GSpace.planets[selectedPlanet].GetComponent<Planet>().planetName;
    }
    public void OnClickPrevPlanet()
    {
        if (selectedPlanet - 1 >= 0)
            selectedPlanet--;
        else
            selectedPlanet = GSpace.planetCount;
        tPlanetTarget.text = GSpace.planets[selectedPlanet].GetComponent<Planet>().planetName;
    }
    public void OnAutoClick()
    {
        Vector3 pv = GSpace.planets[selectedPlanet].transform.position;
        Vector3 npv = new Vector3(pv.x, pv.y + 50, pv.z);
        sc.Auto(npv);
    }
    void ShowDistance()
    {
        float distance = Vector3.Distance(pShip.transform.position, GSpace.planets[selectedPlanet].transform.position);
        tPlanetDistance.text = (distance).ToString();
    }
}
