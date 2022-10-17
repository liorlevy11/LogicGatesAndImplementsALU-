using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A bitwise gate takes as input WireSets containing n wires, and computes a bitwise function - z_i=f(x_i)
    class BitwiseMux : BitwiseTwoInputGate
    {
        public Wire ControlInput { get; private set; }

        //your code here

        private MuxGate[] m_gmuxArray;

        public BitwiseMux(int iSize)
            : base(iSize)
        {
            ControlInput = new Wire();
            m_gmuxArray = new MuxGate[iSize];
            for (int i = 0; i < iSize; i++)
            {
                m_gmuxArray[i] = new MuxGate();
                m_gmuxArray[i].ConnectInput1(Input1[i]);
                m_gmuxArray[i].ConnectInput2(Input2[i]);
                m_gmuxArray[i].ConnectControl(ControlInput);
                Output[i].ConnectInput(m_gmuxArray[i].Output);

            }


        }

        public void ConnectControl(Wire wControl)
        {
            ControlInput.ConnectInput(wControl);
        }



        public override string ToString()
        {
            return "Mux " + Input1 + "," + Input2 + ",C" + ControlInput.Value + " -> " + Output;
        }




        public override bool TestGate()
        {
            ControlInput.Value = 0;
            for (int i = 0; i < Output.Size; i++)
            {
                Input1[i].Value = 0;
                Input2[i].Value = 1;
            }
            for (int i = 0; i < Output.Size; i++)
            {
                if (Output[i].Value != 0)
                    return false;
            }

            ControlInput.Value = 1;
            for (int i = 0; i < Output.Size; i++)
            {
                Input1[i].Value = 0;
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
