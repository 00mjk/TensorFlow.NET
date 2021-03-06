﻿using System.Collections.Generic;

namespace Tensorflow.Keras.Engine
{
    public interface INode
    {
        Tensors input_tensors { get; }
        Tensors Outputs { get; }
        ILayer Layer { get; set; }
        List<Tensor> KerasInputs { get; set; }
        INode[] ParentNodes { get; }
        IEnumerable<(ILayer, int, int, Tensor)> iterate_inbound();
    }
}
