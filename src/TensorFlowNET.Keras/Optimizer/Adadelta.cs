﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Tensorflow.Keras
{
    public class Adadelta : Optimizer
    {
        public Adadelta(float lr= 0.01f, float rho = 0.95f, float? epsilon = null, float decay = 0) : base(null)
        {
            throw new NotImplementedException();
        }

        public override Tensor[] get_updates(Tensor loss, variables @params)
        {
            throw new NotImplementedException();
        }

        public override Hashtable get_config()
        {
            throw new NotImplementedException();
        }
    }
}
