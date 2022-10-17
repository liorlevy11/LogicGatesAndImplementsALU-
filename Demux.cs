using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A demux has 2 outputs. There is a single input and a control bit, selecting whether the input should be directed to the first or second output.
    class Demux : Gate
    {
        public Wire Output1 { get; private set; }
        public Wire Output2 { get; private set; }
        public Wire Input { get; private set; }
        public Wire Control { get; private set; }
        private AndGate m_gAnd1;
        private AndGate m_gAnd2;
        private NotGate m_gNot;

        //your code here

        public Demux()
        {
            Input = new Wire();
            Control = new Wire();
            Output1 = new Wire();
            Output2 = new Wire();
            m_gAnd1 = new AndGate();
            m_gAnd2 = new AndGate();
            m_gNot = new NotGate();
            m_gAnd2.ConnectInput1(Input);
            m_gAnd2.ConnectInput2(Control);
            Output2.ConnectInput(m_gAnd2.Output);
            m_gNot.ConnectInput(Control);
            m_gAnd1.ConnectInput1(Input);
            m_gAnd1.ConnectInput2(m_gNot.Output);
            Output1.ConnectInput(m_gAnd1.Output);

            //your code here
        }

        public void ConnectControl(Wire wControl)
        {
            Control.ConnectInput(wControl);
        }
        public void ConnectInput(Wire wInput)
        {
            Input.ConnectInput(wInput);
        }



        public override bool TestGate()
        {
            
            Input.Value = 0;
            Control.Value = 0;
            if (Output1.Value != 0)
                return false;
            if (Output2.Value != 0)
                return false;
            Input.Value = 0;
            Control.Value = 1;
            if (Output1.Value != 0)
                return false;
            if (Output2.Value != 0)
                return false;
            Input.Value = 1;
            Control.Value = 0;
            if (Output1.Value != 1)
                return false;
            if (Output2.Value != 0)
                return false;
            Input.Value = 1;
            Control.Value =1;
            if (Output1.Value != 0)
                return false;
            if (Output2.Value != 1)
                return false;
            return true;
        }
    }
}
