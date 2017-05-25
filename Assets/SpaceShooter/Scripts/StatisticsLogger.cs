using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class StatisticsLogger {

	public Dictionary<int,float> bestFitness;
	public Dictionary<int,float> meanFitness;
	public Dictionary<int,float> standardFitness; //----- standart dev
	public Dictionary<int, float> worstFitness; //------ worst fit

	//public GameController pscore;

	private string filename;
	private StreamWriter logger;

	public StatisticsLogger(string name) {
		filename = name;
		bestFitness = new Dictionary<int,float> ();
		meanFitness = new Dictionary<int,float> ();
		standardFitness = new Dictionary<int, float> (); //----- stdt dev
		worstFitness = new Dictionary<int, float> (); //------ worst fit

	}

	//saves fitness info and writes to console
	public void GenLog(List<Individual> pop, int currentGen) {
		pop.Sort((x, y) => y.Fitness.CompareTo(x.Fitness));

		float avgSquaredFitness = 0;
		bestFitness.Add (currentGen, pop[0].Fitness);
		meanFitness.Add (currentGen, 0f);

		foreach (Individual ind in pop) {
			meanFitness[currentGen]+=ind.Fitness;
		}
		meanFitness [currentGen] /= pop.Count;


		foreach (Individual ind in pop) { // ------- standart deviantion 
			avgSquaredFitness += Mathf.Pow (ind.Fitness - meanFitness [currentGen], 2);
		}

		float a = Mathf.Sqrt (avgSquaredFitness / pop.Count);
		standardFitness.Add (currentGen, a); //-------


		worstFitness.Add (currentGen, pop [pop.Count - 1].Fitness); //---- worst fit



		//pscore = GameController.; 

		Debug.Log ("generation: " + currentGen + "\tbest: " + bestFitness [currentGen] + "\tmean: " + meanFitness [currentGen]+ "\tworst: "+ worstFitness [currentGen]+  "\tstandard:" + standardFitness [currentGen] );
		//Debug.Log ("generation: " + currentGen + "\t solution: " + pop [0].ToString ());



	}

	//writes to file
	public void FinalLog(int individualMultiplier, int numGenerations, int populationSize, float mutationProbability, float crossoverProbability, int tournamentSize, int N_cutsCrossover,int IndividualElitism ) {
		logger = File.CreateText (filename);

		logger.WriteLine ("Data: ");
		logger.WriteLine ("Ind mult: " + individualMultiplier);
		logger.WriteLine ("Num Generations: "+ numGenerations);
		logger.WriteLine ("Pop Size: "+ populationSize );
		logger.WriteLine ( "Mutation: " + mutationProbability);
		logger.WriteLine ( "Crossover: " + crossoverProbability );
		logger.WriteLine ( "Tourn Size: " + tournamentSize);
		logger.WriteLine ( "N cuts: " + N_cutsCrossover);
		logger.WriteLine ( "Elistism: " + IndividualElitism );

		logger.WriteLine ("G |"+"Best |"+ "Mean   |"+"Worst|"+" Dev    |");
		//writes with the following format: generation, bestfitness, meanfitness
		for (int i=0; i<bestFitness.Count; i++) {
			logger.WriteLine(i+" | "+bestFitness[i]+" | "+meanFitness[i]+" | " + worstFitness[i]  +" | "+standardFitness[i]);
		}

		logger.Close ();
	}
}


//guarda melhor e média de cada geração
//enunciado pede tambem desvio padrão
