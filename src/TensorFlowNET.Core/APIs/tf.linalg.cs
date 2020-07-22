﻿/*****************************************************************************
   Copyright 2018 The TensorFlow.NET Authors. All Rights Reserved.

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
******************************************************************************/

namespace Tensorflow
{
    public partial class tensorflow
    {
        public LinalgApi linalg { get; } = new LinalgApi();

        public class LinalgApi
        {
            linalg_ops ops = new linalg_ops();

            public Tensor eye(int num_rows,
                int num_columns = -1,
                TensorShape batch_shape = null,
                TF_DataType dtype = TF_DataType.TF_FLOAT,
                string name = null)
                => ops.eye(num_rows, num_columns: num_columns, batch_shape: batch_shape, dtype: dtype, name: name);

            public Tensor diag(Tensor diagonal, string name = null)
                => gen_array_ops.diag(diagonal, name: name);

            public Tensor matmul(Tensor a, Tensor b)
                => math_ops.matmul(a, b);

            public Tensor batch_matmul(Tensor x, Tensor y)
                => gen_math_ops.batch_mat_mul(x, y);
        }

        public Tensor diag(Tensor diagonal, string name = null)
            => gen_array_ops.diag(diagonal, name: name);

        public Tensor matmul(Tensor a, Tensor b)
            => math_ops.matmul(a, b);

        public Tensor batch_matmul(Tensor x, Tensor y)
            => gen_math_ops.batch_mat_mul(x, y);
    }
}
