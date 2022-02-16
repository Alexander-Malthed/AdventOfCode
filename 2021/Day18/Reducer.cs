using System;

namespace Day18
{
    static class Reducer
    {
        static Heap<Pair> explosionOrder;
        static Heap<RegularNumber> splitOrder;

        static public void SetExplosionAndSplitOrder(Heap<Pair> newExplosionOrder, Heap<RegularNumber> newSplitOrder)
        {
            explosionOrder = newExplosionOrder;
            splitOrder = newSplitOrder;
        }

        static public Pair ReducePair(Pair pairToReduce)
        {
            explosionOrder = new Heap<Pair>(100);
            splitOrder = new Heap<RegularNumber>(100);

            FindAllPairsToExplode(pairToReduce, 4);

            while (explosionOrder.Count > 0 || splitOrder.Count > 0)
            {
                if (explosionOrder.Count > 0)
                {
                    Pair pairToExplode = explosionOrder.RemoveFirst();

                    //Console.WriteLine($"exploded [{((RegularNumber)pairToExplode.LeftChild).Value},{((RegularNumber)pairToExplode.RightChild).Value}]");
                    ExplodePair(pairToExplode);
                    //PrintString(pairToReduce);
                    continue;
                }

                if (splitOrder.Count > 0)
                {
                    RegularNumber numberToSplit = splitOrder.RemoveFirst();

                    if (!numberToSplit.Valid)
                    {
                        continue;
                    }

                    //Console.WriteLine($"splitted [{numberToSplit.Value}]");
                    SplitRegularNumber(numberToSplit);
                    //PrintString(pairToReduce);
                }
            }

            return pairToReduce;
        }

        #region Actions
        // Add the left value of the pair into the immediate number to the left.
        // Add the right value of the pair into the immediate number to the right.
        // Replace pair with a RegularNumber with the value 0.
        // Exploding [1,2] in [3,[[1,2],4]] results in [4,[0,6]].
        static void ExplodePair(Pair explodingPair)
        {
            // Add to right number. Check if that number should be split.
            RegularNumber closestNumber = FindClosestRegularNumberRight(explodingPair);
            if (closestNumber != null)
            {
                closestNumber.Value += ((RegularNumber)explodingPair.RightChild).Value;

                if (closestNumber.Value > 9)
                {
                    closestNumber.UpdateDistance();

                    if (!splitOrder.Contains(closestNumber))
                    {
                        splitOrder.Add(closestNumber);
                    }
                }
            }

            // Add to left number. Check if that number should be split.
            closestNumber = FindClosestRegularNumberLeft(explodingPair);
            if (closestNumber != null)
            {
                closestNumber.Value += ((RegularNumber)explodingPair.LeftChild).Value;

                if (closestNumber.Value > 9)
                {
                    closestNumber.UpdateDistance();

                    if (!splitOrder.Contains(closestNumber))
                    {
                        splitOrder.Add(closestNumber);
                    }
                }
            }

            // Replace the pair with a 0.
            new RegularNumber(0, explodingPair.ParentPair, explodingPair.ChildState);

            explodingPair.RightChild.Valid = false;
            explodingPair.LeftChild.Valid = false;
            explodingPair = null;
        }

        // Numbers above 9 gets split into a pair following the rule [floor(n/2), ceil(n/2)].
        // 15 becomes [7,8].
        static void SplitRegularNumber(RegularNumber numberToExplode)
        {
            // Create pair.
            int leftValue = (int)Math.Floor((float)numberToExplode.Value / 2);
            int rightValue = (int)Math.Ceiling((float)numberToExplode.Value / 2);
            Pair newPair = new Pair(leftValue, rightValue, numberToExplode.ParentPair, numberToExplode.ChildState);

            // Check if the new pair should explode.
            Pair parent = newPair.ParentPair;
            int numOfParents = 1;
            while (parent.ParentPair != null)
            {
                parent = parent.ParentPair;
                if (++numOfParents == 4)
                {
                    newPair.UpdateDistance();

                    if (!explosionOrder.Contains(newPair))
                    {
                        explosionOrder.Add(newPair);
                    }

                    break;
                }
            }

            // Check if the new numbers should be split.
            RegularNumber leftNumber = (RegularNumber)newPair.LeftChild;
            RegularNumber rightNumber = (RegularNumber)newPair.RightChild;

            if (leftNumber.Value > 9)
            {
                splitOrder.Add(leftNumber);
            }

            if (rightNumber.Value > 9)
            {
                splitOrder.Add(rightNumber);
            }
        }
        #endregion

        #region Find
        #region FindClosestNumber
        static RegularNumber FindClosestRegularNumberRight(Pair explodingPair)
        {
            Pair parent = explodingPair.ParentPair;
            Pair curSearchPair;

            if (explodingPair.ChildState == ChildState.LEFTCHILD)
            {
                if (parent.RightChild is RegularNumber)
                {
                    return (RegularNumber)parent.RightChild;
                }

                curSearchPair = (Pair)parent.RightChild;
            }
            else
            {
                while (true)
                {
                    switch (parent.ChildState)
                    {
                        case ChildState.NONE:
                            return null;

                        case ChildState.LEFTCHILD:
                            break;

                        case ChildState.RIGHTCHILD:
                            parent = parent.ParentPair;
                            continue;

                        default:
                            break;
                    }

                    break;
                }

                if (parent.ParentPair.RightChild is RegularNumber)
                {
                    return (RegularNumber)parent.ParentPair.RightChild;
                }

                curSearchPair = (Pair)parent.ParentPair.RightChild;
            }

            while (curSearchPair.LeftChild is Pair)
            {
                curSearchPair = (Pair)curSearchPair.LeftChild;
            }

            return (RegularNumber)curSearchPair.LeftChild;
        }

        static RegularNumber FindClosestRegularNumberLeft(Pair explodingPair)
        {
            Pair parent = explodingPair.ParentPair;
            Pair curSearchPair;

            if (explodingPair.ChildState == ChildState.RIGHTCHILD)
            {
                if (parent.LeftChild is RegularNumber)
                {
                    return (RegularNumber)parent.LeftChild;
                }

                curSearchPair = (Pair)parent.LeftChild;
            }
            else
            {
                while (true)
                {
                    switch (parent.ChildState)
                    {
                        case ChildState.NONE:
                            return null;

                        case ChildState.LEFTCHILD:
                            parent = parent.ParentPair;
                            continue;

                        case ChildState.RIGHTCHILD:
                            break;

                        default:
                            break;
                    }

                    break;
                }

                if (parent.ParentPair.LeftChild is RegularNumber)
                {
                    return (RegularNumber)parent.ParentPair.LeftChild;
                }

                curSearchPair = (Pair)parent.ParentPair.LeftChild;
            }

            while (curSearchPair.RightChild is Pair)
            {
                curSearchPair = (Pair)curSearchPair.RightChild;
            }

            return (RegularNumber)curSearchPair.RightChild;
        }
        #endregion

        static void FindAllPairsToExplode(Pair pair, int depth)
        {
            if (depth <= 0)
            {
                if (pair.LeftChild is RegularNumber && pair.RightChild is RegularNumber)
                {
                    pair.UpdateDistance();
                    explosionOrder.Add(pair);
                    return;
                }
            }

            if (pair.RightChild is Pair)
            {
                FindAllPairsToExplode((Pair)pair.RightChild, depth - 1);
            }

            if (pair.LeftChild is Pair)
            {
                FindAllPairsToExplode((Pair)pair.LeftChild, depth - 1);
            }
        }
        #endregion
    }
}