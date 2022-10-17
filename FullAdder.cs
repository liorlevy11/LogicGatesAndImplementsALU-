using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a FullAdder, taking as input 3 bits - 2 numbers and a carry, and computing the result in the output, and the carry out.
    class FullAdder : TwoInputGate
    {
        public Wire CarryInput { get; private set; }
        public Wire CarryOutput { get; private set; }
        private HalfAdder halfAdder1;
        private HalfAdder halfAdder2;
        private OrGate Or;


        //your code here


        public FullAdder()
        {
            CarryInput = new Wire();
            CarryOutput = new Wire();
            halfAdder1 = new HalfAdder();
            halfAdder2 = new HalfAdder();
            Or = new OrGate();
            halfAdder1.ConnectInput1(Input1);
            halfAdder1.ConnectInput2(CarryInput);
            halfAdder2.ConnectInput1(halfAdder1.Output);
            halfAdder2.ConnectInput2(Input2);
            Or.ConnectInput1(halfAdder1.CarryOutput);
            Or.ConnectInput2(halfAdder2.CarryOutput);
            CarryOutput.ConnectInput(Or.Output);
            Output.ConnectInput(halfAdder2.Output);
            
            //your code here

        }


        public override string ToString()
        {
            return Input1.Value + "+" + Input2.Value + " (C" + CarryInput.Value + ") = " + Output.Value + " (C" + CarryOutput.Value + ")";
        }

        public override bool TestGate()
        {
            Input1.Value = 0;
            Input2.Value = 0;
            CarryInput.Value = 1;
            if (Output.Value != 1 || CarryOutput.Value != 0)
                return false;
            Input1.Value = 0;
            Input2.Value = 0;
            CarryInput.Value = 0;
            if (Output.Value != 0 || CarryOutput.Value != 0)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            CarryInput.Value = 1;
           // Console.WriteLine(" Input1.Value  1 +Input2.Value  0 + CarryInput.Value = 1  + Output.Value++ CarryOutput.Value" + Input1.Value + "+" + Input2.Value + "+" + CarryInput.Value+ "+"+ Output.Value+"+"+ CarryOutput.Value);
            if (Output.Value != 0 || CarryOutput.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 1;
            CarryInput.Value = 1;
            if (Output.Value != 1 || CarryOutput.Value != 1)
                return false;
            return true;
        }
    }
}
