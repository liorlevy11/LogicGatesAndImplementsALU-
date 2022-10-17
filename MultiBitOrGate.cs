using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //Multibit gates take as input k bits, and compute a function over all bits - z=f(x_0,x_1,...,x_k)

    class MultiBitOrGate : MultiBitGate
    {
        private OrGate[] m_gOrArray;
        //your code here

        public MultiBitOrGate(int iInputCount)
            : base(iInputCount)
        {
            //your code here
            m_gOrArray = new OrGate[iInputCount - 1];
            //m_wsInput = new WireSet(iInputCount);
            m_gOrArray[0] = new OrGate();
            m_gOrArray[0].ConnectInput1(m_wsInput[0]);
            m_gOrArray[0].ConnectInput2(m_wsInput[1]);
            for (int i = 1; i < iInputCount - 1; i++)
            {
                m_gOrArray[i] = new OrGate();
                m_gOrArray[i].ConnectInput1(m_gOrArray[i - 1].Output);
                m_gOrArray[i].ConnectInput2(m_wsInput[i + 1]);
            }
            Output.ConnectInput(m_gOrArray[m_gOrArray.Length - 1].Output);


        }

        public override bool TestGate()
        {
            for (int i = 0; i < m_wsInput.Size; i++)
            {
                m_wsInput[i].Value = 0;
            }
            if (Output.Value != 0)
                return false;


            for (int i = 0; i < m_wsInput.Size; i++)
            {
                m_wsInput[i].Value = 1;
            }
            if (Output.Value != 1)
                return false;
            return true;
        }
    }
}
