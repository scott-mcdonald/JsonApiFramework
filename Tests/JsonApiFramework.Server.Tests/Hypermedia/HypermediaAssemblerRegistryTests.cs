// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Server.Hypermedia;
using JsonApiFramework.Server.TestAsserts.Hypermedia;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Server.Tests.Hypermedia
{
    public class HypermediaAssemblerRegistryTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public HypermediaAssemblerRegistryTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("GetAssemblerTestData")]
        public void TestHypermediaAssemblerRegistryGetAssembler(string name, IEnumerable<IHypermediaAssembler> hypermediaAssemblers, Func<IHypermediaAssemblerRegistry, IHypermediaAssembler> getAssemblerFunc, IHypermediaAssembler expectedHypermediaAssembler)
        {
            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);

            // Arrange
            var hypermediaAssemblerRegistry = new HypermediaAssemblerRegistry(hypermediaAssemblers);

            // Act
            var actualHypermediaAssembler = getAssemblerFunc(hypermediaAssemblerRegistry);

            // Assert
            HypermediaAssemblerAssert.Equal(expectedHypermediaAssembler, actualHypermediaAssembler);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        public static readonly IHypermediaAssembler[] HypermediaAssemblers =
        {
            new ResourceHypermediaAssembler1(),
            new ResourceHypermediaAssembler2(),
            new ResourceHypermediaAssembler3(),
            new ResourceHypermediaAssembler4(),
            new ResourceHypermediaAssembler5(),
            new ResourceHypermediaAssembler6(),
            new ResourceHypermediaAssembler7()
        };

        public static readonly IEnumerable<object[]> GetAssemblerTestData = new[]
            {
                new object[]
                    {
                        "WithResourceTypeDerivedHypermediaAssembler",
                        HypermediaAssemblers,
                        new Func<IHypermediaAssemblerRegistry, IHypermediaAssembler>(x => x.GetAssembler(typeof(Resource1))),
                        new ResourceHypermediaAssembler1()
                    },
                new object[]
                    {
                        "WithPath1AndResourceTypeDerivedHypermediaAssembler",
                        HypermediaAssemblers,
                        new Func<IHypermediaAssemblerRegistry, IHypermediaAssembler>(x => x.GetAssembler(typeof(Resource1), typeof(Resource2))),
                        new ResourceHypermediaAssembler2()
                    },
                new object[]
                    {
                        "WithPath1AndPath2AndResourceTypeDerivedHypermediaAssembler",
                        HypermediaAssemblers,
                        new Func<IHypermediaAssemblerRegistry, IHypermediaAssembler>(x => x.GetAssembler(typeof(Resource1), typeof(Resource2), typeof(Resource3))),
                        new ResourceHypermediaAssembler3()
                    },
                new object[]
                    {
                        "WithPath1AndPath2AndPath3AndResourceTypeDerivedHypermediaAssembler",
                        HypermediaAssemblers,
                        new Func<IHypermediaAssemblerRegistry, IHypermediaAssembler>(x => x.GetAssembler(typeof(Resource1), typeof(Resource2), typeof(Resource3), typeof(Resource4))),
                        new ResourceHypermediaAssembler4()
                    },
                new object[]
                    {
                        "WithPath1AndPath2AndPath3AndPath4AndResourceTypeDerivedHypermediaAssembler",
                        HypermediaAssemblers,
                        new Func<IHypermediaAssemblerRegistry, IHypermediaAssembler>(x => x.GetAssembler(typeof(Resource1), typeof(Resource2), typeof(Resource3), typeof(Resource4), typeof(Resource5))),
                        new ResourceHypermediaAssembler5()
                    },
                new object[]
                    {
                        "WithPath1AndPath2AndPath3AndPath4AndPath5AndResourceTypeDerivedHypermediaAssembler",
                        HypermediaAssemblers,
                        new Func<IHypermediaAssemblerRegistry, IHypermediaAssembler>(x => x.GetAssembler(typeof(Resource1), typeof(Resource2), typeof(Resource3), typeof(Resource4), typeof(Resource5), typeof(Resource6))),
                        new ResourceHypermediaAssembler6()
                    },
                new object[]
                    {
                        "WithPath1AndPath2AndPath3AndPath4AndPath5AndPath6AndResourceTypeDerivedHypermediaAssembler",
                        HypermediaAssemblers,
                        new Func<IHypermediaAssemblerRegistry, IHypermediaAssembler>(x => x.GetAssembler(typeof(Resource1), typeof(Resource2), typeof(Resource3), typeof(Resource4), typeof(Resource5), typeof(Resource6), typeof(Resource7))),
                        new ResourceHypermediaAssembler7()
                    },

                new object[]
                    {
                        "WithResourceTypeDefaultHypermediaAssembler",
                        HypermediaAssemblers,
                        new Func<IHypermediaAssemblerRegistry, IHypermediaAssembler>(x => x.GetAssembler(typeof(Resource11))),
                        new HypermediaAssembler()
                    },
                new object[]
                    {
                        "WithPath1AndResourceTypeDefaultHypermediaAssembler",
                        HypermediaAssemblers,
                        new Func<IHypermediaAssemblerRegistry, IHypermediaAssembler>(x => x.GetAssembler(typeof(Resource11), typeof(Resource12))),
                        new HypermediaAssembler()
                    },
                new object[]
                    {
                        "WithPath1AndPath2AndResourceTypeDefaultHypermediaAssembler",
                        HypermediaAssemblers,
                        new Func<IHypermediaAssemblerRegistry, IHypermediaAssembler>(x => x.GetAssembler(typeof(Resource11), typeof(Resource12), typeof(Resource13))),
                        new HypermediaAssembler()
                    },
                new object[]
                    {
                        "WithPath1AndPath2AndPath3AndResourceTypeDefaultHypermediaAssembler",
                        HypermediaAssemblers,
                        new Func<IHypermediaAssemblerRegistry, IHypermediaAssembler>(x => x.GetAssembler(typeof(Resource11), typeof(Resource12), typeof(Resource13), typeof(Resource14))),
                        new HypermediaAssembler()
                    },
                new object[]
                    {
                        "WithPath1AndPath2AndPath3AndPath4AndResourceTypeDefaultHypermediaAssembler",
                        HypermediaAssemblers,
                        new Func<IHypermediaAssemblerRegistry, IHypermediaAssembler>(x => x.GetAssembler(typeof(Resource11), typeof(Resource12), typeof(Resource13), typeof(Resource14), typeof(Resource15))),
                        new HypermediaAssembler()
                    },
                new object[]
                    {
                        "WithPath1AndPath2AndPath3AndPath4AndPath5AndResourceTypeDefaultHypermediaAssembler",
                        HypermediaAssemblers,
                        new Func<IHypermediaAssemblerRegistry, IHypermediaAssembler>(x => x.GetAssembler(typeof(Resource11), typeof(Resource12), typeof(Resource13), typeof(Resource14), typeof(Resource15), typeof(Resource16))),
                        new HypermediaAssembler()
                    },
                new object[]
                    {
                        "WithPath1AndPath2AndPath3AndPath4AndPath5AndPath6AndResourceTypeDefaultHypermediaAssembler",
                        HypermediaAssemblers,
                        new Func<IHypermediaAssemblerRegistry, IHypermediaAssembler>(x => x.GetAssembler(typeof(Resource11), typeof(Resource12), typeof(Resource13), typeof(Resource14), typeof(Resource15), typeof(Resource16), typeof(Resource17))),
                        new HypermediaAssembler()
                    },
            };
        #endregion

        #region Test Types
        public class Resource1 { }
        public class Resource2 { }
        public class Resource3 { }
        public class Resource4 { }
        public class Resource5 { }
        public class Resource6 { }
        public class Resource7 { }

        public class Resource11 { }
        public class Resource12 { }
        public class Resource13 { }
        public class Resource14 { }
        public class Resource15 { }
        public class Resource16 { }
        public class Resource17 { }

        public class ResourceHypermediaAssembler1 : HypermediaAssembler<Resource1> { }
        public class ResourceHypermediaAssembler2 : HypermediaAssembler<Resource1, Resource2> { }
        public class ResourceHypermediaAssembler3 : HypermediaAssembler<Resource1, Resource2, Resource3> { }
        public class ResourceHypermediaAssembler4 : HypermediaAssembler<Resource1, Resource2, Resource3, Resource4> { }
        public class ResourceHypermediaAssembler5 : HypermediaAssembler<Resource1, Resource2, Resource3, Resource4, Resource5> { }
        public class ResourceHypermediaAssembler6 : HypermediaAssembler<Resource1, Resource2, Resource3, Resource4, Resource5, Resource6> { }
        public class ResourceHypermediaAssembler7 : HypermediaAssembler<Resource1, Resource2, Resource3, Resource4, Resource5, Resource6, Resource7> { }
        #endregion
    }
}