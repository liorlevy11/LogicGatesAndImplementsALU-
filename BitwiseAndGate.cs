using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A two input bitwise gate takes as input two WireSets containing n wires, and computes a bitwise function - z_i=f(x_i,y_i)
    class BitwiseAndGate : BitwiseTwoInputGate
    {
        //your code here
        private AndGate[] m_gAndArray;
     

        public BitwiseAndGate(int iSize)
            : base(iSize)

        {
            m_gAndArray = new AndGate[iSize];
            for(int i=0;i<iSize;i++)
            {
                m_gAndArray[i] = new AndGate();
                m_gAndArray[i].ConnectInput1(Input1[i]);
                m_gAndArray[i].ConnectInput2(Input2[i]);
                Output[i].ConnectInput(m_gAndArray[i].Output);

            }
        }

        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(and)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "And " + Input1 + ", " + Input2 + " -> " + Output;
        }

        public override bool TestGate()
        {
            for (int i = 0; i < Output.Size; i++)
            {
                Input1[i].Value = 0;
                Input2[i].Value = 0;
            }
            for (int i = 0; i < Output.Size; i++)
            {
                if (Output[i].Value != 0)
                    return false;
            }

            for (int i = 0; i < Output.Size; i++)
            {
                Input1[i].Value = 1;
                Input2[i].Value = 1;
            }
            for (int i = 0; i < Output.Size; i++)
            {
                if (Output[i].Value != 1)
                    return false;
            }


            return true;
        }
    }
}
