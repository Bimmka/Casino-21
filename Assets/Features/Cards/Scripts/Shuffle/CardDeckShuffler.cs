using System;
using System.Collections.Generic;

namespace Features.Cards.Scripts.Shuffle
{
  public class CardDeckShuffler
  {
    private readonly Random random;
    
    public CardDeckShuffler()
    {
      random = new Random();
    }
    
    public List<string> Shuffle(List<string> array)
    {
        int n = array.Count;
        while (n > 1) 
        {
            int k = random.Next(n--);
            string temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }

        return array;
    }
  }
}