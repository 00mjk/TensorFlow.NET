﻿using NumSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tensorflow.Eager
{
    public partial class EagerTensor : Tensor
    {
        Status status = new Status();
        TFE_TensorHandle tfe_tensor_handle;
        public EagerTensor(IntPtr handle) : base(handle)
        {
            tfe_tensor_handle = handle;
            _handle = c_api.TFE_TensorHandleResolve(handle, status.Handle);
        }

        public EagerTensor(string value, string device_name) : base(value)
        {
            tfe_tensor_handle = c_api.TFE_NewTensorHandle(_handle, status.Handle);
        }

        public EagerTensor(int value, string device_name) : base(value)
        {
            tfe_tensor_handle = c_api.TFE_NewTensorHandle(_handle, status.Handle);
        }

        public EagerTensor(float[] value, string device_name) : base(value)
        {
            tfe_tensor_handle = c_api.TFE_NewTensorHandle(_handle, status.Handle);
        }

        public EagerTensor(double[] value, string device_name) : base(value)
        {
            tfe_tensor_handle = c_api.TFE_NewTensorHandle(_handle, status.Handle);
        }

        public EagerTensor(NDArray value, string device_name) : base(value)
        {
            tfe_tensor_handle = c_api.TFE_NewTensorHandle(_handle, status.Handle);
        }

        public override string ToString()
        {
            switch (rank)
            {
                case -1:
                    return $"tf.Tensor: shape=<unknown>, dtype={dtype.as_numpy_name()}, numpy={GetFormattedString()}";
                case 0:
                    return $"tf.Tensor: shape=(), dtype={dtype.as_numpy_name()}, numpy={GetFormattedString()}";
                default:
                    return $"tf.Tensor: shape=({string.Join(",", shape)}), dtype={dtype.as_numpy_name()}, numpy={GetFormattedString()}";
            }
        }

        private string GetFormattedString()
        {
            var nd = numpy();
            switch (dtype)
            {
                case TF_DataType.TF_STRING:
                    return $"b'{(string)nd}'";
                case TF_DataType.TF_BOOL:
                    return (nd.GetByte(0) > 0).ToString();
                default:
                    return nd.ToString();
            }
        }
    }
}
