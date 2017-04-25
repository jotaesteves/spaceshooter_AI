using UnityEngine;
using System.Collections;
using System.Collections.Generic;


	public class TournamentSelection : SelectionMethod
	{
		int tournamentNumber;

		public TournamentSelection (int nTournament) : base() {
			tournamentNumber = nTournament;
		}

		public override List<Individual> selectIndividuals (List<Individual> oldpop, int num)
		{
			return tournamentSelection (oldpop, num);
		}

		List<Individual> tournamentSelection(List<Individual>oldpop, int num) {

			List<Individual> selectedInds = new List<Individual> ();

			for (int i = 0; i<num; i++) {
				//make sure selected individuals are different
				Individual ind = selectIndividual(oldpop);
				while (selectedInds.Contains(ind)) {
					ind = selectIndividual(oldpop);
				}
				selectedInds.Add (ind.Clone()); //we return copys of the selected individuals
			}

			return selectedInds;
		}

		Individual selectIndividual(List<Individual> oldpop){

			List<Individual> indList = new List<Individual> ();
			for(int i = 0; i < tournamentNumber; i++){
				int index = (int)(Random.Range(0, tournamentNumber));
				indList.Add(oldpop[index]);
			}

			Individual best = indList[0];
			float bestFitness = best.Fitness;
			for(int i = 1; i < tournamentNumber; i++){
				if(indList[i].Fitness < bestFitness){
					best = indList[i];
					bestFitness = indList[i].Fitness;
				}
			}
			return best;
		}

	}


