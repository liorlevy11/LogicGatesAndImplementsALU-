using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            //This is an example of a testing code that you should run for all the gates that you create
            
                        //Create a gate
                        AndGate and = new AndGate();
                        Console.WriteLine(and + "");
                        //Test that the unit testing works properly
                        if (!and.TestGate())
                            Console.WriteLine("bugbug");

                        OrGate or = new OrGate();
                        Console.WriteLine(or + "");
                        //Test that the unit testing works properly
                        if (!or.TestGate())
                            Console.WriteLine("bugbug");

                         MuxGate mux = new MuxGate();
                        Console.WriteLine(mux + "");
                        //Test that the unit testing works properly
                        if (!mux.TestGate())
                            Console.WriteLine("bugbug");
            
            XorGate xor = new XorGate();
            Console.WriteLine(xor + "");
            //Test that the unit testing works properly
            if (!xor.TestGate())
                Console.WriteLine("bugbug");
            
            Demux demux = new Demux();
            Console.WriteLine(demux + "");
            //Test that the unit testing works properly
            if (!demux.TestGate())
                Console.WriteLine("bugbug");

            MultiBitAndGate multibitAnd = new MultiBitAndGate(5);
            Console.WriteLine(multibitAnd + "");
            //Test that the unit testing works properly
            if (!multibitAnd.TestGate())
                Console.WriteLine("bugbug");

            MultiBitOrGate multibitOr = new MultiBitOrGate(5);
            Console.WriteLine(multibitOr + "");
            //Test that the unit testing works properly
            if (!multibitOr.TestGate())
                Console.WriteLine("bugbug");

            BitwiseAndGate bitwiseAndGate = new BitwiseAndGate(5);
            Console.WriteLine(bitwiseAndGate + "");
            //Test that the unit testing works properly
            if (!bitwiseAndGate.TestGate())
                Console.WriteLine("bugbug");

            BitwiseNotGate bitwiseNotGate = new BitwiseNotGate(5);
            Console.WriteLine(bitwiseNotGate + "");
            //Test that the unit testing works properly
            if (!bitwiseNotGate.TestGate())
                Console.WriteLine("bugbug");

            BitwiseOrGate bitwiseOrGate = new BitwiseOrGate(5);
            Console.WriteLine(bitwiseOrGate + "");
            //Test that the unit testing works properly
            if (!bitwiseOrGate.TestGate())
                Console.WriteLine("bugbug");


            BitwiseMux bitwiseMux = new BitwiseMux(5);
            Console.WriteLine(bitwiseMux + "");
            //Test that the unit testing works properly
            if (!bitwiseMux.TestGate())
                Console.WriteLine("bugbug");

           
            BitwiseDemux bitwiseDemux = new BitwiseDemux(5);
            Console.WriteLine(bitwiseDemux + "");
            //Test that the unit testing works properly
            if (!bitwiseDemux.TestGate())
                Console.WriteLine("bugbug");

          
            BitwiseMultiwayMux bitwiseMultiwayMux = new BitwiseMultiwayMux(1, 3);
            Console.WriteLine(bitwiseMultiwayMux + "");
            if (!bitwiseMultiwayMux.TestGate())
                Console.WriteLine("bugbug");
          
                
            BitwiseMultiwayDemux bitwiseMultiwayDemux = new BitwiseMultiwayDemux (4,4);
            Console.WriteLine(bitwiseMultiwayDemux + "");
            if (!bitwiseMultiwayDemux.TestGate())
                Console.WriteLine("bugbug");


            HalfAdder half = new HalfAdder();
            Console.WriteLine(half + "");
            if (!half.TestGate())
                Console.WriteLine("bugbug");

            FullAdder fullAdder = new FullAdder();
            Console.WriteLine(fullAdder + "");
            if (!fullAdder.TestGate())
                Console.WriteLine("bugbug");


            MultiBitAdder multiBitAdder = new MultiBitAdder(1);
            Console.WriteLine(multiBitAdder + "");
            if (!multiBitAdder.TestGate())
                Console.WriteLine("bugbug");
            */
            Console.WriteLine("start");
            ALU alu = new ALU(4);
            Console.WriteLine(alu.Size + "");
            if (!alu.TestGate())
                Console.WriteLine("bugbug");
            /*
            Console.WriteLine("***********");
            WireSet a = new WireSet(4);
            a.SetValue(4);
            Console.WriteLine("a.Tost");
            Console.WriteLine( a.ToString());
           Console.WriteLine( a.GetValue()+"   end");
            a.Set2sComplement(4);
            Console.WriteLine(a.ToString());
            Console.WriteLine(a.Get2sComplement());
            a.Set2sComplement(-4);
            Console.WriteLine(a.ToString());
            Console.WriteLine("a"+a.Get2sComplement());
            */

            //Now we ruin the nand gates that are used in all other gates. The gate should not work properly after this.
            //   NAndGate.Corrupt = true;
            // if (and.TestGate())
            //     Console.WriteLine("bugbug");


            BitwiseMultiwayMux m = new BitwiseMultiwayMux(7, 3);


            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
