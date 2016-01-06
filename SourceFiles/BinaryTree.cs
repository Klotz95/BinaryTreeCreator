using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryTreeCreator
{
  class BinaryTree
  {
    //Attributes
    BinaryNode root;
    int levelcount;
    int nodeCount;

    public BinaryTree(bool avl, double startValue)
    {
      root = new BinaryNode(startValue, avl);
    }
    public BinaryTree(BinaryNode root)
    {
      this.root = root;
    }
    public int getNodeCount()
    {
      BinaryNode[,] current = toArray();
      int result = 0;
      for(int i = 0; i < getLevelCount(); i++)
      {
        for(int k = 0; k < Convert.ToInt32(Math.Pow(2,i));k++)
        {
          //check if there is a node. If yes increase the count of nodes
          if(current[i,k] != null)
          {
            result++;
          }
        }
      }
      return result;
    }
    public double[] getLevelOrder()
    {
      BinaryNode[,] current = toArray();
      double[] result = new double[0];
      for(int i = 0; i < getLevelCount(); i++)
      {
        for(int k = 0; k < Convert.ToInt32(Math.Pow(2,i));k++)
        {
          if(current[i,k] != null)
          {
            //add it to the result array
            double[] backup = result;
            result = new double[backup.Length + 1];
            for(int j = 0; j < backup.Length; j++)
            {
              result[j] = backup[j];
            }
            result[backup.Length] = current[i,k].getValue();
          }
        }
      }
      return result;
    }
    public Node[,] toArray()
    {
      Node[,] result = new Node[getLevelCount(), Convert.ToInt32(Math.Pow(2,getLevelCount() - 1))];
      result[0,0] = root;
      for(int i = 0; i < getLevelCount() - 2; i++)
      {
        int currentXIndex = 0;
        for(int k = 0; k < Convert.ToInt32(Math.Pow(2,i));k++)
        {
          if(result[i,k] != null)
          {
            //get the left Value and add it to the array
            result[i + 1, currentXIndex] = result[i,k].GetLeftNode();
            currentXIndex++;
            result[i + 1, currentXIndex] = result[i,k].GetRightNode();
          }
          else
          {
            currentXIndex += 2;
          }
        }
      }
    }
    public int getLevelCount()
    {
      root.getDeep();
    }
    public double[] GetPreOrder(BinaryNode current)
    {
      if(current == null)
      {
        //use the root as initial value
        current = root;
      }
      //W-L-R
      //add my own value
      double[] result = {value};
      double[] backup;
      if(leftNode != null)
      {
        double[] leftResult = GetPreOrder(leftNode);
        backup = result;
        result = new double[backup.Length + leftResult.Length];
        for(int i = 0; i < backup.Length; i++)
        {
          result[i] = backup[i];
        }
        for(int i = 0; i < leftResult.Length; i++)
        {
          result[i + backup.Length] = leftResult[i];
        }
      }
      if(rightNode != null)
      {
        double[] rightResult = GetPreOrder(rightNode);
        backup = result;
        result = new double[backup.Length + rightResult.Length];
        for(int i = 0; i < backup.Length; i++)
        {
          result[i] = backup[i];
        }
        for(int i = 0; i < rightResult.Length; i++)
        {
          result[i + backup.Length] = rightResult[i];
        }
      }
      //now return the result
      return result;
    }
    public double[] GetInOrder(BinaryNode current)
    {
      if(current == null)
      {
        //use the root as initial value
        current = root;
      }
      //L-W-R
      double[] result = new double[0];
      double[] backup;
      if(leftNode != null)
      {
        result = GetInOrder(leftNode);
      }
      //add my own value
      backup = result;
      result = new double[backup.Length + 1];
      for(int i = 0; i < backup.Length; i++)
      {
        result[i] = backup[i];
      }
      result[backup.Length] = value;
      //add the right value
      if(rightNode != null)
      {
        double[] rightResult = GetInOrder(rightNode);
        backup = result;
        result = new double[backup.Length + rightResult.Length];
        for(int i = 0; i < backup.Length; i++)
        {
          result[i] = backup[i];
        }
        for(int i = 0; i < rightResult.Length; i++)
        {
          result[i + backup.Length] = rightResult[i];
        }
      }
      //return the result
      return result;
    }
    public double[] GetPostOrder(BinaryNode current)
    {
      //check for the first call of the method
      if(current == null)
      {
        current = root;
      }
      //L-R-W
      double[] result = new double[0];
      if(leftNode != null)
      {
        double[] leftResult = GetPostOrder(leftNode);
        result = leftResult;
      }
      if(rightNode != null)
      {
        double[] righResult = GetPostOrder(rightNode);
        double[] backup = result;
        result = new double[backup.Length + rightResult.Length];
        for(int i = 0; i < backup.Length; i++)
        {
          result[i] = backup[i];
        }
        for(int i = 0; i < rightResult.Length; i++)
        {
          result[i + backup.Length] = rightResult[i];
        }
      }
      //now add my own value
      double[] backup = result;
      result = new double[backup.Length + 1];
      for(int i = 0; i < backup.Length; i++)
      {
        result[i] = backup[i];
      }
      result[backup.Length] = value;
      //return the result
      return result;
    }
  }
}
