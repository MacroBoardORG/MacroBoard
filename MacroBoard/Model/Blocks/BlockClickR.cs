using System.Runtime.InteropServices;
using static MacroBoard.Utils;


namespace MacroBoard
{
    public class BlockClickR : Block
    {


        public BlockClickR()
        {
            base.Name = "Right Click";
            base.info = "Simulate a Right mouse click in the current position of the mouse pointer.";
        }


        public override void Execute()
        {
            mouse_event((int)MouseEventFlags.RightDown, 0, 0, 0, 0);
            mouse_event((int)MouseEventFlags.RightUp, 0, 0, 0, 0);
        }


        public override void Accept(IBlockVisitor visitor)
        {
            visitor.Visit(this);
        }


    }
}

