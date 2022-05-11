﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroBoard
{
    public class BlockCreateTextFile : Block
    {
        public string _filePath, _fileName, _fileType, _text;
        public BlockCreateTextFile(string filePath, string fileName, string fileType, string text)
        {
            base.Name = "Create text file";
            base.info = "Create a text file of any type.";
            _filePath = filePath;
            _fileName = fileName;
            _fileType = fileType;
            _text = text;
        }

        public override void Execute()
        {
            
            Process.Start("powershell.exe", "ADD-content -path " +
                "'"+_filePath + "\\" + _fileName + _fileType + "'" +
                " -value " + "'" + _text + "'");

        }
    }
}
