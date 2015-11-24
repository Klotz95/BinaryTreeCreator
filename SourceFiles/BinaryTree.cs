using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;

namespace BinaryTreeCreator
{
  class BinaryTree
  {
    //Attributes
    Node root;
    public BinaryTree(double Value)
    {
      root = new Node(Value);
    }
    public BinaryTree(Node root)
    {
      this.root = root;
    }
    public Node getRoot()
    {
      return root;
    }
    
  }
}
