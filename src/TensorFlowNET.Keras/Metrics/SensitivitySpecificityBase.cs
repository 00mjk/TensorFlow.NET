﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tensorflow.Keras.Metrics
{
    public class SensitivitySpecificityBase : Metric
    {
        public SensitivitySpecificityBase(float value, int num_thresholds= 200, string name = null, string dtype = null) : base(name, dtype)
        {
            throw new NotImplementedException();
        }

        public override Tensor result()
        {
            throw new NotImplementedException();
        }

        public override void update_state(Args args, KwArgs kwargs)
        {
            throw new NotImplementedException();
        }

        public override void reset_states()
        {
            throw new NotImplementedException();
        }
    }
}
