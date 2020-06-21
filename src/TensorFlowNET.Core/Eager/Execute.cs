﻿using System.Collections.Generic;
using System;
using System.Linq;

namespace Tensorflow.Eager
{
    public class Execute
    {
        /// <summary>
        /// Execute a TensorFlow operation.
        /// </summary>
        /// <param name="op_name">
        /// Name of the TensorFlow operation (see REGISTER_OP in C++ code) to 
        /// execute.
        /// </param>
        /// <param name="num_outputs">
        /// The number of outputs of the operation to fetch.
        /// </param>
        /// <param name="inputs">
        /// A list of inputs to the operation. Each entry should be a Tensor, or
        /// a value which can be passed to the Tensor constructor to create one.
        /// </param>
        /// <param name="attrs">
        /// A tuple with alternating string attr names and attr values for this
        /// operation.
        /// </param>
        /// <param name="ctx">The value of context.context().</param>
        /// <param name="name">Customized name for the operation.</param>
        /// <returns>List of output Tensor objects. The list is empty if there are no outputs</returns>
        public EagerTensor[] execute(Context ctx, string op_name, int num_outputs,
            EagerTensor[] inputs, object[] attrs, 
            string name = null)
        {
            ctx.ensure_initialized();

            using var status = new Status();
            var results = wrap_tfe_src.TFE_Execute(ctx,
               ctx.device_name,
               op_name,
               inputs,
               attrs,
               num_outputs,
               status);

            return results;
        }

        public (TF_DataType, EagerTensor[]) args_to_matching_eager(Context ctx, TF_DataType default_dtype = TF_DataType.DtInvalid, object[] args = null)
        {
            if (args.Length == 0 && default_dtype != TF_DataType.DtInvalid)
                return (default_dtype, null);

            if (args.Count(x => x is EagerTensor) == args.Length)
                return ((args[0] as EagerTensor).dtype, args.Select(x => x as EagerTensor).ToArray());

            var dtype = TF_DataType.DtInvalid;
            foreach (var x in args)
            {
                if (x is EagerTensor et)
                    dtype = et.dtype;
            }

            if (dtype == TF_DataType.DtInvalid)
            {
                var ret = new List<EagerTensor>();
                foreach (var t in args)
                {
                    ret.Add(ops.convert_to_tensor(t, dtype, preferred_dtype: default_dtype, ctx: ctx) as EagerTensor);
                    if (dtype == TF_DataType.DtInvalid)
                        dtype = ret.Last().dtype;
                }

                return (dtype, ret.ToArray());
            }
            else
                throw new NotImplementedException("");
        }
    }
}
