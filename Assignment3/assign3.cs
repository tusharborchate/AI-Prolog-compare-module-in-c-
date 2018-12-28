
/// *    Note


// Compiler used -  C# compiler (csc)

//steps to execute program :

// 1 . Go to https://repl.it/repls/BlaringWaterloggedUnix

// 2.  Copy and paste all source code from assign3Borchate.cs

// 3. developer: tushar borchate/ 200393116 /306-529-7874
// This code contains all comments describing logic.

//4. please clear all code then copy and paste

//5. Try to use simple inputs :P

//6.Last update : 21/03/18 00 40
//////////////////////////////////////////////////////////////////////////

//  code starts here ////////




using System;
using System.Collections.Generic;
using System.Linq;

namespace PrologAssignment3
{
    //assign3prolog class
    public class PrologAssign3
    {
        static void Main(string[] args)
        {
            //create object of UnifyLogic class
            UnifyLogic unify = new UnifyLogic();

            //assignment no 3 prolog funtion
            Console.WriteLine("Assignment No : 3 | ?- unify_with_occurs_check");
            Console.WriteLine("Please enter first term press enter and then second term");

            //get two inputs from user i.e unifyterm1 and unifytem2
            unify.TermInput();

            Console.ReadLine();
        }


        //logic class which contains user input,solution and validation
        public class UnifyLogic
        {

            #region Models

            //Unify solution model to save solutios i.e valuesearch and valuereplace
            public class UnifySolutionModel
            {
                public string TermtoSearch { get; set; }
                public string TermtoReplace { get; set; }
            }

            //Unify tree model which contain all letters from each term
            public class UnifyTreeModel
            {
                public string Parent { get; set; }
                public string Position { get; set; }
                public int Cost { get; set; }
                public string Value { get; set; }
            }

            //Unify node model to search all nodes
            public class UnifyNodeModel
            {
                public string NodePosition { get; set; }
                public int NodeChildren { get; set; }
                public string NodeParent { get; set; }

            }

            #endregion

            #region variabledeclaration
            //dictionary which key= value of parent and value= no.of children parent has. .i.e "0":"2"
            public Dictionary<string, int> parentNodesList = new Dictionary<string, int>();

            //list contain all letters without "(" "," ")" 
            public List<UnifyTreeModel> treeModelList = new List<UnifyTreeModel>();

            //list contain all char
            public List<UnifyTreeModel> treeModelunfilterList = new List<UnifyTreeModel>();

            //list contains all solutions
            public List<UnifySolutionModel> solutionmodelList = new List<UnifySolutionModel>();

            //list contains all parent nodes
            public List<UnifyNodeModel> nodeModelList = new List<UnifyNodeModel>();

            // to track reason if output is no
            string resultreason = "";

            //term 1
            string unifyterm1;
            //term 2
            string unifyterm2;

            //values for constant,variable and function
            int defaultconstantValue = 1;
            int defaultvariableValue = 2;
            int defaultfunctionValue = 3;

            //track iteration no. in solution
            int numberofiteration = 0;
            #endregion

            #region termsinputfromuser
            //user input in two terms i.e unifyterm1 and unifyterm2
            public void TermInput()
            {
                //check the term type
                int unifyterm1Type = 0;
                int unifyterm2Type = 0;

                try
                {
                    //if result false then ask input again from user 
                    bool result = false;

                    while (!result)
                    {
                        //track iteration no. in solution
                        numberofiteration = 0;

                        //dictionary which key= value of parent and value= no.of children parent has. .i.e "0":"2"
                        parentNodesList = new Dictionary<string, int>();

                        //list contain all letters without "(" "," ")" 
                        treeModelList = new List<UnifyTreeModel>();

                        //list contain all char
                        treeModelunfilterList = new List<UnifyTreeModel>();


                        //list contains all solutions
                        solutionmodelList = new List<UnifySolutionModel>();

                        //list contains all parent nodes
                        nodeModelList = new List<UnifyNodeModel>();

                        //result if output no
                        resultreason = "";

                        //new line
                        Console.WriteLine(" ");
                        Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");

                        Console.WriteLine("Enter first term : ");
                        unifyterm1 = Console.ReadLine();

                        Console.WriteLine("Enter second term : ");
                        unifyterm2 = Console.ReadLine();

                        Console.WriteLine("----------------------------------------------");

                        unifyterm1Type = GetTermType(unifyterm1);
                        unifyterm2Type = GetTermType(unifyterm2);

                        //both terms are not valid
                        if (unifyterm1Type == 0 || unifyterm2Type == 0)
                        {
                            resultreason = "not valid terms";
                            Console.WriteLine("Output: no");
                            Console.WriteLine("Reason : " + resultreason);
                            result = false;
                            continue;
                        }
                        else
                        {
                            //both terms are valid
                            //check for solution validation
                            result = CheckTerm(unifyterm1Type, unifyterm2Type, unifyterm1, unifyterm2);
                        }


                        //creating tree for term1
                        bool treeResultforterm1 = GenerateBinaryTree(unifyterm1, "0");

                        //creating tree for term2
                        bool treeResultforterm2 = GenerateBinaryTree(unifyterm2, "0");

                        if (treeResultforterm1 && treeResultforterm2)
                        {
                            //both tree created then check validation 
                            bool treeresult = TreeValidationforBothTerms();

                            //tree contains invalid terms
                            if (!treeresult)
                            {
                                result = false;
                                Console.WriteLine("output : no ");
                                Console.WriteLine("Reason  : " + resultreason);
                                continue;
                            }

                        }


                        //check if current node has children nodes
                        int term1nodes = treeModelList.Where(a => a.Parent == "0.1").Count();
                        int term2nodes = treeModelList.Where(a => a.Parent == "0.2").Count();


                        //if termnodes1 is 0 then its variable no function
                        //if termnodes2 is 0 then its variable no function
                        UnifySolutionModel unifysolutionModel = new UnifySolutionModel();
                        if (term1nodes == 0 && (char.IsUpper(Convert.ToChar(unifyterm1))))
                        {
                            //validate both terms
                            if (CheckSolutionTerm(unifyterm1, unifyterm2))
                            {
                                unifysolutionModel.TermtoSearch = unifyterm1;
                                unifysolutionModel.TermtoReplace = unifyterm2;
                                solutionmodelList.Add(unifysolutionModel);
                                ShowSolution();
                                Console.WriteLine("output:Yes");
                                result = false;
                                continue;

                            }
                            else
                            {
                                Console.WriteLine("output : no");
                                Console.WriteLine("Reason : " + resultreason);
                                result = false;
                                continue;
                            }
                        }

                        if (term2nodes == 0)
                        {
                            //validate both terms
                            if (CheckSolutionTerm(unifyterm1, unifyterm2))
                            {
                                unifysolutionModel.TermtoSearch = unifyterm2;
                                unifysolutionModel.TermtoReplace = unifyterm1;
                                solutionmodelList.Add(unifysolutionModel);
                                ShowSolution();
                                result = false;
                                Console.WriteLine("output:Yes");
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("output : no");
                                Console.WriteLine("Reason : " + resultreason);
                                result = false;
                                continue;
                            }
                        }



                        //both terms contain function then compare both terms
                        if (UnificationnodeLogic("0.1"))
                        {
                            Console.WriteLine("output:yes");
                            result = false;
                        }
                        else
                        {
                            Console.WriteLine("output : no");
                            Console.WriteLine("Reason : " + resultreason);
                            result = false;
                        }

                    }
                }
                catch (Exception)
                {


                }
            }
            #endregion

            #region termtype
            public int GetTermType(string term)
            {

                try
                {
                    if (term == "")
                    {
                        return 0;
                    }
                    if (term.Contains("("))
                    {
                        return defaultfunctionValue;
                    }

                    char termchar = Convert.ToChar(term[0]);
                    if (Char.IsLower(termchar))
                    {
                        return defaultconstantValue;
                    }
                    else if (Char.IsUpper(termchar))
                    {
                        return defaultvariableValue;
                    }
                    else if (Char.IsNumber(termchar) && term.Length == 1)
                    {
                        return defaultconstantValue;
                    }
                    else
                    {
                        return 0;
                    }

                }
                catch (Exception)
                {
                    return 1;
                }
            }

            #endregion

            #region generate tree
            public bool GenerateBinaryTree(string term, string parentPosition)
            {
                try
                {
                    int parentNode = 0;
                    for (int i = 0; i < term.Length; i++)
                    {


                        UnifyTreeModel treeModel = new UnifyTreeModel();
                        treeModel.Parent = parentPosition;


                        char currentTerm = term[i];

                        treeModel.Value = currentTerm.ToString();
                        if (currentTerm == ')' || currentTerm == ',')
                        {
                            parentPosition = parentPosition.Substring(0, parentPosition.Length - 2);
                            parentNode = 0;
                            treeModel.Position = parentPosition + "K";
                            treeModel.Parent = "";
                            treeModelunfilterList.Add(treeModel);
                            continue;
                        }
                        else if (currentTerm == '(')
                        {
                            parentNode = 0;
                            treeModel.Position = parentPosition + "K";
                            treeModelunfilterList.Add(treeModel);

                            //parentNode = parentNode + 1;
                            continue;
                        }
                        else
                        {
                            parentNode = parentNode + 1;
                        }



                        char nextTerm = '0';
                        if (i != term.Length - 1)
                        {
                            nextTerm = term[i + 1];
                        }
                        if (char.IsLower(currentTerm) && nextTerm == '(')
                        {
                            //   Console.WriteLine("function");
                        }
                        int count = parentNodesList.Where(a => a.Key == parentPosition).Count();
                        if (count > 0)
                        {
                            count = parentNodesList.Where(a => a.Key == parentPosition).FirstOrDefault().Value;
                            parentNodesList.Remove(parentPosition);
                        }
                        parentNodesList.Add(parentPosition, (count + 1));
                        parentPosition = parentPosition + "." + (count + 1);
                        treeModel.Position = parentPosition;
                        treeModel.Cost = parentPosition.Replace(".", "").Length - 1;
                        treeModelunfilterList.Add(treeModel);
                        treeModelList.Add(treeModel);
                    }
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
            #endregion

            #region unificationLogic
            //unify nodes and get solution
            public bool UnificationnodeLogic(string unifyparentPosition)
            {

                try
                {
                    //check all nodes until unifyparentPosition = 0 from 0.1
                    while (unifyparentPosition != "0")
                    {
                        //get count of all children where parent=unifyparentPosition
                        int childrenCount = treeModelList.Where(a => a.Parent == unifyparentPosition).Count();

                        //if count==0 then its last node no more node its variable or its constant
                        if (childrenCount == 0)
                        {
                            //get current node position i.e 0.1.1
                            string positionterm1 = unifyparentPosition;

                            //get current node position i.e 0.2.1
                            string positionterm2 = "0.2" + unifyparentPosition.Substring(3);

                            //max level for term1 and term2
                            int maxlevelforterm1 = 0, maxlevelforterm2 = 0;

                            //get no. of children for term1 
                            maxlevelforterm1 = treeModelList.Where(a => a.Parent == unifyparentPosition).Count();

                            //get no. of children for term2
                            maxlevelforterm2 = treeModelList.Where(a => a.Parent == unifyparentPosition).Count();

                            //to make change in positionterm1
                            string newterm1 = positionterm1;

                            //to make change in positionterm2
                            string newterm2 = "";

                            //to check which term has more nodes
                            bool maxnoderesult = false;

                            newterm2 = positionterm2;

                            //check if term2 has same position of node if no node then term1 has more variable
                            UnifyTreeModel treeModelnodes = new UnifyTreeModel();


                            treeModelnodes = treeModelList.Where(a => a.Position == newterm2).FirstOrDefault();
                            if (treeModelnodes == null)
                            {
                                while (!maxnoderesult)
                                {
                                    treeModelnodes = treeModelList.Where(a => a.Position == newterm2).FirstOrDefault();

                                    // cant found node for term2 for specific position
                                    if (treeModelnodes == null)
                                    {
                                        //term 1 has more node
                                        maxlevelforterm1 = 1;
                                        maxlevelforterm2 = 0;

                                        //get term2 max node by reducing position  i.e 0.1.1  no 0.2.1 then for 0.2 node
                                        newterm2 = newterm2.Substring(0, newterm2.Length - 2);

                                    }
                                    else
                                    {
                                        //if 0.2.1 node there then newterm1=0.1.1 from 0.1.1.1
                                        maxnoderesult = true;
                                        newterm1 = "0.1" + newterm2.Substring(3);
                                    }
                                }
                            }

                            //check if term2 has more node than term1

                            if (treeModelList.Where(a => a.Position == positionterm2) != null && treeModelList.Where(a => a.Parent == positionterm2).ToList().Count() > 0)
                            {
                                maxlevelforterm2 = 1;
                                maxlevelforterm1 = 0;
                                newterm2 = "0.2" + newterm1.Substring(3);
                                //term 2 has more node
                            }


                            UnifySolutionModel solutionModel = new UnifySolutionModel();
                            if (maxlevelforterm1 > maxlevelforterm2)
                            {
                                //term1 has more nodes then take all letter from list for term1
                                string valuereplace = "";
                                bool result2 = false;

                                //get all letters from list which contain newterm1
                                foreach (var item in treeModelunfilterList)
                                {

                                    if (item.Position == newterm1)
                                    {
                                        result2 = true;

                                    }
                                    if ((result2 && item.Position.Contains(newterm1)))
                                    {
                                        valuereplace = valuereplace + item.Value;
                                    }

                                    if (result2 && (!item.Position.Contains(newterm1)))
                                    {
                                        break;
                                    }
                                }

                                int maxcountob = 0;
                                int maxcountcb = 0;

                                foreach (var item in valuereplace)
                                {
                                    if (item.ToString() == "(")
                                    {
                                        maxcountob = maxcountob + 1;
                                    }
                                    if (item.ToString() == ")")
                                    {
                                        maxcountcb = maxcountcb + 1;
                                    }

                                }

                                if (maxcountob > maxcountcb)
                                {
                                    int diff = maxcountob - maxcountcb;
                                    for (int cb = 0; cb < diff; cb++)
                                    {
                                        valuereplace = valuereplace + ")";
                                    }
                                }

                                solutionModel = new UnifySolutionModel();
                                solutionModel.TermtoSearch = treeModelList.Where(a => a.Position == newterm2).FirstOrDefault().Value;
                                solutionModel.TermtoReplace = valuereplace;

                                bool solutionvalidation = CheckSolution(newterm1, newterm2, valuereplace, solutionModel.TermtoSearch);
                                if (!solutionvalidation || !CheckSolutionTerm(solutionModel.TermtoSearch, solutionModel.TermtoReplace))
                                {
                                    return false;
                                }
                                foreach (var item in treeModelunfilterList)
                                {
                                    if (item.Value.Contains(solutionModel.TermtoSearch))
                                    {
                                        item.Value = solutionModel.TermtoReplace;
                                    }
                                    if (item.Value == (solutionModel.TermtoSearch))
                                    {
                                        item.Value = solutionModel.TermtoReplace;
                                    }
                                }
                                solutionmodelList.Add(solutionModel);




                            }
                            else if (maxlevelforterm1 < maxlevelforterm2)
                            {

                                //term2 has more nodes
                                string valuereplace = "";
                                bool result2 = false;


                                //get all nodes which in list
                                foreach (var item in treeModelunfilterList)
                                {

                                    if (item.Position == newterm2)
                                    {
                                        result2 = true;

                                    }
                                    if ((result2 && item.Position.Contains(newterm2)))
                                    {
                                        valuereplace = valuereplace + item.Value;
                                    }

                                    if (result2 && (!item.Position.Contains(newterm2)))
                                    {
                                        break;
                                    }
                                }

                                int maxcountob = 0;
                                int maxcountcb = 0;

                                foreach (var item in valuereplace)
                                {
                                    if (item.ToString() == "(")
                                    {
                                        maxcountob = maxcountob + 1;
                                    }
                                    if (item.ToString() == ")")
                                    {
                                        maxcountcb = maxcountcb + 1;
                                    }

                                }

                                if (maxcountob > maxcountcb)
                                {
                                    int diff = maxcountob - maxcountcb;
                                    for (int cb = 0; cb < diff; cb++)
                                    {
                                        valuereplace = valuereplace + ")";
                                    }
                                }

                                solutionModel = new UnifySolutionModel();
                                solutionModel.TermtoSearch = treeModelList.Where(a => a.Position == newterm1).FirstOrDefault().Value;
                                solutionModel.TermtoReplace = valuereplace;
                                bool solutionvalidation = CheckSolution(newterm1, newterm2, solutionModel.TermtoSearch, valuereplace);
                                if (!solutionvalidation || !CheckSolutionTerm(solutionModel.TermtoSearch, solutionModel.TermtoReplace))
                                {
                                    return false;
                                }


                                foreach (var item in treeModelunfilterList)
                                {
                                    if (item.Value.Contains(solutionModel.TermtoSearch))
                                    {
                                        item.Value = solutionModel.TermtoReplace;
                                    }
                                    if (item.Value == (solutionModel.TermtoSearch))
                                    {
                                        item.Value = solutionModel.TermtoReplace;
                                    }
                                }
                                solutionmodelList.Add(solutionModel);

                            }
                            else
                            {

                            }

                            if (maxlevelforterm1 == maxlevelforterm2)
                            {
                                //both are function then have same name
                                string term1parent = treeModelList.Where(a => a.Position == newterm1).FirstOrDefault().Parent;
                                string term2parent = treeModelList.Where(a => a.Position == newterm2).FirstOrDefault().Parent;

                                int term1parentcount = treeModelList.Where(a => a.Parent == term1parent).Count();
                                int term2parentcount = treeModelList.Where(a => a.Parent == term2parent).Count();

                                if (term1parentcount != term2parentcount)
                                {
                                    resultreason = "both terms has no same number of nodes.";
                                    return false;
                                }

                                solutionModel = new UnifySolutionModel();
                                solutionModel.TermtoReplace = treeModelList.Where(a => a.Position == newterm1).FirstOrDefault().Value;
                                solutionModel.TermtoSearch = treeModelList.Where(a => a.Position == newterm2).FirstOrDefault().Value;
                                if (char.IsLower(Convert.ToChar(solutionModel.TermtoSearch)))
                                {
                                    solutionModel.TermtoSearch = treeModelList.Where(a => a.Position == newterm1).FirstOrDefault().Value;
                                    solutionModel.TermtoReplace = treeModelList.Where(a => a.Position == newterm2).FirstOrDefault().Value;
                                }

                                bool solutionvalidation = CheckSolution(newterm1, newterm2, solutionModel.TermtoSearch, solutionModel.TermtoReplace);
                                if (!solutionvalidation || !CheckSolutionTerm(solutionModel.TermtoSearch, solutionModel.TermtoReplace))
                                {
                                    return false;
                                }
                                foreach (var item in treeModelunfilterList)
                                {
                                    if (item.Value.Contains(solutionModel.TermtoSearch))
                                    {
                                        item.Value = solutionModel.TermtoReplace;
                                    }
                                    if (item.Value == (solutionModel.TermtoSearch))
                                    {
                                        item.Value = solutionModel.TermtoReplace;
                                    }
                                }
                                solutionmodelList.Add(solutionModel);


                                foreach (var solution in solutionmodelList)
                                {
                                    foreach (var item in treeModelList)
                                    {
                                        if (item.Value == solution.TermtoSearch)
                                        {
                                            item.Value = solution.TermtoReplace;
                                        }
                                    }
                                }


                            }

                            ReplaceSolutionTerm();
                            numberofiteration = numberofiteration + 1;
                            for (int tree = 0; tree < solutionmodelList.Count; tree++)
                            {
                                if (!string.IsNullOrEmpty(solutionmodelList[tree].TermtoSearch))
                                {
                                    Console.WriteLine("[ iteration - " + numberofiteration + " ] " + solutionmodelList[tree].TermtoSearch + "=" + solutionmodelList[tree].TermtoReplace);
                                    Console.WriteLine("----------------------------------------------------------------");

                                }

                            }
                            string term1 = "";
                            string term2 = "";

                            foreach (var item in treeModelunfilterList)
                            {
                                if (item.Position.Contains("0.1"))
                                {
                                    term1 = term1 + item.Value;
                                }
                                else
                                {
                                    term2 = term2 + item.Value;
                                }
                            }
                            treeModelList = new List<UnifyTreeModel>();
                            treeModelunfilterList = new List<UnifyTreeModel>();
                            parentNodesList = new Dictionary<string, int>();

                            GenerateBinaryTree(term1, "0");
                            GenerateBinaryTree(term2, "0");

                            // last children
                            unifyparentPosition = unifyparentPosition.Substring(0, unifyparentPosition.Length - 2);
                        }
                        else //no last node
                        {
                            List<UnifyTreeModel> treeList = new List<UnifyTreeModel>();
                            treeList = treeModelList.Where(a => a.Parent == unifyparentPosition).ToList();
                            int nodes = 0;
                            int countno = 0;
                            foreach (var item in treeList) // save to nodemodel list that node searched
                            {
                                nodes = nodeModelList.Where(a => a.NodePosition == item.Position).Count();
                                if (nodes == 0)
                                {
                                    UnifyNodeModel nodemodel = new UnifyNodeModel();
                                    nodemodel.NodePosition = item.Position;
                                    nodemodel.NodeParent = item.Parent;
                                    nodeModelList.Add(nodemodel);
                                    countno = countno + 1;
                                    unifyparentPosition = nodemodel.NodePosition;
                                    break;
                                }
                            }
                            if (countno == 0)
                            {
                                unifyparentPosition = unifyparentPosition.Substring(0, unifyparentPosition.Length - 2);

                            }
                        }
                    }

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            #endregion

            #region TermValidation
            public bool CheckTerm(int term1Type, int term2Type, string term1, string term2)
            {
                try
                {

                    if ((term1Type == 1 && term2Type == 1) && (term1 != term2))
                    {
                        return false;
                    }
                    if ((term1Type == 3 && term2Type == 1) || (term1Type == 1 && term2Type == 3))
                    {
                        return false;
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public bool TreeValidationforBothTerms()
            {
                try
                {
                    //rule 1 - if function then in lowercase rule 2 if variable then uppercase
                    foreach (var item in treeModelList)
                    {
                        int count = treeModelList.Where(a => a.Parent == item.Position).Count();
                        if (count > 0 && !Char.IsLower(Convert.ToChar(item.Value)))
                        {
                            resultreason = "Function name in uppercase.Note: change it to lowercase";
                            return false;
                        }

                    }

                    int obcount = treeModelunfilterList.Where(a => a.Value == "(").Count();
                    int cbcount = treeModelunfilterList.Where(a => a.Value == ")").Count();
                    if (obcount != cbcount)
                    {
                        resultreason = "number of open and close brackets are not matching.Note: check your term again.";

                        return false;
                    }

                    return true;
                }
                catch (Exception)
                {
                    return false;

                }
            }

            public bool CheckSolution(string newterm1, string newterm2, string newterm1value, string newterm2value)
            {
                try
                {
                    // if both function has  same pareamter
                    string term1parent = treeModelList.Where(a => a.Position == newterm1).FirstOrDefault().Parent;
                    string term2parent = treeModelList.Where(a => a.Position == newterm2).FirstOrDefault().Parent;
                    string term1parentValue = ""; string term2parentValue = "";
                    int term1parentcount = treeModelList.Where(a => a.Parent == term1parent).Count();
                    int term2parentcount = treeModelList.Where(a => a.Parent == term2parent).Count();
                    if (treeModelList.Where(a => a.Position == term1parent).ToList().Count != 0)
                    {
                        term1parentValue = treeModelList.Where(a => a.Position == term1parent).FirstOrDefault().Value;
                        term2parentValue = treeModelList.Where(a => a.Position == term2parent).FirstOrDefault().Value;
                    }

                    //no of nodes same
                    if (term1parentcount != term2parentcount)
                    {
                        resultreason = "Number of parameter are not matching.Note: please check term";
                        return false;
                    }

                    //solution contain variable
                    if ((newterm1value.Length == 1 && !char.IsUpper(Convert.ToChar(newterm1value.ToString()))) &&
                        newterm2value.Length == 1 && !char.IsUpper(Convert.ToChar(newterm2value.ToString())) && newterm1value!=newterm2value)
                    {
                        resultreason = "No variable in both position.Note:change term.";
                        return false;
                    }

                    if (newterm2value.Length > 1 && newterm1value.Length > 1)
                    {
                        return false;
                    }

                    //name of parent node same
                    if (term1parentValue != term2parentValue)
                    {
                        resultreason = "Both terms have different function name.Note:Change term";
                        return false;
                    }
                    string position = "";
                    string position2 = "";
                    position = term1parent;
                    position2 = term2parent;
                    while (position != "0")
                    {

                        if (treeModelList.Where(a => a.Position == position).FirstOrDefault().Value != treeModelList.Where(a => a.Position == position2).FirstOrDefault().Value)
                        {
                            resultreason = "Both terms have different function name.Note:Change term";
                            return false;
                        }
                        position = position.Substring(0, position.Length - 2);
                        position2 = position2.Substring(0, position2.Length - 2);

                    }
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }

            public bool CheckSolutionTerm(string term1, string term2)
            {
                try
                {
                    if ((term1.Length == 1 && !char.IsUpper(Convert.ToChar(term1.ToString()))) &&
                       term2.Length == 1 && !char.IsUpper(Convert.ToChar(term2.ToString())) && term1!=term2)
                    {

                        resultreason = "No variable in both terms.";
                        return false;
                    }

                    if (term1.Length == 1 && term2.Contains(term1) && term2.Length > 1)
                    {
                        resultreason = "term2 contain term1.";

                        return false;
                    }
                    if (term2.Length == 1 && term1.Contains(term2) && term1.Length > 1)
                    {
                        resultreason = "term1 contain term2.";

                        return false;
                    }
                    if ((term1.Length == 1 && !char.IsUpper(Convert.ToChar(term1.ToString()))) && term2.Length > 1)
                    {
                        resultreason = "No variable to assign.";

                        return false;
                    }
                    if ((term2.Length == 1 && !char.IsUpper(Convert.ToChar(term2.ToString()))) && term1.Length > 1)
                    {
                        resultreason = "No variable to assign.";

                        return false;
                    }



                    return true;

                }
                catch (Exception)
                {
                    return false;

                }
            }

            public void ReplaceSolutionTerm()
            {
                List<UnifySolutionModel> solutionmodellist1 = new List<UnifySolutionModel>();
                try
                {
                    solutionmodellist1 = solutionmodelList;
                    foreach (var term in solutionmodelList)
                    {
                        string currentValueTosearch = term.TermtoSearch;
                        string currentValuetoreplace = term.TermtoReplace;
                        foreach (var solution in solutionmodellist1)
                        {
                            if (solution.TermtoReplace == currentValueTosearch)
                            {
                                solution.TermtoReplace = currentValuetoreplace;
                            }
                            else
                            {
                                if (solution.TermtoReplace.Contains(currentValueTosearch))
                                {
                                    solution.TermtoReplace = solution.TermtoReplace.Replace(currentValueTosearch, currentValuetoreplace);
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {


                }
            }

            public void ShowSolution()
            {
                try
                {
                    for (int i = 0; i < solutionmodelList.Count; i++)
                    {
                        Console.WriteLine("iteration no: " + (i + 1) + " : " + solutionmodelList[i].TermtoSearch + "=" + solutionmodelList[i].TermtoReplace);
                    }
                }
                catch (Exception)
                {


                }
            }

            #endregion


        }

    }
}
