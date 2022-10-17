using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class is used to implement the ALU
    class ALU : Gate
    {
        //The word size = number of bit in the input and output
        public int Size { get; private set; }

        //Input and output n bit numbers
        //inputs
        public WireSet InputX { get; private set; }
        public WireSet InputY { get; private set; }
        public WireSet Control { get; private set; }

        private WireSet ZeroWireSet;
        private WireSet oneWireSet;
        private WireSet MinusoneWireSet;
        private BitwiseMux[] bitwiseMuxes;
        private BitwiseNotGate[] bitwiseNotGates;
        private MultiBitAdder[] multiBitAdders;
        private BitwiseAndGate bitwiseAndGate;
        private BitwiseOrGate bitwiseOrGate;
        private MultiBitAndGate[] multiBitAndGate;
        private MultiBitOrGate multiBitOrGate;

        //outputs
        public WireSet Output { get; private set; }
        public Wire Zero { get; private set; }
        public Wire Negative { get; private set; }
        private Wire change;
       // public WireSet Inputx { get; }

        //mUX
        private BitwiseMultiwayMux bitwiseMultiwayMux;

        //your code here

        public ALU(int iSize)
        {
            
            
            Size = iSize;
            Output = new WireSet(Size);
            InputX = new WireSet(Size);
            InputX.Set2sComplement(InputX.Get2sComplement());
            InputY = new WireSet(Size);
            InputY.Set2sComplement(InputY.Get2sComplement());
            Control = new WireSet(6);
            Zero = new Wire();
            Negative = new Wire();
            bitwiseMultiwayMux = new BitwiseMultiwayMux(Size, Control.Size);
            ZeroWireSet = new WireSet(Size);
            MinusoneWireSet = new WireSet(Size);
            oneWireSet = new WireSet(Size);
            bitwiseMuxes = new BitwiseMux[4];
            bitwiseNotGates = new BitwiseNotGate[2];
            multiBitAdders = new MultiBitAdder[3];
            bitwiseAndGate = new BitwiseAndGate(Size);
            bitwiseOrGate = new BitwiseOrGate(Size);
            multiBitAndGate = new MultiBitAndGate[2];
            multiBitOrGate = new MultiBitOrGate(Size);
            change = new Wire();
            change.Value = 0;
           // Zero.Value = 0;
            for(int i=0;i<Size;i++)
            {
                ZeroWireSet[i].Value = 0;
                oneWireSet[i].Value = 0;
                MinusoneWireSet[i].Value = 1;

            }
            
            for(int i=0;i< bitwiseNotGates.Length;i++)
            {
                bitwiseNotGates[i] = new BitwiseNotGate(Size);
            }
            oneWireSet[0].Value = 1;
            for (int i = 0; i < bitwiseMuxes.Length; i++)
            {
                bitwiseMuxes[i] = new BitwiseMux(Size);
            }
            
            for (int i = 0; i < multiBitAdders.Length; i++)
            {
                multiBitAdders[i] = new MultiBitAdder(Size);
            }
            for(int i=0;i<multiBitAndGate.Length;i++)
            {
                multiBitAndGate[i] = new MultiBitAndGate(Size);
            }



            //Create and connect all the internal components
            // the basic
            bitwiseMultiwayMux.ConnectInput(0, ZeroWireSet);
            bitwiseMultiwayMux.ConnectInput(1, oneWireSet);
            bitwiseMultiwayMux.ConnectInput(2, InputX);
            bitwiseMultiwayMux.ConnectInput(3, InputY);

            //!, -,+1

            bitwiseMuxes[0].ConnectInput1(InputX);
            bitwiseMuxes[0].ConnectInput2(InputY);
            bitwiseMuxes[0].ConnectControl(Control[0]);
            bitwiseNotGates[0].ConnectInput(bitwiseMuxes[0].Output);
            bitwiseMuxes[1].ConnectInput1(bitwiseMuxes[0].Output);
            bitwiseMuxes[1].ConnectInput2(bitwiseNotGates[0].Output);
            bitwiseMuxes[1].ConnectControl(Control[2]);
            multiBitAdders[0].ConnectInput1(bitwiseMuxes[1].Output);
            multiBitAdders[0].ConnectInput2(oneWireSet);

            bitwiseMultiwayMux.ConnectInput(4, bitwiseNotGates[0].Output);
            bitwiseMultiwayMux.ConnectInput(5, bitwiseNotGates[0].Output);
            bitwiseMultiwayMux.ConnectInput(6, multiBitAdders[0].Output);
            bitwiseMultiwayMux.ConnectInput(7, multiBitAdders[0].Output);
            bitwiseMultiwayMux.ConnectInput(8, multiBitAdders[0].Output);
            bitwiseMultiwayMux.ConnectInput(9, multiBitAdders[0].Output);

            //-1,X+Y
            bitwiseMuxes[2].ConnectInput1(MinusoneWireSet);
            bitwiseMuxes[2].ConnectInput2(InputY);
            bitwiseMuxes[2].ConnectControl(Control[2]);
            multiBitAdders[1].ConnectInput1(bitwiseMuxes[0].Output);
            multiBitAdders[1].ConnectInput2(bitwiseMuxes[2].Output);

            bitwiseMultiwayMux.ConnectInput(10, multiBitAdders[1].Output);
            bitwiseMultiwayMux.ConnectInput(11, multiBitAdders[1].Output);
            bitwiseMultiwayMux.ConnectInput(12, multiBitAdders[1].Output);

            //x-y y-x
            bitwiseMuxes[3].ConnectInput1(InputY);
            bitwiseMuxes[3].ConnectInput2(InputX);
            bitwiseMuxes[3].ConnectControl(Control[0]);
            multiBitAdders[2].ConnectInput1(bitwiseMuxes[3].Output);
            multiBitAdders[2].ConnectInput2(multiBitAdders[0].Output);

            bitwiseMultiwayMux.ConnectInput(13, multiBitAdders[2].Output);
            bitwiseMultiwayMux.ConnectInput(14, multiBitAdders[2].Output);

            // X&y

            bitwiseAndGate.ConnectInput1(InputX);
            bitwiseAndGate.ConnectInput2(InputY);
            bitwiseMultiwayMux.ConnectInput(15, bitwiseAndGate.Output);

            //x^ y(logical and)- change!!!!!!!!!!!
            multiBitAndGate[0].ConnectInput(multiBitAdders[1].Output);
            WireSet a = new WireSet(Size);
            a.SetValue(multiBitAndGate[0].Output.Value);
            bitwiseMultiwayMux.ConnectInput(16, a);

            //X LOGIC OR Y
            multiBitOrGate.ConnectInput(multiBitAdders[1].Output);
            a.SetValue(multiBitOrGate.Output.Value);
            bitwiseMultiwayMux.ConnectInput(18, a);

            //x|y
            bitwiseOrGate.ConnectInput1(InputX);
            bitwiseOrGate.ConnectInput2(InputY);
            bitwiseMultiwayMux.ConnectInput(17, bitwiseOrGate.Output);

            //outputs
            //output
            bitwiseMultiwayMux.ConnectControl(Control);
            Output.ConnectInput(bitwiseMultiwayMux.Output);
            //negativ
            Negative.ConnectInput(Output[Size-1]);
            //zero
            bitwiseNotGates[1].ConnectInput(Output);
            multiBitAndGate[1].ConnectInput(bitwiseNotGates[1].Output);
            Zero.ConnectInput(multiBitAndGate[1].Output);

        }

        public override bool TestGate()
        {
            //x=4 y=2
            InputX[0].Value = 0;
            InputX[1].Value =1;
            InputX[2].Value = 0;
            InputX[3].Value = 0;


            InputY[0].Value = 0;
            InputY[1].Value = 0;
            InputY[2].Value = 1;
            InputY[3].Value = 0;

            //0
            Control[0].Value = 0;
            Control[1].Value = 0;
            Control[2].Value = 0;
            Control[3].Value = 0;
            Control[4].Value = 0;
            Control[5].Value = 0;
         //   Console.WriteLine( multiBitAdders[1].Output.Get2sComplement()+ " multiBitAdders[1].Output.Get2sComplement( " + multiBitAdders[1].Output.ToString()+ " ultiBitAdders[1].Output.ToString() " + MinusoneWireSet.Get2sComplement() + "MinusoneWireSet.Get2sComplement() " + MinusoneWireSet.ToString() + " MinusoneWireSet.ToString() " + bitwiseMuxes[0].Output.Get2sComplement()+ "    bitwiseMuxes[0].Output.Get2sComplement() " + bitwiseMuxes[2].Output.Get2sComplement()+ "   bitwiseMuxes[2].Output.Get2sComplement()");
         /*   Console.WriteLine(Output.GetValue()+" 0");//0
            Control[0].Value = 1;
            Console.WriteLine(Output.GetValue()+" 1");//1
            Control[0].Value = 0;
            Control[1].Value = 1;
            Console.WriteLine(Output.GetValue()+" x");//2
            Control[0].Value = 1;
            Control[1].Value = 1;
            Console.WriteLine(Output.GetValue()+" y");//3
            Control[0].Value = 0;
            Control[1].Value = 0;
            Control[2].Value = 1;
            Console.WriteLine(Output.Get2sComplement()+" !x");//4
            Control[0].Value = 1;
            Console.WriteLine(Output.Get2sComplement()+" !y");//5
            Control[0].Value = 0;
            Control[1].Value = 1;
            Console.WriteLine(Output.Get2sComplement()+" -x");//6
            Control[0].Value = 1;
            Console.WriteLine(Output.Get2sComplement()+" -y");//7
            Control[0].Value = 0;
            Control[1].Value = 0;
            Control[2].Value = 0;
            Control[3].Value = 1;
            Console.WriteLine(Output.Get2sComplement()+" x+1");//8
            Control[0].Value = 1;
            Console.WriteLine(Output.Get2sComplement()+" y+1");//9
            Control[0].Value = 0;
            Control[1].Value = 1;
            Console.WriteLine(bitwiseMuxes[0].Output.Get2sComplement());
            Console.WriteLine(bitwiseMuxes[2].Output.Get2sComplement());
            Console.WriteLine(Output.Get2sComplement()+" x-1");//10
            Control[0].Value = 1;
            Console.WriteLine(Output.Get2sComplement()+" y-1");//11
            Control[0].Value = 0;
            Control[1].Value = 0;
            Control[2].Value = 1;
            Console.WriteLine(Output.Get2sComplement()+" x+y");//12
            Control[0].Value = 1;
            Console.WriteLine(Output.Get2sComplement()+" x-y");//13
            Control[0].Value = 0;
            Control[1].Value = 1;
            Console.WriteLine(Output.Get2sComplement()+" y-x");//14
            Control[0].Value = 1;
            Console.WriteLine(Output.Get2sComplement()+"and");//15
            Console.WriteLine(Output.Get2sComplement());//16
            Control[0].Value = 1;
            Console.WriteLine(Output.Get2sComplement()+" or");//17
            Control[0].Value = 0;
            Control[1].Value = 1;
            Console.WriteLine(Output.Get2sComplement());//18
         */

            return true;
        }
    }
}
