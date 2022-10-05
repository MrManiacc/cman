﻿using CMan.Ast.Type;

namespace CMan.Ast
{
    /// <summary>
    /// The base level for any expression. Defines a type at the most basic level
    /// </summary>
    public interface IExpression : IStatement
    {
        /// <summary>
        /// Gets the defined type for this expression.
        /// </summary>
        IType GetType();
    }
}