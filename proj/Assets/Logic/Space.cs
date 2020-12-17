using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    private int maxPlanet = 9;
    private int planetCount;
    private int minPlanetDistance = 1200;
    private int maxPlanetDistance = 2200;

    private List<GameObject> planets = new List<GameObject>();
    private GameObject pShip;
    private ShipController sCon;
    private Camera cam;

    private List<GameObject> asteroidsList;

    void Start()
    {
        pShip = GameObject.FindGameObjectWithTag("Player");
        sCon = pShip.GetComponent<ShipController>();
        cam = Camera.main;

        asteroidsList = new List<GameObject>();
        asteroidsList.Add(Resources.Load<GameObject>("Asteroids\\Asteroid 1"));
        asteroidsList.Add(Resources.Load<GameObject>("Asteroids\\Asteroid 2"));
        asteroidsList.Add(Resources.Load<GameObject>("Asteroids\\Asteroid 3"));

        Vector3 scale = new Vector3(0.01f, 0.01f, 0.01f);
        asteroidsList[0].transform.localScale = scale;
        asteroidsList[1].transform.localScale = scale;
        asteroidsList[2].transform.localScale = scale;


        LoadPlanets();
        Generate();
        StartCoroutine(CreateAsteroid());
    }

    void Update()
    {
        
    }
    void LoadPlanets()
    {
        for (int i = 1; i <= maxPlanet; i++)
        {
            planets.Add(GameObject.Find($"P{i}"));
            planets[i - 1].GetComponent<Planet>().planetName = $"Планета {i}";
            planets[i - 1].GetComponent<Planet>().planetNumber = i;
        }
        if (planets.Count != maxPlanet)
            Debug.LogError("Error planets loading from");
        GSpace.planets = planets;
    }
    void Generate()
    {
        foreach (var item in planets)
            item.SetActive(false);
        int pCount = Random.Range(1, maxPlanet);
        for (int i = 0; i < pCount; i++)
        {
            int x = Random.Range(minPlanetDistance, maxPlanetDistance);
            int y = Random.Range(minPlanetDistance, maxPlanetDistance);
            int z = Random.Range(minPlanetDistance, maxPlanetDistance);
            Transform tr = planets[i].GetComponent<Transform>();
            Vector3 pos = new Vector3();
            if (i == 0)
                pos = new Vector3(x * GetSign(), y * GetSign(), z * GetSign());
            else
                pos = new Vector3(x + planets[i - 1].GetComponent<Transform>().position.x * GetSign(), y * GetSign(), z * GetSign());
            tr.position = pos;
            planets[i].SetActive(true);
        }
        GSpace.planetCount = planetCount = pCount - 1;
    }
    int GetSign()
    {
        int sign = 1;
        if (Random.Range(0, 2) == 0)
            sign = 1;
        else
            sign = -1;
        return sign;
    }
    IEnumerator CreateAsteroid()
    {
        while (true)
        {
            if (sCon.currentSpeed >= GSpace.shipAcceleration)
            {
                var ast = Instantiate(asteroidsList[1]);
                ast.transform.position = new Vector3(cam.transform.position.x - 0.3f, cam.transform.position.y, cam.transform.position.z);
                ast.transform.position += cam.transform.forward;
                float r = Random.Range(0.3f, 5f);
                yield return new WaitForSeconds(r);
            }
            yield return new WaitForSeconds(2f);
        }
    }

}
