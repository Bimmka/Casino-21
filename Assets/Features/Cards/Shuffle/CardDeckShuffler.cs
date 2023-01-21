using System;

namespace Features.Cards.Shuffle
{
  public class CardDeckShuffler
  {
    private readonly Random random;
    
    public CardDeckShuffler()
    {
      random = new Random();
    }
    
    public void Shuffle<T> (T[] array)
    {
        int n = array.Length;
        while (n > 1) 
        {
            int k = random.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
  }
}