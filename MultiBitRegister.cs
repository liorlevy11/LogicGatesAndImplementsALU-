using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class represents an n bit register that can maintain an n bit number
    class MultiBitRegister : Gate
    {
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        //A bit setting the register operation to read or write
        public Wire Load { get; private set; }
        public SingleBitRegister[] singleBitRegisters;

        //Word size - number of bits in the register
        public int Size { get; private set; }


        public MultiBitRegister(int iSize)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Output = new WireSet(Size);
            Load = new Wire();
            singleBitRegisters = new SingleBitRegister[Size];
            for(int i=0;i<Size;i++)
            {
                singleBitRegisters[i] = new SingleBitRegister();
                singleBitRegisters[i].ConnectInput(Input[i]);
                singleBitRegisters[i].ConnectLoad(Load);
                Output[i].ConnectInput(singleBitRegisters[i].Output); 
            }
            
            //your code here

        }

        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }

        
        public override string ToString()
        {
            return Output.ToString();
        }


        public override bool TestGate()
        {
             Input.SetValue(2);
             Load.Value = 1;
             Clock.ClockDown();
             Clock.ClockUp();
             if (Output.GetValue() != 2){
              return false;}

              Input.SetValue(4);
              Load.Value = 0;
              Clock.ClockDown();
              Clock.ClockUp();
              if (Output.GetValue() != 2){
                return false;}

              Input.SetValue(4);
              Load.Value = 1;
              Clock.ClockDown();
              Clock.ClockUp();
              if (Output.GetValue() != 4){
                return false;}


         return true;



        }
    }
}
