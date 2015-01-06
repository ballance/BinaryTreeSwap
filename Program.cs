using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SwapoSwap
{
    class Node
    {
        public int value;
        public Node parent { get; set; }
        public Node left { get; set; }
        public Node right { get; set; }

        public Node addLeft(int value)
        {
            this.left = new Node(value);
            this.left.parent = this;
            this.left.left = null;
            this.left.right = null;
            return this.left;
        }

        public Node addRight(int value)
        {
            this.right = new Node(value);
            this.right.parent = this;
            this.right.left = null;
            this.right.right = null;
            return this.right;
        }

        public Node(int value)
        {
            this.value = value;
            this.parent = null;
            this.left = null;
            this.right = null;
        }

        public Node getRoot()
        {
            Node n = this;
            while (n.parent != null)
            {
                n = n.parent;
            }
            return n;
        }

        public static void swap(ref Node A, ref Node B)
        {
            var newA = new Node(B.value);
            var newB = new Node(A.value);

            newA.left = A.left;
            newA.right = A.right;
            newA.parent = A.parent;

            newB.left = B.left;
            newB.right = B.right;
            newB.parent = B.parent;

            // Fix up parent node for A
            if (A.parent.left == A)
            {
                // A is a left node
                A.parent.left = newA;
            }
            if (A.parent.right == A)
            {
                // A is a Right node
                A.parent.right = newA;
            }

            // Fix up parent node for B
            if (B.parent.left == B)
            {
                // B is a left node
                B.parent.left = newB;
            }
            if (B.parent.right == B)
            {
                // B is a right node
                B.parent.right = newB;
            }

           
            if (newA.right == B)
            {
                // If B was a right child of A, update reference to newB
                newA.right = newB;
            }
            if (newA.left == A)
            {
                // If B was a left child of A, update reference to newB
                newA.left = newB;
            }

            if (newB.right == A)
            {
                // If A was a right child of B, update reference to newA
                newB.right = newA;
            }
            if (newB.left == A)
            {
                // If A was a left child of B, update reference to newA
                newA.left = newB;
            }

            // Update child references to be orphaned to point to new parents for A
            A.left.parent = newA;
            A.right.parent = newA;

            // Update child references to be orphaned to point to new parents for A
            B.left.parent = newB;
            B.right.parent = newB;

            // Final Swap to update ref types
            A = newA;
            B = newB;

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var root = new Node(10);
            var a = root.addLeft(1);
            var b = root.addRight(2);
            var c = a.addLeft(3);
            var d = a.addRight(4);
            var e = b.addLeft(5);
            var f = b.addRight(6);
            var g = d.addLeft(7);
            var h = d.addRight(8);
            Node.swap(ref a, ref d);

            if (root.left.value != 4) 
                throw new ApplicationException("New Root --> left --> value != 4 as expected");
            Console.WriteLine("New root --> left node has correct value of 4");

            if ((root.left.right.parent != root.left))
                throw new Exception("and root --> left --> right has incorrect parent");   
            Console.WriteLine("Root --> left --> right has the correct parent"); 

            if (root.left.right.value != 1)
                throw new ApplicationException("New Root --> left --> right --> value did not equal 1.");
            Console.WriteLine("New Root --> Left --> right has the correct value of 1");

            if (root.left.right.left.value != 7)
                throw new ApplicationException("New Root --> left --> right --> left --> value was not 7 as expected.");
            Console.WriteLine("New Root --> left --> right --> left.value had a value of 7 as expected");
            
            if (root.left.right.left.parent != root.left.right)
                throw new ApplicationException("New Root --> left --> right --> left --> parent was not root --> left --> right as expected");
            Console.WriteLine("New Root --> Left --> right --> left has the correct value of 7 and expected parent");

            Console.WriteLine("Value is: " + root.left.value);
            Console.WriteLine("Value is: " + root.left.right.value);
            Console.WriteLine("Root: " + a.getRoot().value);
            Console.WriteLine("Root: " + d.getRoot().value);
            
            Console.Read();
        }
    }
}
