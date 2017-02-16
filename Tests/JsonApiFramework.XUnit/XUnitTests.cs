// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Text;

using Xunit.Abstractions;

namespace JsonApiFramework.XUnit
{
    /// <summary>Base class for all xUnit collection of tests.</summary>
    public abstract class XUnitTests
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Write Methods
        public void WriteBuffer()
        {
            if (this.OutputBuffer == null)
                return;

            var output = this.OutputBuffer.ToString();
            this.Output.WriteLine(output);
        }

        public void WriteLine()
        {
            if (this.BufferOutput)
            {
                this.OutputBuffer = this.OutputBuffer ?? new StringBuilder();
                this.OutputBuffer.AppendLine();
                return;
            }

            this.Output.WriteLine(String.Empty);
        }

        public void WriteLine(string message)
        {
            if (this.BufferOutput)
            {
                this.OutputBuffer = this.OutputBuffer ?? new StringBuilder();
                this.OutputBuffer.AppendLine(message);
                return;
            }

            this.Output.WriteLine(message);
        }

        public void WriteLine(string format, params object[] args)
        {
            if (this.BufferOutput)
            {
                this.OutputBuffer = this.OutputBuffer ?? new StringBuilder();
                this.OutputBuffer.AppendFormat(format, args);
                this.OutputBuffer.AppendLine();
                return;
            }

            this.Output.WriteLine(format, args);
        }

        public void WriteDashedLine()
        {
            if (this.BufferOutput)
            {
                this.OutputBuffer = this.OutputBuffer ?? new StringBuilder();
                this.OutputBuffer.AppendLine(SingleDashedLine);
                return;
            }

            this.Output.WriteLine(SingleDashedLine);
        }

        public void WriteDoubleDashedLine()
        {
            if (this.BufferOutput)
            {
                this.OutputBuffer = this.OutputBuffer ?? new StringBuilder();
                this.OutputBuffer.AppendLine(SingleDashedLine);
                return;
            }

            this.Output.WriteLine(DoubleDashedLine);
        }

        #endregion

        // PROTECTED CONSTRUCTORS ///////////////////////////////////////////
        #region Constructors
        protected XUnitTests(ITestOutputHelper output, bool bufferOutput = false)
        {
            Contract.Requires(output != null);

            this.Output = output;
            this.BufferOutput = bufferOutput;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private ITestOutputHelper Output { get; set; }

        private bool BufferOutput { get; set; }
        private StringBuilder OutputBuffer { get; set; }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private const string DoubleDashedLine =
            "=============================================================================";

        private const string SingleDashedLine =
            "-----------------------------------------------------------------------------";
        #endregion
    }
}
