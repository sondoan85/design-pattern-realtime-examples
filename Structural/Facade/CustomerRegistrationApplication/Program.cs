namespace CustomerRegistrationApplication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public enum OperationType
    {
        Append,
        Delete,
        Print,
        Undo
    };
    public class Operation
    {
        public Operation(string opString)
        {
            string[] parsedOp = opString.Split(' ');
            OpType = (OperationType)int.Parse(parsedOp[0]) - 1;
            switch (OpType)
            {
                case OperationType.Append:
                    StringParameter = parsedOp[1].ToCharArray();
                    IntParameter = parsedOp[1].Length;
                    break;
                case OperationType.Delete:
                case OperationType.Print:
                    IntParameter = int.Parse(parsedOp[1]);
                    break;
                default: break;
            }
        }
        /*const static int APPEND = 1;
        const static int DELETE = 2;
        const static int PRINT = 3;
        const static int UNDO = 4;*/

        public OperationType OpType { get; set; }
        public char[] StringParameter { get; set; }
        public int IntParameter { get; set; }
        public bool HasBeenPerformed { get; set; } = false;
    }
    class Solution
    {
        static List<Operation> opStack = new List<Operation>();
        static char[] S = new char[1000000];
        static int stringLength = 0;
        static int opsPerformed = 0;
        static List<string> strToPrint = new List<string>();
        static void PerformList()
        {
            for (int i = opsPerformed; i < opStack.Count; i++)
            {
                var op = opStack[i];
                if (op.HasBeenPerformed) continue;
                switch (op.OpType)
                {
                    case OperationType.Append:
                        for (int j = 0; j < op.StringParameter.Length; j++)
                        {
                            S[stringLength++] = op.StringParameter[j];
                        }
                        break;
                    case OperationType.Delete:
                        int numCharsToDelete = op.IntParameter;
                        stringLength -= numCharsToDelete;
                        char[] stringDeleted = new char[numCharsToDelete];
                        for (int j = 0; j < numCharsToDelete; j++)
                        {
                            stringDeleted[j] = S[stringLength + j];
                        }
                        op.StringParameter = stringDeleted;
                        break;
                    default:
                        break;
                }
                op.HasBeenPerformed = true;
                opsPerformed++;
            }
        }
        static void Main(String[] args)
        {
            int opCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < opCount; i++)
            {
                var op = new Operation(Console.ReadLine());
                switch (op.OpType)
                {
                    case OperationType.Append:
                    case OperationType.Delete:
                        opStack.Add(op);
                        break;
                    case OperationType.Print:
                        PerformList();
                        strToPrint.Add(S[op.IntParameter - 1].ToString());
                        //strToPrint.Add("\n");
                        //Console.WriteLine(S[op.IntParameter-1]);
                        break;
                    case OperationType.Undo:
                        /*Console.Write("Before undo:");
                        for (int q=0;q<stringLength;q++)
                        {
                            Console.Write(S[q].ToString());
                        }
                        Console.WriteLine();*/
                        Operation opToUndo = opStack.Last();
                        if (opToUndo.HasBeenPerformed)
                        {
                            if (opToUndo.OpType == OperationType.Append)
                            {
                                //Console.WriteLine($"undoing append of length {parsedOpToUndo[1]}");
                                stringLength -= opToUndo.IntParameter;
                            }
                            else
                            {
                                char[] charsToReappend = opToUndo.StringParameter;
                                /*Console.Write("Reappending: ");
                                for (int q=0;q<charsToReappend.Length;q++)
                                {
                                    Console.Write(charsToReappend[q].ToString());
                                }
                                Console.WriteLine();*/
                                charsToReappend.CopyTo(S, stringLength);
                                stringLength += charsToReappend.Count();
                            }
                            opsPerformed--;
                            /*Console.Write("After undo:");
                            for (int q=0;q<stringLength;q++)
                            {
                                Console.Write(S[q].ToString());
                            }
                            Console.WriteLine();*/
                        }
                        opStack.RemoveAt(opStack.Count - 1);
                        break;
                    default: throw new Exception("Invalid operation");

                }

            }
            Console.WriteLine(String.Join("\n", strToPrint));

        }
    }
}
