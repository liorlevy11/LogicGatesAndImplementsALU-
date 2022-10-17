using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class represents a set of n wires (a cable)
    class WireSet
    {
        //Word size - number of bits in the register
        public int Size { get; private set; }
        
        public bool InputConected { get; private set; }
        
        private Wire[] set2cSupport;

        //An indexer providing access to a single wire in the wireset
        public Wire this[int i]
        {
            get
            {
                return m_aWires[i];
            }
        }
        private Wire[] m_aWires;
        
        public WireSet(int iSize)
        {
            Size = iSize;
            InputConected = false;
            m_aWires = new Wire[iSize];
            for (int i = 0; i < m_aWires.Length; i++)
                m_aWires[i] = new Wire();
        }
        public override string ToString()
        {
            string s = "[";
            for (int i = m_aWires.Length - 1; i >= 0; i--)
                s += m_aWires[i].Value;
            s += "]";
            return s;
        }

        //Transform a positive integer value into binary and set the wires accordingly, with 0 being the LSB
        public void SetValue(int iValue)
        {
            if (iValue == 0)
            {
                for (int i = 0; i < m_aWires.Length; i++)
                    m_aWires[i].Value = 0;
            }
            else
            {
                int output = 0;
                int count = 1;
                while (iValue != 0)
                {
                    output = output + ((iValue % 2) * count);
                    iValue = iValue / 2;
                    count = count * 10;

                }
               
                for (int i = 0; i <= m_aWires.Length-1; i++)
                {
                    m_aWires[i].Value = output % 10;
                    output = output / 10;
                }

            }
        }

        //Transform the binary code into a positive integer
        public int GetValue()
        {
            int output = 0;
            for(int i=0;i<Size;i++)
            {
                output = output + ((int)Math.Pow(2, i)) * m_aWires[i].Value;
            }
            return output;
        }

        //Transform an integer value into binary using 2`s complement and set the wires accordingly, with 0 being the LSB
        public void Set2sComplement(int iValue)
        {
            if (iValue >= 0)
                SetValue(iValue);
            else
            {
                SetValue((iValue * (-1)) - 1);
                for (int i = m_aWires.Length - 1; i >= 0; i--)
                {
                    if (m_aWires[i].Value == 0)
                        m_aWires[i].Value = 1;
                    else
                        m_aWires[i].Value = 0;
                }
                

            }
        }


        //Transform the binary code in 2`s complement into an integer
        public int Get2sComplement()
        {
            Wire[] temp0 = m_aWires;
            if (m_aWires[m_aWires.Length - 1].Value == 0)
                return GetValue();
            else
            {
                for (int i = m_aWires.Length - 1; i >= 0; i--)
                {
                    if (m_aWires[i].Value == 0)
                        m_aWires[i].Value = 1;
                    else
                        m_aWires[i].Value = 0;

                }
                int temp1 = (GetValue()+1)*(-1);
                for (int i = m_aWires.Length - 1; i >= 0; i--)
                {
                    if (m_aWires[i].Value == 0)
                        m_aWires[i].Value = 1;
                    else
                        m_aWires[i].Value = 0;
                   
                }
                return temp1;
            

            }
           
          

        }
            
        

        public void ConnectInput(WireSet wIn)
        {
            if (InputConected)
                throw new InvalidOperationException("Cannot connect a wire to more than one inputs");
            if(wIn.Size != Size)
                throw new InvalidOperationException("Cannot connect two wiresets of different sizes.");
            for (int i = 0; i < m_aWires.Length; i++)
                m_aWires[i].ConnectInput(wIn[i]);

            InputConected = true;
            
        }

    }
}
