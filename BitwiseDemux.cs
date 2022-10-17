using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A bitwise gate takes as input WireSets containing n wires, and computes a bitwise function - z_i=f(x_i)
    class BitwiseDemux : Gate
    {
        public int Size { get; private set; }
        public WireSet Output1 { get; private set; }
        public WireSet Output2 { get; private set; }
        public WireSet Input { get; private set; }
        public Wire Control { get; private set; }
        public Demux[] m_gDemux;

        //your code here

        public BitwiseDemux(int iSize)
        {
            Size = iSize;
            Control = new Wire();
            Output1 = new WireSet(Size);
            Output2 = new WireSet(Size);
            Input = new WireSet(Size);
            m_gDemux = new Demux[Size];
            for (int i = 0; i < Size; i++)
            {
                m_gDemux[i] = new Demux();
                m_gDemux[i].ConnectInput(Input[i]);
                m_gDemux[i].ConnectControl(Control);
                Output1[i].ConnectInput(m_gDemux[i].Output1);
                Output2[i].ConnectInput(m_gDemux[i].Output2);

            }

        }

        public void ConnectControl(Wire wControl)
        {
            Control.ConnectInput(wControl);
        }
        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }

        public override bool TestGate()
        {
            Control.Value = 0;
            
            for (int i = 0; i < Size; i++)
            {
                Input[i].Value = 0;
      
            }
            for (int i = 0; i < Size; i++)
            {
                if (Output1[i].Value != 0)
                    return false;
                if (Output2[i].Value != 0)
                    return false;
            }


            for (int i = 0; i < Size; i++)
            {
                Input[i].Value = 1;

            }
            for (int i = 0; i < Size; i++)
            {
                if (Output1[i].Value != 1)
                    return false;
                if (Output2[i].Value != 0)
                    return false;
            }

            Control.Value = 1;

            for (int i = 0; i < Size; i++)
            {
                Input[i].Value = 0;

            }
            for (int i = 0; i < Size; i++)
            {
                if (Output1[i].Value != 0)
                    return false;
                if (Output2[i].Value != 0)
                    return false;
            }


            for (int i = 0; i < Size; i++)
            {
                Input[i].Value = 1;

            }
            for (int i = 0; i < Size; i++)
            {
                if (Output1[i].Value != 0)
                    return false;
                if (Output2[i].Value != 1)
                    return false;
            }
            return true;



        }
    }
}
