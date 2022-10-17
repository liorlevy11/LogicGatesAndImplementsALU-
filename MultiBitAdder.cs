using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements an adder, receving as input two n bit numbers, and outputing the sum of the two numbers
    class MultiBitAdder : Gate
    {
        //Word size - number of bits in each input
        public int Size { get; private set; }

        public WireSet Input1 { get; private set; }
        public WireSet Input2 { get; private set; }
        public WireSet Output { get; private set; }
        //An overflow bit for the summation computation
        public Wire Overflow { get; private set; }
        private FullAdder[] fullAdder;

        public MultiBitAdder(int iSize)
        {
          
                Size = iSize;
                Input1 = new WireSet(Size);
                Input2 = new WireSet(Size);
                Output = new WireSet(Size);
                fullAdder = new FullAdder[Size];
                Overflow = new Wire();

                for (int i = 0; i < fullAdder.Length; i++)
                {
                    fullAdder[i] = new FullAdder();
                    fullAdder[i].ConnectInput1(Input1[i]);
                    fullAdder[i].ConnectInput2(Input2[i]);
                    if(i>0)
                    {
                        fullAdder[i].CarryInput.ConnectInput(fullAdder[i-1].CarryOutput);
                    }
                    else
                    {
                        fullAdder[i].CarryInput.Value = 0;
                    }
                   Output[i].ConnectInput(fullAdder[i].Output);
                }
            Overflow.ConnectInput(fullAdder[fullAdder.Length - 1].CarryOutput);


               
            }
     
    

        public override string ToString()
        {
            return Input1 + "(" + Input1.Get2sComplement() + ")" + " + " + Input2 + "(" + Input2.Get2sComplement() + ")" + " = " + Output + "(" + Output.Get2sComplement() + ")";
        }

        public void ConnectInput1(WireSet wInput)
        {
            Input1.ConnectInput(wInput);
        }
        public void ConnectInput2(WireSet wInput)
        {
            Input2.ConnectInput(wInput);
        }


        public override bool TestGate()
        {
            Input1[0].Value = 0;
          
            Input2[0].Value = 1;

            
            if (Output[0].Value != 1 )
                return false;
            if (Overflow.Value != 0)
                return false;
            return true;

        }
    }
}
