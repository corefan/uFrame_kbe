﻿using System.ComponentModel;
using uFrame.Kernel;

namespace uFrame.MVVM.Events
{
    public class ViewModelPropertyChangedEvent //uFrame_kbe
    {
        public PropertyChangedEventHandler Handler { get; set; }
        public object Sender { get; set; }
        public string PropertyName { get; set; }
    }
}