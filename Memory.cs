﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a memory unit, containing k registers, each of size n bits.
    class Memory : SequentialGate
    {
        //The address size determines the number of registers
        public int AddressSize { get; private set; }
        //The word size determines the number of bits in each register
        public int WordSize { get; private set; }

        //Data input and output - a number with n bits
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        //The address of the active register
        public WireSet Address { get; private set; }
        //A bit setting the memory operation to read or write
        public Wire Load { get; private set; }
        private BitwiseMultiwayDemux bitwiseMultiwayDemux;
        private MultiBitRegister[] multiBitRegisters;
        private BitwiseMultiwayMux bitwiseMultiwayMux;
        private WireSet loadwireset;
        //your code here

        public Memory(int iAddressSize, int iWordSize)
        {
            AddressSize = iAddressSize;
            WordSize = iWordSize;

            Input = new WireSet(WordSize);
            Output = new WireSet(WordSize);
            Address = new WireSet(AddressSize);
            Load = new Wire();
            loadwireset = new WireSet(WordSize);
            int numberOfRegister = (int)(Math.Pow(2, AddressSize));
            bitwiseMultiwayDemux = new BitwiseMultiwayDemux(WordSize, AddressSize);
            multiBitRegisters = new MultiBitRegister[numberOfRegister];
            bitwiseMultiwayMux = new BitwiseMultiwayMux(WordSize, AddressSize);
            for(int i=0;i<AddressSize;i++)
            {
                loadwireset[i].ConnectInput(Load);
            }

            bitwiseMultiwayDemux.ConnectInput(loadwireset);
            bitwiseMultiwayDemux.ConnectControl(Address);
            bitwiseMultiwayMux.ConnectControl(Address);

            for (int i=0;i<multiBitRegisters.Length;i++)
            {
                multiBitRegisters[i] = new MultiBitRegister(WordSize);
                multiBitRegisters[i].ConnectInput(Input);
                multiBitRegisters[i].Load.ConnectInput(bitwiseMultiwayDemux.Outputs[i][0]);
                bitwiseMultiwayMux.Inputs[i].ConnectInput(multiBitRegisters[i].Output);
            }
            Output.ConnectInput(bitwiseMultiwayMux.Output);


            //your code here

        }

        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }
        public void ConnectAddress(WireSet wsAddress)
        {
            Address.ConnectInput(wsAddress);
        }


        public override void OnClockUp()
        {
        }

        public override void OnClockDown()
        {
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override bool TestGate()
        {

            Input.SetValue(0);
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != 0)
            return false;

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
