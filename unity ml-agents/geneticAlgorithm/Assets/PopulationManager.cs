using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager : MonoBehaviour {
    public GameObject personPrefab;
    public int populationSize=10;
    List<GameObject>population=new List<GameObject>();
    public static float elapsed = 0;
    int trialTime = 10;
    int generation = 1;

    GUIStyle guiStyle = new GUIStyle();
    //new random color for person prefab
    private void OnGUI()
    {
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "generation: " + generation, guiStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "trial time: " + (int)elapsed, guiStyle);

    }


    // Use this for initialization
    void Start()
    {
        //create size i of population
        for (int i = 0; i < populationSize; i++)
        {
            // person should appear ata random position onscreen
            Vector3 pos = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f), 0);
            // create game obj by-default
            GameObject go = Instantiate(personPrefab, pos, Quaternion.identity);
            // set dna colour get dna from DNA.cs file
            go.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);
            //size factor 
            go.GetComponent<DNA>().s = Random.Range(0.1f, 0.3f);

            // add game obj(go) to population list...
            population.Add(go);
        }
    }
    //breeding happens between 2 parents only no three-somes allowed
    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        //parents are creating children
        Vector3 pos = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f), 0);
        GameObject offspring = Instantiate(personPrefab, pos, Quaternion.identity);
        //get parents dna from DNA files
        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();
        //swap parent DNA
        //we have left it to 50 percent chance of getting either parents COLOUR
        ////////////////////////////////////////////////////////////////////////////////
        //this code piece is what makes genetic algorithm work.....
        //explain it with red,green,blue eyes example
        // we have added the mutation through if..else statement
        //to high mutation set high range
        
        if (Random.Range(0, 1000) < 5)
        {
            offspring.GetComponent<DNA>().r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
            offspring.GetComponent<DNA>().g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
            offspring.GetComponent<DNA>().b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;
            //size factor 

            offspring.GetComponent<DNA>().s = Random.Range(0, 10) < 5 ? dna1.s : dna2.s;

        }
        else {

            offspring.GetComponent<DNA>().r = Random.Range(0.0f,1.0f);
            offspring.GetComponent<DNA>().g = Random.Range(0.0f,1.0f);
            offspring.GetComponent<DNA>().b = Random.Range(0.0f,1.0f);
            //size factor 
            offspring.GetComponent<DNA>().s = Random.Range(0.0f,1.0f);


        }
        return offspring;
        /////////////////////////////////////////////////////////////////////////////////////
    }

    
    void BreedNewPopulation() {
        //offspring
        List<GameObject> newPopulation = new List<GameObject>();
        //get rid of unfir individuals
        //order the population by value that is in individual DNA 
        //time to die is set when click on it at particular time(in secs)
        //list in them that order they died so fittest individual is at bottom of list
        // orderbydescending will give opposite results i.e first to get eliminated will be fittest!
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().timeToDie).ToList();
        population.Clear();
        // breed upper half of population
        for (int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++) {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i+1], sortedList[i]));


        }
        //destroy all previous population and parents
        for (int i = 0; i < sortedList.Count; i++) {
            Destroy(sortedList[i]);
        }
        generation++;

    }
	
	// Update is called once per frame
	void Update () {
        elapsed += Time.deltaTime;
        if (elapsed > trialTime) {
            BreedNewPopulation();
            elapsed = 0;
        }
	}
}
