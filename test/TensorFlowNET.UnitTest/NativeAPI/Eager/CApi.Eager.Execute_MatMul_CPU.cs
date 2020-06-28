﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tensorflow;
using static Tensorflow.Binding;

namespace TensorFlowNET.UnitTest.NativeAPI
{
    public partial class CApiEagerTest
    {
        /// <summary>
        /// TEST(CAPI, Execute_MatMul_CPU)
        /// </summary>
        [TestMethod]
        public unsafe void Execute_MatMul_CPU()
        {
            Execute_MatMul_CPU(false);
        }

        unsafe void Execute_MatMul_CPU(bool async)
        {
            using var status = TF_NewStatus();
            var opts = TFE_NewContextOptions();
            c_api.TFE_ContextOptionsSetAsync(opts, Convert.ToByte(async));

            IntPtr t;
            using (var ctx = TFE_NewContext(opts, status))
            {
                CHECK_EQ(TF_OK, TF_GetCode(status), TF_Message(status));
                TFE_DeleteContextOptions(opts);

                var m = TestMatrixTensorHandle();
                var matmul = MatMulOp(ctx, m, m);
                var retvals = new IntPtr[] { IntPtr.Zero, IntPtr.Zero };
                int num_retvals = 2;
                c_api.TFE_Execute(matmul, retvals, ref num_retvals, status);
                EXPECT_EQ(1, num_retvals);
                EXPECT_EQ(TF_OK, TF_GetCode(status), TF_Message(status));
                TFE_DeleteOp(matmul);
                TFE_DeleteTensorHandle(m);

                t = TFE_TensorHandleResolve(retvals[0], status);
                ASSERT_EQ(TF_OK, TF_GetCode(status), TF_Message(status));
                TFE_DeleteTensorHandle(retvals[0]);
            }

            ASSERT_EQ(TF_OK, TF_GetCode(status), TF_Message(status));
            var product = new float[4];
            EXPECT_EQ(product.Length * sizeof(float), (int)TF_TensorByteSize(t));
            tf.memcpy(product, TF_TensorData(t), TF_TensorByteSize(t));

            c_api.TF_DeleteTensor(t);
            EXPECT_EQ(7f, product[0]);
            EXPECT_EQ(10f, product[1]);
            EXPECT_EQ(15f, product[2]);
            EXPECT_EQ(22f, product[3]);
        }
    }
}
