using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace MacroBoard
{
    internal class MessageBoxBlock : Block
    {

        string param1 = "param1_default";
        string param2 = "param2_default";

        public MessageBoxBlock(string param1, string param2)
        {
            this.param1 = param1;
            this.param2 = param2;
        }

        public override void Execute()
        {
            MessageBox.Show($"MESSAGEBOX    param1: {this.param1}   param2: {this.param2}");
        }






    }
}
