using UnityEngine;
using System.Collections;
using System.Collections.Generic;    

public class EvolutionState : MonoBehaviour
{
	public int individualSize;
	public int individualMultiplier;
	public int numGenerations;
	public int populationSize;
	public float mutationProbability;
	public float crossoverProbability;
	public int tournamentSize;
	public string statsFilename = "log.txt";
	public StatisticsLogger stats;

	protected List<Individual> population;
	protected SelectionMethod selection;

	protected int evaluatedIndividuals;

	public int generation; 

	public List<Individual> Population
	{
		get
		{
			return population;
		}
	}

	public Individual Best
	{
		get
		{
			float max = float.MinValue;
			Individual max_ind = null;
			foreach (Individual indiv in population) {
				if (indiv.Fitness > max) {
					max = indiv.Fitness;
					max_ind = indiv;
				}
			}
			return max_ind;
		}
	}

	void Start()
	{
		generation = 0;
		selection = new RandomSelection ();  //-------- mudar quando for outro algoritmo
		stats = new StatisticsLogger (statsFilename);   
	}


	public virtual void InitPopulation(){
		population = new List<Individual> ();

		while (population.Count < populationSize) {
			ExampleIndividual new_ind= new ExampleIndividual (individualSize, individualMultiplier);
			new_ind.Initialize ();
			new_ind.Translate ();
			population.Add (new_ind);
		}
			
	}

	//The Step function assumes that the fitness values of all the individuals in the population have been calculated.
	public virtual void Step()
	{
		if (generation < numGenerations) 
		{
			List<Individual> new_pop;

			//Store statistics in log
			stats.GenLog (population, generation);

			//Select parents
			new_pop = selection.selectIndividuals (population, populationSize);

			//Crossover
			for (int i = 0; i < populationSize; i += 2) {
				Individual parent1 = new_pop [i];
				Individual parent2 = new_pop [i + 1];
				parent1.Crossover (parent2, crossoverProbability);
			}

			//Mutation and Translation 
			for (int i = 1; i < populationSize; i++) {
				new_pop [i].Mutate (mutationProbability);
				new_pop [i].Translate ();
			}
				
			//Select new population
			population = new_pop;

			generation++;
		}
	}

	public void FinalLog()
	{
		stats.GenLog (population, generation);
		stats.FinalLog ();
	}

}

