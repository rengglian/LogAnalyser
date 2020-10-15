using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PatternGenerator.Helper
{
    public class ShapeOptions
    {
        public int Value { get; set; }

        public string Description { get; set; }

        public ShapeOptions(int value, string description)
        {
            Value = value;
            Description = description;
        }

    }
}
