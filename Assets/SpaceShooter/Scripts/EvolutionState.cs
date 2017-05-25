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

	public int N_cutsCrossover;
	public int IndividualElitism;

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
		selection = new RandomSelection ();
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
			new_pop = selection.selectIndividuals (population, populationSize - IndividualElitism);

			//-----------Crossover
			for (int i = 0; i < populationSize - IndividualElitism; i += 2) {
				Individual parent1 = new_pop [i];
				Individual parent2 = new_pop [i + 1];
				parent1.n_cuts = N_cutsCrossover; //----------------
				parent1.Crossover (parent2, crossoverProbability);
			}

			//-------------Mutation and Translation 
			for (int i = 0; i < populationSize - IndividualElitism; i++) {
				new_pop [i].Mutate (mutationProbability);
				new_pop [i].Translate ();
			}


			//------Elitism 
			population.Sort ((x, y) => y.Fitness.CompareTo (x.Fitness));
			for (int i = 0; i < IndividualElitism; i++) {
				//Debug.Log(population[i].Fitness);
				new_pop.Add (population [i]);
			}




			//Select new population
			population = new_pop;

			generation++;
		}
	}

	public void FinalLog()
	{
		stats.GenLog (population, generation);
		stats.FinalLog (individualMultiplier, numGenerations, populationSize, mutationProbability, crossoverProbability, tournamentSize, N_cutsCrossover,IndividualElitism);
	}

}

