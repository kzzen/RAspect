﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RAspect.Patterns.Threading
{
    /// <summary>
    /// Attribute when applied to a class implements the freezeable threading model. The aspect introduces functionality that allows using provided freeze method on class. After the Freeze() method has been invoked, the object can no longer be modified, can therefore be safely shared between several threads
    /// </summary>
    public class FreezeableAttribute : AspectBase
    {
        /// <summary>
        /// Validate Contract Method
        /// </summary>
        private readonly static MethodInfo ThrowIfFrozenMethod = typeof(Freezeable).GetMethod("ThrowIfFrozen", ILWeaver.NonPublicBinding);

        /// <summary>
        /// Initializes a new instance of the <see cref="FreezeableAttribute"/> class.
        /// </summary>
        public FreezeableAttribute()
        {
            OnBeginAspectBlock = BeginAspectBlock;
        }

        /// <summary>
        /// Gets weave block type
        /// </summary>
        internal override WeaveBlockType BlockType
        {
            get
            {
                return WeaveBlockType.Inline;
            }
        }

        /// <summary>
        /// Aspect code to inject at the beginning of weaved method
        /// </summary>
        /// <param name="typeBuilder">Type Builder</param>
        /// <param name="method">Method</param>
        /// <param name="parameter">Parameter</param>
        /// <param name="il">ILGenerator</param>
        internal void BeginAspectBlock(TypeBuilder typeBuilder, MethodBase method, ParameterInfo parameter, ILGenerator il)
        {
            var isSetProperty = method.Name.StartsWith("set_") && !method.IsStatic;

            if (!isSetProperty)
            {
                return;
            }

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, ThrowIfFrozenMethod);
        }
    }
}
