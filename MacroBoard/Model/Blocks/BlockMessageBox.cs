using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace MacroBoard
{
    [Serializable]
    public class BlockMessageBox : Block
    {

        public string param1 = "param1_default";
        public string param2 = "param2_default";

        public BlockMessageBox(string param1, string param2)
        {
            base.Name = "Pop Up";
            base.info = "Block useful for debugging.";
            base.LogoUrl = "/Resources/Logo_Blocks/Logo_BlockPopup.png";
            this.param1 = param1;
            this.param2 = param2;
            base.category = Categories.System;

        }

        public override void Execute()
        {
            MessageBox.Show($"MESSAGEBOX    param1: {this.param1}   param2: {this.param2}");
        }


        public override void Accept(IBlockVisitor visitor)
        {
            visitor.Visit(this);
        }



    }
}
