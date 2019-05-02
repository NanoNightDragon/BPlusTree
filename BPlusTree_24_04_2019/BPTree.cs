using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPlusTree_24_04_2019
{
    class Node
    {
        public bool IsLeaf;

        public int key_num;   // количество ключей узла
        public List<int> key; // The list of keys 

   
        public List<Node> children; 
        public Node parent;

        public List<int> data;


        //
        //public Node left; public Node right;




    }

    class BPlusTree
    {
        public int MDegree;        // минимальная степень дерева
        public Node Root;  // указатель на корень дерева

        public BPlusTree(){
            Root = null;
            MDegree = 3;
        }


        public bool insert(int value)
        {
            if (Root == null)
            {
                Root = new Node();
                Root.IsLeaf = true;
                Root.parent = null;
                Root.key_num = 1;
                Root.data = new List<int>((MDegree+1));
                Root.data.Add(value);

            }
            else
            {

                if (FindE(value, Root))
                    return false;
                //if Root not null 
                if (Root.IsLeaf)
                {
                    //!!!!!!!!!add new to leaf    add THEN SORT in leaf!!!!!!!!!!!!!!
                    Root.key_num += 1;
                    Root.data.Add(value);
                    Root.data.Sort();
                    if (Root.key_num > MDegree)
                    {
                        
                    
                        Root.children = new List<Node>(MDegree+1) {new Node(),new Node() };
                        for (int i = 0; i < 2; i++)
                        {
                            Root.children[i].parent = Root;
                            Root.children[i].IsLeaf = true;
                            Root.children[i].key_num = 2;
                            Root.children[i].data = new List<int>((MDegree + 1));
                            for (int j = 0; j < (int)Math.Ceiling((double)MDegree / 2); j++)
                            {
                                
                                Root.children[i].data.Add (Root.data[2*i+j]);
                            }
                        }
                        Root.key = new List<int>((MDegree + 1));
                        Root.key.Add(Root.data[0]);
                        Root.key.Add(Root.data[(MDegree + 1) / 2]);
                        Root.data = null;
                        Root.key_num = 2;
                        Root.IsLeaf = false;

                    }
                }
                else
                {

                    //if root not leaf
                    Node L_LEAF = FindNode(value, Root);
                    int current_item_index = 0;
                    int Pref_V = L_LEAF.data[0];
                    for (int i = 0; i < L_LEAF.parent.key_num; i++)
                    {
                        if (L_LEAF.parent.key[i] == Pref_V)
                        {
                            current_item_index = i;
                            break;
                        }
                    }

                    //new FFF
                    L_LEAF.key_num += 1;
                    L_LEAF.data.Add(value);
                    L_LEAF.data.Sort();
                    //change all key
                    
                    L_LEAF.parent.key[current_item_index] = L_LEAF.data[0];
                    Node L_s = L_LEAF.parent;
                    



                    
                    while (L_s.parent != null && L_LEAF.parent.key[current_item_index] != Pref_V && current_item_index == 0)
                    {
                        if (L_s.parent.key[0] == Pref_V)
                        {
                            L_s.parent.key[0] = L_s.key[0];
                            L_s = L_s.parent;
                               
                        }
                    }

                    
                    if(L_LEAF.key_num > MDegree)
                    {
                        //if free space in parent add new leaf to parent
                        L_LEAF.parent.key_num += 1;
                        L_LEAF.parent.key.Add(L_LEAF.data[Convert.ToInt32(Math.Ceiling(Convert.ToDouble(MDegree)/2))]);
                        L_LEAF.parent.children.Add(new Node());
                        L_LEAF.parent.children.Last().parent = L_LEAF.parent;
                        L_LEAF.parent.children.Last().key_num = (int)Math.Ceiling( (double)MDegree / 2);
                        L_LEAF.parent.children.Last().data = new List<int>(MDegree + 1);
                        for(int i= (int)Math.Ceiling((double)MDegree / 2); i< MDegree +1;i++)
                        {
                            L_LEAF.parent.children.Last().data.Add(L_LEAF.data[i]);
                        }
                        L_LEAF.parent.children.Last().IsLeaf = true;
                        //end of add 

                        L_LEAF.key_num = (int)Math.Ceiling((double)MDegree / 2);
                        List<int> l_list = L_LEAF.data;
                        L_LEAF.data = new List<int>(L_LEAF.key_num);
                        for (int i = 0; i < L_LEAF.key_num; i++)
                            L_LEAF.data.Add(l_list[i]);

                        L_LEAF.parent.key[current_item_index] = L_LEAF.data[0];

                        //change key if needed
                        for(int i = 1; i < MDegree;i++)
                        {
                            if(L_LEAF.parent.key[i] > L_LEAF.parent.children.Last().data[0])
                            {
                                int t = L_LEAF.parent.key[i];
                                L_LEAF.parent.key[i] = L_LEAF.parent.key[L_LEAF.parent.key_num - 1];
                                L_LEAF.parent.key[L_LEAF.parent.key_num - 1] = t;
                                Node T = L_LEAF.parent.children[i];
                                L_LEAF.parent.children[i] = L_LEAF.parent.children[L_LEAF.parent.key_num - 1];
                                L_LEAF.parent.children[L_LEAF.parent.key_num - 1] = T;

                               
                            }


                        }

                        //if Parent.key_num > 3(MD) 
                        if(L_LEAF.parent.key_num > MDegree)
                        {
                            InsertIntoT(L_LEAF.parent);

                        }
                        







                    }






                    //end of new FFF






                }
            }


            return true;
        }


        private Node InsertIntoT(Node node)
        {
            if (node == Root)
            {
                Node NewRoot = new Node();
                NewRoot.IsLeaf = false;
                NewRoot.key_num = 2;
                NewRoot.key = new List<int>(MDegree + 1);
                NewRoot.parent = null;
                NewRoot.children = new List<Node>(MDegree + 1);
                for (int i = 0; i < 2; i++)
                {
                    NewRoot.children.Add(new Node());
                    NewRoot.children[i].key_num = (int)Math.Ceiling((double)MDegree / 2);
                    NewRoot.children[i].key = new List<int>(MDegree + 1);
                    NewRoot.children[i].children = new List<Node>(MDegree + 1);
                    for (int j = 0; j < NewRoot.children[i].key_num; j++)
                    {
                        NewRoot.children[i].key.Add(Root.key[2 * i + j]);
                        NewRoot.children[i].children.Add(Root.children[2 * i + j]);
                        NewRoot.children[i].children[j].parent = NewRoot.children[i];
                    }

                    NewRoot.key.Add(NewRoot.children[i].key[0]);
                    NewRoot.children[i].parent = NewRoot;
                }
                Root = NewRoot;




            }
            else
            {
                int current_item_index = 0;
                for (int i = 0; i < node.parent.key_num; i++)
                {
                    if (node.parent.key[i] == node.key[0])
                    {
                        current_item_index = i;
                        break;

                    }

                }


                //if free space in parent add new leaf to parent
                node.parent.key_num += 1;
                node.parent.key.Add(node.key[(int)(Math.Ceiling((double)(MDegree) / 2))]);
                node.parent.children.Add(new Node());
                node.parent.children.Last().parent = node.parent;
                node.parent.children.Last().key_num = (int)Math.Ceiling((double)MDegree / 2);
                node.parent.children.Last().key = new List<int>(MDegree + 1);
                node.parent.children.Last().children = new List<Node>(MDegree + 1);
                for (int i = (int)Math.Ceiling((double)MDegree / 2); i < MDegree + 1; i++)
                {
                    node.parent.children.Last().key.Add(node.key[i]);
                    node.parent.children.Last().children.Add(node.children[i]);
                    node.parent.children.Last().children.Last().parent = node.parent.children.Last();
                }
                node.parent.children.Last().IsLeaf = false;
                //end of add 

                node.key_num = (int)Math.Ceiling((double)MDegree / 2);
                List<int> l_list = node.key;
                List<Node> l_node = node.children;
                node.key = new List<int>(node.key_num);
                node.children = new List<Node>(node.key_num);
                for (int i = 0; i < node.key_num; i++)
                { 
                node.key.Add(l_list[i]);
                node.children.Add(l_node[i]);
                node.children[i].parent = node;

    ;           }

                node.parent.key[current_item_index] = node.key[0];

                //change key if needed
                for (int i = 1; i < MDegree; i++)
                {
                    if (node.parent.key[i] > node.parent.children.Last().key[0])
                    {
                        int t = node.parent.key[i];
                        node.parent.key[i] = node.parent.key[node.parent.key_num - 1];
                        node.parent.key[node.parent.key_num - 1] = t;
                        Node T = node.parent.children[i];
                        node.parent.children[i] = node.parent.children[node.parent.key_num - 1];
                        node.parent.children[node.parent.key_num - 1] = T;

                        
                    }


                }
                if (node.parent.key_num > MDegree)
                {
                    InsertIntoT(node.parent);

                }




            }


            return null;
        }

        

        public Node FindNode(int value, Node node)
        {
            if (node.IsLeaf)
                return node;
            for (int i = 1; i < node.key_num; i++)
            {
                if (node.key[i] > value)
                {
                    return FindNode(value, node.children[i - 1]);
                }
            }
            return FindNode(value, node.children[node.key_num - 1]);
            
        }


        public bool FindE(int value, Node node)
        {
            //Node NewRoot = Root;
            if(node.IsLeaf)
            {
                for(int i = 0;i<node.key_num;i++)
                {
                    if (node.data[i] > value)
                        return false;
                    else
                    {
                        if (node.data[i] == value)
                            return true;
                    }
                    
                }
            }
            else
            {
                for(int i = 1;i < node.key_num;i++)
                {
                    if (node.key[i] > value)
                    {
                        return FindE(value, node.children[i-1]);
                    }
                }
                return FindE(value, node.children[node.key_num-1]);
                //return false;

            }


            return false;
        }


        public List<int> Get_all_data(int size)
        {
            List<int> L_List_data = new List<int>(size);
            OneStep(Root, L_List_data);

            return L_List_data;
        }

        public void OneStep(Node node,List<int> L_arr)
        {
            if(node.IsLeaf)
            {
                L_arr.AddRange(node.data);
            }else
            {
                for(int i =0;i< node.key_num;i++)
                     OneStep(node.children[i],L_arr);

            }


        }




    }
   
}
