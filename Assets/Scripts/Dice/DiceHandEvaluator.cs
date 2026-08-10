public enum DiceHand
{
    None,
    Pair_1,
    Pair_2,
    Triple,
    FullHouse,
    Straight,
    Four,
    Lucky
}


public class DiceHandEvaluator
{
    public DiceHand Evaluate(int[] diceValue)
    {
        if (diceValue == null || diceValue.Length != 5)
            return DiceHand.None;

        int[] counts = CountDice(diceValue);

        int pairCount = 0;
        bool hasTriple = false;
        bool hasFour = false;
        bool hasLucky = false;

        for (int i = 0; i <= 6; i++)
        {
            switch (counts[i])
            {
                case 2:
                    pairCount++;
                    break;
                case 3:
                    hasTriple = true;
                    break;
                case 4:
                    hasFour = true;
                    break;
                case 5:
                    hasLucky = true;
                    break;
            }
        }

        if (hasLucky) 
            return DiceHand.Lucky;

        if (hasFour) 
            return DiceHand.Four;

        if (IsStraight(counts)) 
            return DiceHand.Straight;

        if (hasTriple && pairCount == 1)
            return DiceHand.FullHouse;

        if (hasTriple)
            return DiceHand.Triple;
        
        if (pairCount == 2)
            return DiceHand.Pair_2;
        
        if (pairCount == 1)
            return DiceHand.Pair_1;

        return DiceHand.None;
    }

    private int[] CountDice(int[] diceValue)
    {
        int[] counts = new int[7];

        for (int i = 0; i < diceValue.Length; i++)
        {
            counts[diceValue[i]]++;
        }

        return counts;
    }

    private bool IsStraight(int[] counts)
    {
        bool lowStraight =
            counts[1] == 1 &&
            counts[2] == 1 &&
            counts[3] == 1 &&
            counts[4] == 1 &&
            counts[5] == 1;

        bool hightStraight =
            counts[2] == 1 &&
            counts[3] == 1 &&
            counts[4] == 1 &&
            counts[5] == 1 &&
            counts[6] == 1;

        return lowStraight || hightStraight;
    }
}
