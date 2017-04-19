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
		

	public override void Mutate (float probability)  //------------- falta implementar   (pelo menos um de mutção)
	{
		//throw new System.NotImplementedException ();
		NewValueMutation1 (probability);
		//NewValueMutation2 (probability);

	}

	public override void Crossover (Individual partner, float probability) // ---------------- falta implementar (pelo menos um de crossover)
	{
		throw new System.NotImplementedException ();
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