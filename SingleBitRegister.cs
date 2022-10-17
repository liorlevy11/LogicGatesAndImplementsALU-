using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a register that can maintain 1 bit.
    class SingleBitRegister : Gate
    {
        public Wire Input { get; private set; }
        public Wire Output { get; private set; }
        //A bit setting the register operation to read or write
        public Wire Load { get; private set; }
        private MuxGate muxg;
        private DFlipFlopGate flipFlopGate;
        public SingleBitRegister()
          
        {
            
            Input = new Wire();
            Load = new Wire();
            Output = new Wire();
            muxg = new MuxGate();
            flipFlopGate = new DFlipFlopGate();
            flipFlopGate.ConnectInput(muxg.Output);
            muxg.ConnectInput1(flipFlopGate.Output);
            muxg.ConnectInput2(Input);
            muxg.ConnectControl(Load);
            Output.ConnectInput(flipFlopGate.Output);

        }

        public void ConnectInput(Wire wInput)
        {
            Input.ConnectInput(wInput);
        }

      

        public void ConnectLoad(Wire wLoad)
        {
            Load.ConnectInput(wLoad);
        }


        public override bool TestGate()
        {
            Input.Value = 1;
            Load.Value = 1;
            return true;
            
            
        }
    }
}
