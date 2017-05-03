using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TournamentSelection : SelectionMethod
{
	int tournamentSize;

	public TournamentSelection (int nTournament) : base ()
	{
		tournamentSize = nTournament;
	}

	//		public override List<Individual> selectIndividuals (List<Individual> oldpop, int num)
	//		{
	//		return selectIndividual (oldpop, num); //---------
	//		}
	//
	//		List<Individual> tournamentSelection(List<Individual>oldpop, int num) {
	//
	//			List<Individual> selectedInds = new List<Individual> ();
	//
	//			for (int i = 0; i<num; i++) {
	//				//make sure selected individuals are different
	//				Individual ind = selectIndividual(oldpop);
	//				while (selectedInds.Contains(ind)) { //useless while aqui
	//					ind = selectIndividual(oldpop);
	//				}
	//				selectedInds.Add (ind.Clone()); //we return copys of the selected individuals
	//			}
	//
	//			return selectedInds;
	//		}



	public override List<Individual> tournamentSelection (List<Individual>oldpop, int tornNum)
	{
		if (tournamentSize > oldpop.Count) { // checks if list lenghts are the same
			tournamentSize = oldpop.Count;
		}

		List<Individual> selectedInds = new List<Individual>(); 

		for (int i = 0; i<tornNum; i++) { 					// tornament
			selectedInds.Add(selectIndividual(oldpop).Clone());	
		}
				

		return selectedInds;

	}


	//função selecionar individuo
	public override Individual selectIndividual (List<Individual> oldpop)
	{
		List<Individual> indList = new List<Individual> ();
		int index = (int)(Random.Range (0, tournamentSize)); 
		for (int i = 0; i < tournamentSize; i++) {    					
			while (indList.Contains (oldpop[index])) {
				index = (int)(Random.Range (0, tournamentSize));
			}
			indList.Add (oldpop [index]);
		}
			Individual best = indList [0];
			
		float bestFitness = best.Fitness;
		for (int j = 1; i < tournamentSize; j++) {
			if (indList [j].Fitness > bestFitness) {
				best = indList [j];
				bestFitness = indList [j].Fitness;
			}
		}
		return best;
	}
}

