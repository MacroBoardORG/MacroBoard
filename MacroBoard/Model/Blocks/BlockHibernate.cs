﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using static MacroBoard.Utils;


namespace MacroBoard 
{
    [Serializable]
    public class BlockHibernate : Block
    {

        public BlockHibernate()
        {
            base.Name = "Hibernate";
            base.info = "Sleep/hibernate the computer.";
        }


        public override void Execute()
        {
            SetSuspendState(false, true, true);
        }

        public override void Accept(IBlockVisitor visitor)
        {
            visitor.Visit(this);
        }


    }
}
