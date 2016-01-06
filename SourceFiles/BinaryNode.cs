using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryTreeCreator
{
  class BinaryNode
  {
    //Attributes
    BinaryNode leftNode;
    BinaryNode rightNode;
    bool avl;
    BinaryNode parent;
    double value;
    int blance;

    //Constructors
    public BinaryNode(double value, BinaryNode parent, bool avl)
    {
      this.value = value;
      this.parent = parent;
      leftNode = null;
      rightNode = null;
      this.avl = avl;
      balance = 0;
    }
    //if I'm the parent of the tree
    public BinaryNode(double value, bool avl)
    {
      this.value = value;
      this.parent = null;
      leftNode = null;
      rightNode = null;
      this.avl = avl;
      balance = 0;
    }
    //get Methods
    public double getValue()
    {
      return value;
    }
    public double getBalance()
    {
      calculateBalance();
      return balance;
    }
    public BinaryNode GetLeftNode()
    {
      return leftNode;
    }
    public BinaryNode GetRightNode()
    {
      return rightNode;
    }
    //set Methods
    public void SetRightNode(BinaryNode newNode)
    {
      rightNode = newNode;
    }
    public void SetLeftNode(BinaryNode newNode)
    {
      leftNode = newNode;
    }
    public void SetParent(BinaryNode newParent)
    {
      parent = newParent;
    }
    public void addValue(double newValue)
    {
      if(newValue > value)
      {
        //check if the right value is null otherwise add it at this Node
        if(rightNode == null)
        {
          rightNode = new BinaryNode(newValue,this, avl);
        }
        else
        {
          rightNode.addValue(newValue);
        }
      }
      else
      {
        //check if the left Node is null otherwise add it at this Node
        if(leftNode == null)
        {
          leftNode = new BinaryNode(newValue, this, avl);
        }
        else
        {
          leftNode.addValue(newValue);
        }
      }
      calculateBalance();
      //now handle the avl desire
      if(avl)
      {
        AVlHandling();
      }
    }
    public void deleteNode(double deleteValue)
    {
      if(deleteValue == value)
      {
        //now delete myself
        //check my children
        if(rightNode == null && leftNode == null)
        {
          //I have no children
          //just delete all references to me
          //check if I'm a right or a left node of my parent
          if(parent.getValue() > value)
          {
            //I'm a left node
            parent.SetLeftNode(null);
          }
          else
          {
            //I'm a right node
            parent.SetRightNode(null);
          }
        }
        else if(rightNode == null && leftNode != null)
        {
          //i have a left child
          //check if im a left or a right node of my parent and change the reference of me to the reference
          //to my children
          if(parent.getValue() > value)
          {
            //I'm a left node
            parent.SetLeftNode(leftNode);
          }
          else
          {
            //I'm a right node
            parent.SetRightNode(leftNode);
          }
        }
        else if(rightNode != null && leftNode == null)
        {
          // I have a right child
          //change the reference to me of my parent to the one to my children
          if(parent.getValue()> value)
          {
            //I'm a left node
            parent.SetLeftNode(rightNode);
          }
          else
          {
            //I'm a right node
            parent.SetRightNode(rightNode);
          }
        }
        else
        {
          //I have two children
          //search for the biggest value of the left Tree
          BinaryNode DeleteValue = leftNode;
          BinaryNode current = null;
          while((current = DeleteValue.GetRightNode())!= null)
          {
            DeleteValue = current;
          }
          //now change the value of the biggest value of the left Tree to my value
          value = DeleteValue.getValue();
          //delete the reference of the DeleteValue
          //save the maybeLeft
          BinaryNode maybeLeft = DeleteValue.GetLeftNode();
          parent.SetRightNode(null);
          if(maybeLeft != null)
          {
            addValue(maybeLeft.getValue());
          }
        }
      }
      else
      {
        //check which path to take
        if(deleteValue > value)
        {
          //it has to be a right node of me
          rightNode.deleteNode(deleteValue);
        }
        else
        {
          //it has to be a left node of me
          leftNode.deleteNode(deleteValue);
        }
      }
    }
    public int getDeep()
    {
      int deep = 1;
      int rightDeep = 0;
      int leftDeep = 0;
      if(rightNode != null)
      {
        rightDeep = rightNode.getDeep();
      }
      if(leftNode != null)
      {
        leftDeep = leftNode.getDeep();
      }
      //now check which of the two nodes are the most deepest
      if(rightDeep > leftDeep)
      {
        return deep +  rightDeep;
      }
      return deep + leftDeep;
    }
    //private methods|| most one for realization of the avl character of the tree
    private int calculateBalance()
    {
      //get the depth of the right Node
      int rightDeep = rightNode.getDeep();
      int leftDeep = leftNode.getDeep();
      balance = rightDeep - leftDeep;
    }
    public void RightRotation()
    {
      //get the right Node of the current Left Node to prevent for loosing data
      BinaryNode rightOfLeft = leftNode.GetRightNode();
      //setup the leftNode to be the parent
      leftNode.setParent(parent);
      leftNode.SetRightNode(this);
      //setup the parent
      //check if I'm the left or the right node of the parent
      if(parent.getValue > value)
      {
        //I'm a left node
        parent.SetLeftNode(leftNode);
      }
      else
      {
        //I'm a right node
        parent.SetRightNode(leftNode);
      }
      //setup myself
      parent = leftNode;
      leftNode = null;
      //now look if the rightOfLeft has a value
      if(rightOfLeft != null)
      {
        parent.addValue(rightOfLeft.getValue());
      }
    }
    public void LeftRotation()
    {
      //get the left Node of the current Right node to prevent loosing data
      BinaryNode leftOfRight = rightNode.GetLeftNode();
      //setup the right node
      rightNode.setParent(parent);
      rightNode.SetLeftNode(this);
      //setup parent
      if(parent.getValue() > value)
      {
        //I'm a left node
        parent.SetLeftNode(rightNode);
      }
      else
      {
        //I'm a right Node
        parent.SetRightNode(rightNode);
      }
      //setup myself
      parent = leftNode;
      leftNode = null;
      //now look if the leftOfRight has a value
      if(leftOfRight != null)
      {
        parent.addValue(leftOfRight.getValue());
      }
    }
    private void LeftRightRotation()
    {
      //make the left Rotation of the left Node
      leftNode.LeftRotation();
      //make the right Rotation
      RightRotation();
    }
    private void RightLeftRotation()
    {
      //make the rightRotation of the right Node
      rightNode.RightRotation();
      //make the left rotation of myself
      LeftRotation();
    }
    private void AVlHandling()
    {
      //this method gets only called when the AVL property is enabled
      //check my balance for 2 or -2
      switch(balance)
      {
        case 2:  //left rotation or RightLeftRotation : Check the balance of the right Node
                 switch(rightNode.getBalance())
                 {
                   case 1: //left Rotation
                           LeftRotation();
                    break;
                   case -1://RightLeftRotation
                           RightLeftRotation();
                    break;
                 }
          break;
        case -2://right rotation or LeftRightRotation
                switch(leftNode.getBalance())
                {
                  case 1:  //LeftRightRotation
                          LeftRightRotation();
                    break;
                  case -1: //RightRotation
                           RightRotation();
                    break;
                }
          break;
      }
    }
  }
}
