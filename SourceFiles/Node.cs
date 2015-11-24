using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;

namespace BinaryTreeCreator
{
   class Node
   {
     //Attributes
     double Value;
     Node rightNode;
     Node leftNode;
     public Node(double Value)
     {
       this.Value = Value;
     }
     public double getValue()
     {
       return Value;
     }
     public Node getRightNode()
     {
       return rightNode;
     }
     public Node getLeftNode()
     {
       return leftNode()
     }
     public void SetNode(double Value)
     {
       if(Value < this.Value)
       {
         //set it left
         //check for null
         if(leftNode == null)
         {
           //Create a new Node
           leftNode = new Node(Value);
         }
         else
         {
           leftNode.SetNode(Value);
         }
       }
       else
       {
         //set it right
         //check for null
         if(rightNode == null)
         {
           //Create a new Node
           rightNode = new Node(Value);
         }
         else
         {
           rightNode.SetNode(Value);
         }
       }
     }
   }
}
