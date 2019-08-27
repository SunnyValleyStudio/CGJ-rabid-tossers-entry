using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SVSPredictor
{
    public class InputPredictorSVS : MonoBehaviour
    {
        private bool _sequenceReady = false;
        public bool SequenceReady { get => _sequenceReady; private set => _sequenceReady = value; }

        /// <summary>
        /// Latest inputs given by the player
        /// </summary>
        private LinkedList<Vector3> _recentInputs = new LinkedList<Vector3>();
        /// <summary>
        /// Dictionary containing all sequences and counts of what inputs already followed this 
        /// sequence in the past.
        /// </summary>
        private Dictionary<List<Vector3>, InputTypeClass> _predictionModel = new Dictionary<List<Vector3>, InputTypeClass>(new ListComparer());


        Vector3 _recentPredictedInput = Vector3.zero;
        //Not knowing if we use horizontal or vertical input I choose to call
        //the types by the sign they bare ex. (1,0,0) = positive, (-1,0,0) = negative
        Vector3 _positiveInput = Vector3.zero;
        Vector3 _negativeInput = Vector3.zero;

        int _positiveGuesses = 0;
        int _negativeGuesses = 0;
        

        public Vector3 PredictNextInput(Vector3 input)
        {
            Vector3 tempPrediction = _positiveInput;
            if (_recentInputs.Count >= 5)
            {
                InputTypeClass inputCountsForThisSequence = _predictionModel[_recentInputs.Take(5).ToList()];
                UpdateModelIfLastPredictionWasInvalid(input, inputCountsForThisSequence);
                Debug.Log("Average prediction score: " + ((float)(_positiveGuesses) / (_positiveGuesses+_negativeGuesses)));
                _recentInputs.RemoveFirst();
                _recentInputs.AddLast(input);
                inputCountsForThisSequence = _predictionModel[_recentInputs.ToList()];
                if (inputCountsForThisSequence.Positive == inputCountsForThisSequence.Negative || inputCountsForThisSequence.Positive > inputCountsForThisSequence.Negative)
                {
                    tempPrediction = _positiveInput;
                }
                else
                {
                    tempPrediction = _negativeInput;
                }

            }
            else
            {
                _recentInputs.AddLast(input);
            }
            
            _recentPredictedInput = tempPrediction;

            return tempPrediction;
        }

        private void UpdateModelIfLastPredictionWasInvalid(Vector3 input, InputTypeClass inputCountsForThisSequence)
        {
            bool wasLastPredictionValid = false;
            if (Vector3.Distance(_recentPredictedInput, input) < 0.01f)
            {
                wasLastPredictionValid = true;
                _positiveGuesses++;
            }
            if (wasLastPredictionValid == false)
            {
                _negativeGuesses++;
                if (Vector3.Distance(_positiveInput, input) < 0.01f)
                {
                    inputCountsForThisSequence.Positive++;
                }
                else
                {
                    inputCountsForThisSequence.Negative++;
                }
            }
        }

        /// <summary>
        /// Generate all sequences of COUNT length from items in the items collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> items, int count)
        {
            int i = 0;
            foreach (var item in items)
            {
                if (count == 1)
                    yield return new T[] { item };
                else
                {
                    foreach (var result in GetPermutations(items.Skip(i + 1), count - 1))
                        yield return new T[] { item }.Concat(result);
                }

                ++i;
            }
        }


        /// <summary>
        /// Prepares the predictor
        /// </summary>
        /// <param name="inputExample"></param>
        public void PrepareThePredictorClass(Vector3 inputExample)
        {
            PresetPositiveNegativeInputValues(inputExample);
            List<List<Vector3>> _allSequencesList = CreateAll32Sequences(inputExample);
            FillInPredictionModelDictionary(_allSequencesList);
            SequenceReady = true;
        }

        /// <summary>
        /// Creates all 32 possible sequences of 5 consecutive inputs
        /// player can give.
        /// </summary>
        /// <param name="inputExample"></param>
        /// <returns></returns>
        private static List<List<Vector3>> CreateAll32Sequences(Vector3 inputExample)
        {
            var listOfInputsToConsider = new List<Vector3> { inputExample, -inputExample, inputExample, -inputExample, inputExample, -inputExample, inputExample, -inputExample, inputExample, -inputExample, };
            var data = GetPermutations(listOfInputsToConsider, 5);
            List<List<Vector3>> _allSequencesList = new List<List<Vector3>>();
            foreach (var item in data)
            {

                List<Vector3> tempList = new List<Vector3>();
                foreach (var element in item)
                {
                    tempList.Add(element);
                }
                bool val = false;
                foreach (var elementList in _allSequencesList)
                {
                    if (elementList.SequenceEqual(tempList))
                    {
                        val = true;
                    }
                }
                if (val == false)
                {
                    _allSequencesList.Add(tempList);
                }
            }

            return _allSequencesList;
        }

        /// <summary>
        /// Creates the sequence dictionary that will store counts of input player already gave
        /// for that sequence.
        /// </summary>
        /// <param name="_allSequencesList"></param>
        private void FillInPredictionModelDictionary(List<List<Vector3>> _allSequencesList)
        {
            foreach (var sequence in _allSequencesList)
            {
                _predictionModel.Add(sequence, new InputTypeClass());
            }
        }

        /// <summary>
        /// Need to preset what is Vector3 value for positive direction movement and for negative.
        /// Needed for returning predicted input.
        /// </summary>
        /// <param name="inputExample"></param>
        private void PresetPositiveNegativeInputValues(Vector3 inputExample)
        {
            if ((inputExample.x + inputExample.y + inputExample.z) > 0)
            {
                _positiveInput = inputExample;
                _negativeInput = -inputExample;
            }
            else
            {
                _positiveInput = -inputExample;
                _negativeInput = inputExample;
            }
        }

        /// <summary>
        /// Class represenging how many positive inputs and how many negative inputs followed
        /// given sequence (see predictionModel dictionary). Check for which type of input followed
        /// the sequence most often will be choosen as a predicted type for next input.
        /// </summary>
        class InputTypeClass
        {
            public int Positive = 0;
            public int Negative = 0;
        }

        sealed class ListComparer : EqualityComparer<List<Vector3>>
        {
            public override bool Equals(List<Vector3> x, List<Vector3> y)
              => StructuralComparisons.StructuralEqualityComparer.Equals(x?.ToArray(), y?.ToArray());

            public override int GetHashCode(List<Vector3> x)
              => StructuralComparisons.StructuralEqualityComparer.GetHashCode(x?.ToArray());
        }
    }
}

