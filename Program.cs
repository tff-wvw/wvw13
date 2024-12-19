using System;
using System.Collections.Generic;

public class AVLTree
{

    public class Node
    {
        public int Key;
        public string Data; 
        public Node Left;
        public Node Right;
        public int Height;

        public Node(int key, string data)
        {
            Key = key;
            Data = data;
            Left = Right = null;
            Height = 1;
        }
    }

    private Node root;

    
    public Node Delete(Node node, int key)
    {
        if (node == null)
            return node;

        if (key < node.Key)
            node.Left = Delete(node.Left, key);
        else if (key > node.Key)
            node.Right = Delete(node.Right, key);
        else
        {
            if (node.Left == null || node.Right == null)
            {
                Node temp = node.Left ?? node.Right;
                if (temp == null)
                {
                    temp = node;
                    node = null;
                }
                else
                    node = temp;
            }
            else
            {
                Node temp = MinValueNode(node.Right);
                node.Key = temp.Key;
                node.Right = Delete(node.Right, temp.Key);
            }
        }

        if (node == null)
            return node;

        node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;

        int balance = GetBalance(node);

        if (balance > 1 && GetBalance(node.Left) >= 0)
            return RightRotate(node);

        if (balance > 1 && GetBalance(node.Left) < 0)
        {
            node.Left = LeftRotate(node.Left);
            return RightRotate(node);
        }

        if (balance < -1 && GetBalance(node.Right) <= 0)
            return LeftRotate(node);

        if (balance < -1 && GetBalance(node.Right) > 0)
        {
            node.Right = RightRotate(node.Right);
            return LeftRotate(node);
        }

        return node;
    }

    private Node MinValueNode(Node node)
    {
        Node current = node;
        while (current.Left != null)
            current = current.Left;

        return current;
    }

    private Node RightRotate(Node y)
    {
        Node x = y.Left;
        Node T2 = x.Right;

        x.Right = y;
        y.Left = T2;

        y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
        x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

        return x;
    }

    private Node LeftRotate(Node x)
    {
        Node y = x.Right;
        Node T2 = y.Left;

        y.Left = x;
        x.Right = T2;

        x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
        y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

        return y;
    }

    private int GetHeight(Node node)
    {
        if (node == null)
            return 0;

        return node.Height;
    }

    private int GetBalance(Node node)
    {
        if (node == null)
            return 0;

        return GetHeight(node.Left) - GetHeight(node.Right);
    }
    public Node Search(Node node, int key)
    {
        if (node == null || node.Key == key)
            return node;

        if (key < node.Key)
            return Search(node.Left, key);

        return Search(node.Right, key);
    }
    public int GetMinValue(Node node)
    {
        Node current = node;
        while (current.Left != null)
            current = current.Left;

        return current.Key;
    }

    public Node MaxValueNode(Node node)
    {
        Node current = node;

        while (current.Right != null)
        {
            current = current.Right;
        }

        return current;
    }

    public int CountNodes(Node node)
    {
        if (node == null)
            return 0;

        return CountNodes(node.Left) + CountNodes(node.Right) + 1;
    }
    public bool IsBalanced(Node node)
    {
        if (node == null)
            return true;

        int balance = GetBalance(node);
        return Math.Abs(balance) <= 1 && IsBalanced(node.Left) && IsBalanced(node.Right);
    }
    public int aGetHeight(Node node)
    {
        if (node == null)
            return 0;

        return node.Height;
    }
    public string PreOrderTraversal(Node node)
    {
        if (node == null)
            return "";

        return node.Key + " " + PreOrderTraversal(node.Left) + PreOrderTraversal(node.Right);
    }
    public Node CopyTree(Node node)
    {
        if (node == null)
            return null;

        Node newNode = new Node(node.Key, node.Data) 
        {
            Left = CopyTree(node.Left), 
            Right = CopyTree(node.Right), 
            Height = node.Height 
        };

        return newNode;
    }

    public AVLTree MergeTrees(AVLTree otherTree)
    {
        if (otherTree == null)
            return this;

        AVLTree mergedTree = new AVLTree();
        InOrderTraversal(root, mergedTree);
        InOrderTraversal(otherTree.root, mergedTree);
        return mergedTree;
    }

    private void InOrderTraversal(Node node, AVLTree tree)
    {
        if (node != null)
        {
            InOrderTraversal(node.Left, tree); 
            tree.Insert(node.Key, node.Data);  
            InOrderTraversal(node.Right, tree); 
        }
    }

    public bool IsSubtree(Node root1, Node root2)
    {
        if (root2 == null)
            return true;

        if (root1 == null)
            return false;

        if (AreIdentical(root1, root2))
            return true;

        return IsSubtree(root1.Left, root2) || IsSubtree(root1.Right, root2);
    }

    private bool AreIdentical(Node node1, Node node2)
    {
        if (node1 == null && node2 == null)
            return true;

        if (node1 != null && node2 != null)
            return node1.Key == node2.Key && AreIdentical(node1.Left, node2.Left) && AreIdentical(node1.Right, node2.Right);

        return false;
    }
    public string ReverseInOrderTraversal(Node node)
    {
        if (node == null)
            return "";

        return ReverseInOrderTraversal(node.Right) + node.Key + " " + ReverseInOrderTraversal(node.Left);
    }
    public Node FindPredecessor(Node root, int key)
    {
        Node current = Search(root, key);
        if (current == null)
            return null;

        if (current.Left != null)
            return MaxValueNode(current.Left);

        Node predecessor = null;
        Node ancestor = root;
        while (ancestor != current)
        {
            if (key < ancestor.Key)
            {
                ancestor = ancestor.Left;
            }
            else
            {
                predecessor = ancestor;
                ancestor = ancestor.Right;
            }
        }

        return predecessor;
    }

    public Node FindSuccessor(Node root, int key)
    {
        Node current = Search(root, key);
        if (current == null)
            return null;

        if (current.Right != null)
            return MinValueNode(current.Right);

        Node successor = null;
        Node ancestor = root;
        while (ancestor != current)
        {
            if (key < ancestor.Key)
            {
                successor = ancestor;
                ancestor = ancestor.Left;
            }
            else
                ancestor = ancestor.Right;
        }

        return successor;
    }
    public List<int> KeysInRange(Node node, int low, int high)
    {
        List<int> keysInRange = new List<int>();

        if (node == null)
            return keysInRange;

        if (node.Key > low)
            keysInRange.AddRange(KeysInRange(node.Left, low, high));

        if (node.Key >= low && node.Key <= high)
            keysInRange.Add(node.Key);

        if (node.Key < high)
            keysInRange.AddRange(KeysInRange(node.Right, low, high));

        return keysInRange;
    }
    public void Insert(int key, string data)
    {
        root = Insert(root, key, data);
    }

    private Node Insert(Node node, int key, string data)
    {
        if (node == null)
            return new Node(key, data);

        if (key < node.Key)
            node.Left = Insert(node.Left, key, data);
        else if (key > node.Key)
            node.Right = Insert(node.Right, key, data);
        else
            return node;

        node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;

        int balance = GetBalance(node);
        if (balance > 1 && key < node.Left.Key)
            return RightRotate(node);

        if (balance < -1 && key > node.Right.Key)
            return LeftRotate(node);

        if (balance > 1 && key > node.Left.Key)
        {
            node.Left = LeftRotate(node.Left);
            return RightRotate(node);
        }

        if (balance < -1 && key < node.Right.Key)
        {
            node.Right = RightRotate(node.Right);
            return LeftRotate(node);
        }

        return node;
    }

    public int GetHeightIteratively(Node root)
    {
        if (root == null)
            return 0;

        Stack<Node> stack = new Stack<Node>();
        stack.Push(root);
        int height = 0;

        while (stack.Count > 0)
        {
            Node current = stack.Pop();
            if (current != null)
            {
                height = Math.Max(height, current.Height);
                stack.Push(current.Left);
                stack.Push(current.Right);
            }
        }

        return height;
    }
}
