using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExampleIndividual : Individual {

	public int multiplier = 10; // [-1,-1,-1,-1, 0,0,0,0, 1,1,1,1] ajuda na procura

	private int chromosomeSize;
	private int[] chromosome1; //horizontal
	private bool[] chromosome2; //shoots

	public ExampleIndividual(int size, int mult): base(size)
	{
		multiplier = mult;
		chromosomeSize = (int) (size / multiplier);
		chromosome1 = new int[chromosomeSize];
		chromosome2 = new bool[chromosomeSize];
	}

	public override void Initialize () //  preenche com valores aleatorios
	{
		for (int i = 0; i < chromosomeSize; i++) 
		{
			chromosome1 [i] = Random.Range (-1, 2);
			chromosome2 [i] = (Random.Range (0, 2) == 1); // true or false
		}
			
	}
		

	public override void Mutate (float probability)  //------------- 
	{
		SequenceMutation (probability); 
	}
		



	public void SequenceMutation(float probability){
	
		for (int i = 0; i < chromosomeSize; i++) {
			if (Random.Range (0f,1f) <= probability){
				chromosome1 [i] = Random.Range (-1, 2); //------ dá novo valor random
				chromosome2 [i] = (Random.Range(0,2) == 1);
			}
		}
	} // modify to not be the same as before

	//--------------------------------

	public override void Crossover (Individual partner, float probability)
	{
		//N_Point Crossover
		//Basic theory: 
		//1: random probability of happening the crossover between 0f and 1f
		//Loop through all chromossome pairs
		//Pick a random position to cut (n_cuts) ---------> should be dynamically changed in Unity, not in the code
		//Create 2 new chromossomes with the two parts cut (Clone?)
		ExampleIndividual bitFlipPartner = (ExampleIndividual)partner;

		//Debug.Log (n_cuts + " cuts");

		if (UnityEngine.Random.Range (0f, 1f) > probability) {
			return;
		}
		int crossoverPoint = Mathf.FloorToInt (chromosomeSize / (n_cuts + 1));

		for (int i = crossoverPoint; i < chromosomeSize; i += 2 * crossoverPoint) {
			for (int j = i; j < chromosomeSize && j < i + crossoverPoint; j++) {
				int temp1 = chromosome1 [j];
				bool temp2 = chromosome2 [j];
				chromosome1 [j] = bitFlipPartner.chromosome1 [j];
				chromosome2 [j] = bitFlipPartner.chromosome2 [j];

				bitFlipPartner.chromosome1 [j] = temp1;
				bitFlipPartner.chromosome2 [j] = temp2;

			}
		}



	}



	//public override void Crossover(Individual partner, float probability) // ---------------- falta implementar (pelo menos um de crossover)
	//{
	//	throw new System.NotImplementedException ();
	//}


//
//	public override void NCrossover(Individual partner, float probability,int cutPoints) {
//
//		if (UnityEngine.Random.Range (0f, 1f) > probability) {
//			return;
//		}
//
//		List<int> ncrossover = new List<int>();
//
//		int found = 0;
//		int rand;
//
//		while(ncrossover.Count != cutPoints)
//		{
//			rand = UnityEngine.Random.Range(0, chromosomeSize);
//
//			if (!ncrossover.Contains(rand))
//			{
//				ncrossover.Add(rand);
//			}
//		}
//
//		ncrossover.Sort ();
//
//
//		List<float> ncrossoverFinal = new List<float>();
//		for (int j = 0; j < ncrossover.Count; j++)
//		{
//			int limit = (j == ncrossover.Count-1)?cutPoints-1:ncrossover[j+1];
//			for (int i = ncrossover[j]; i < limit; i++)
//			{
//				float parent = [[i]];
//				trackPoi[keys[i]] = partner.trackPoints[keys[i]];
//				partner.trackPoints[keys[i]] = tmp;
//				j++;
//			}
//		}
//
//	}
//
//
//	void HalfCrossover(Individual partner, float probability) {
//
//		if (UnityEngine.Random.Range (0f, 1f) > probability) {
//			return;
//		}
//		//this example always splits the chromosome in half
//		int crossoverPoint = Mathf.FloorToInt (info.numTrackPoints / 2f);
//		List<float> keys = new List<float>(trackPoints.Keys);
//		for (int i=0; i<crossoverPoint; i++) {
//			float tmp = trackPoints[keys[i]];
//			trackPoints[keys[i]] = partner.trackPoints[keys[i]];
//			partner.trackPoints[keys[i]]=tmp;
//		}
//
//	}
//


	//---- desvio padrao
	float GetDesvioPadrao() {
		float mean = (chromosome1[0] + chromosome1[chromosome1.Length]) /2;
		float sigma = (chromosome1[chromosome1.Length] - mean) / 3;

		return UnityEngine.Random.Range(mean, sigma);
	}






	public override void Translate () //traduzir o fenotipo para o genotipo
	{
		for (int i = 0; i < chromosomeSize; i++) 
		{
			for (int j = 0; j < multiplier; j++) 
			{
				horizontalMoves [i * multiplier + j] = chromosome1 [i];
				shots [i * multiplier + j] = chromosome2 [i];
			}
		}
	}

	public override Individual Clone ()  //copiar um individuo para outro individuo 
	{
		ExampleIndividual new_ind = new ExampleIndividual(totalSize, multiplier);

		chromosome1.CopyTo (new_ind.chromosome1, 0);
		chromosome2.CopyTo (new_ind.chromosome2, 0);

		//new_ind.Translate ();

		new_ind.fitness = 0.0f;
		new_ind.evaluated = false;

		return new_ind;
	}

	public void HalfCrossover(Individual ind, float probability){
		if (UnityEngine.Random.Range (0f, 1f) > probability) {
			return;
		}
		//TODO continue function
	}

	public void NewValueMutation1 (float probability){
		
	}
		

	public override string ToString ()
	{
		string res = "[ExampleIndividual] Chromosome1: [";

		for (int i = 0; i < chromosomeSize; i++) {
			res += chromosome1 [i].ToString ();
			if (i != chromosomeSize - 1) {
				res += ",";
			}
		}

		res += "] Chromosome2: [";

		for (int i = 0; i < chromosomeSize; i++) {
			res += chromosome2 [i].ToString ();
			if (i != chromosomeSize - 1) {
				res += ",";
			}
		}
		res += "]";

		return res;
	}
}


// Perceber como os cromossomas são criados, o porquê de existirem chromossom1 e chromossom2 e para que é que servem
// Perceber como é que fazemos as mutações: em que lista é que temos de fazer as mutações