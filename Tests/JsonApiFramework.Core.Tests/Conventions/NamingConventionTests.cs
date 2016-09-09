// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

using JsonApiFramework.Conventions;
using JsonApiFramework.Conventions.Internal;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Conventions
{
    public class NamingConventionTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public NamingConventionTests(ITestOutputHelper output)
            : base(output)
        { }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData("ApplyTestData")]
        public void TestNamingConventionApply(string testName, INamingConvention namingConvention, string originalOldName, string expectedNewName)
        {
            this.Output.WriteLine("Test Name: {0}", testName);
            this.Output.WriteLine(String.Empty);

            // Arrange
            this.Output.WriteLine("Original Old Name");
            this.Output.WriteLine(originalOldName ?? String.Empty);

            this.Output.WriteLine(String.Empty);

            this.Output.WriteLine("Expected New Name");
            this.Output.WriteLine(expectedNewName ?? String.Empty);

            // Act
            var actualNewName = namingConvention.Apply(originalOldName);

            this.Output.WriteLine(String.Empty);

            this.Output.WriteLine("Actual New Name");
            this.Output.WriteLine(actualNewName ?? String.Empty);

            // Assert
            Assert.Equal(expectedNewName, actualNewName);
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        // ReSharper disable UnusedMember.Global
        public static readonly IEnumerable<object[]> ApplyTestData = new[]
            {
                new object[] {"WithLowerCaseNamingConventionAndNameParameterAsNullString", new LowerCaseNamingConvention(), null, null},
                new object[] {"WithLowerCaseNamingConventionAndNameParameterAsEmptyString", new LowerCaseNamingConvention(), String.Empty, String.Empty},
                new object[] {"WithLowerCaseNamingConventionAndNameParameterAsName", new LowerCaseNamingConvention(), "Name", "name"},
                new object[] {"WithLowerCaseNamingConventionAndNameParameterAsUserName", new LowerCaseNamingConvention(), "UserName", "username"},
                new object[] {"WithLowerCaseNamingConventionAndNameParameterAsPreviousUserName", new LowerCaseNamingConvention(), "PreviousUserName", "previoususername"},

                new object[] {"WithPluralNamingConventionAndNameParameterAsNullString", new PluralNamingConvention(), null, null},
                new object[] {"WithPluralNamingConventionAndNameParameterAsEmptyString", new PluralNamingConvention(), String.Empty, String.Empty},
                new object[] {"WithPluralNamingConventionAndNameParameterAsArticleString", new PluralNamingConvention(), "Article", "Articles"},
                new object[] {"WithPluralNamingConventionAndNameParameterAsArticlesString", new PluralNamingConvention(), "Articles", "Articles"},
                new object[] {"WithPluralNamingConventionAndNameParameterAsManString", new PluralNamingConvention(), "Man", "Men"},
                new object[] {"WithPluralNamingConventionAndNameParameterAsMenString", new PluralNamingConvention(), "Men", "Men"},
                new object[] {"WithPluralNamingConventionAndNameParameterAsPersonString", new PluralNamingConvention(), "Person", "People"},
                new object[] {"WithPluralNamingConventionAndNameParameterAsPeopleString", new PluralNamingConvention(), "People", "People"},
                new object[] {"WithPluralNamingConventionAndNameParameterAsStatusString", new PluralNamingConvention(), "Status", "Statuses"},
                new object[] {"WithPluralNamingConventionAndNameParameterAsStatusesString", new PluralNamingConvention(), "Statuses", "Statuses"},
                new object[] {"WithPluralNamingConventionAndNameParameterAsTheDeprecatedArticleString", new PluralNamingConvention(), "TheDeprecatedArticle", "TheDeprecatedArticles"},
                new object[] {"WithPluralNamingConventionAndNameParameterAsTheDeprecatedArticlesString", new PluralNamingConvention(), "TheDeprecatedArticles", "TheDeprecatedArticles"},
                new object[] {"WithPluralNamingConventionAndNameParameterAsTheDeprecatedManString", new PluralNamingConvention(), "TheDeprecatedMan", "TheDeprecatedMen"},
                new object[] {"WithPluralNamingConventionAndNameParameterAsTheDeprecatedMenString", new PluralNamingConvention(), "TheDeprecatedMen", "TheDeprecatedMen"},
                new object[] {"WithPluralNamingConventionAndNameParameterAsTheDeprecatedPersonString", new PluralNamingConvention(), "TheDeprecatedPerson", "TheDeprecatedPeople"},
                new object[] {"WithPluralNamingConventionAndNameParameterAsTheDeprecatedPeopleString", new PluralNamingConvention(), "TheDeprecatedPeople", "TheDeprecatedPeople"},
                new object[] {"WithPluralNamingConventionAndNameParameterAsTheDeprecatedStatusString", new PluralNamingConvention(), "TheDeprecatedStatus", "TheDeprecatedStatuses"},
                new object[] {"WithPluralNamingConventionAndNameParameterAsTheDeprecatedStatusesString", new PluralNamingConvention(), "TheDeprecatedStatuses", "TheDeprecatedStatuses"},

                new object[] {"WithSingularNamingConventionAndNameParameterAsNullString", new SingularNamingConvention(), null, null},
                new object[] {"WithSingularNamingConventionAndNameParameterAsEmptyString", new SingularNamingConvention(), String.Empty, String.Empty},
                new object[] {"WithSingularNamingConventionAndNameParameterAsArticleString", new SingularNamingConvention(), "Article", "Article"},
                new object[] {"WithSingularNamingConventionAndNameParameterAsArticlesString", new SingularNamingConvention(), "Articles", "Article"},
                new object[] {"WithSingularNamingConventionAndNameParameterAsManString", new SingularNamingConvention(), "Man", "Man"},
                new object[] {"WithSingularNamingConventionAndNameParameterAsMenString", new SingularNamingConvention(), "Men", "Man"},
                new object[] {"WithSingularNamingConventionAndNameParameterAsPersonString", new SingularNamingConvention(), "Person", "Person"},
                new object[] {"WithSingularNamingConventionAndNameParameterAsPeopleString", new SingularNamingConvention(), "People", "Person"},
                new object[] {"WithSingularNamingConventionAndNameParameterAsStatusString", new SingularNamingConvention(), "Status", "Status"},
                new object[] {"WithSingularNamingConventionAndNameParameterAsStatusesString", new SingularNamingConvention(), "Statuses", "Status"},
                new object[] {"WithSingularNamingConventionAndNameParameterAsTheDeprecatedArticleString", new SingularNamingConvention(), "TheDeprecatedArticle", "TheDeprecatedArticle"},
                new object[] {"WithSingularNamingConventionAndNameParameterAsTheDeprecatedArticlesString", new SingularNamingConvention(), "TheDeprecatedArticles", "TheDeprecatedArticle"},
                new object[] {"WithSingularNamingConventionAndNameParameterAsTheDeprecatedManString", new SingularNamingConvention(), "TheDeprecatedMan", "TheDeprecatedMan"},
                new object[] {"WithSingularNamingConventionAndNameParameterAsTheDeprecatedMenString", new SingularNamingConvention(), "TheDeprecatedMen", "TheDeprecatedMan"},
                new object[] {"WithSingularNamingConventionAndNameParameterAsTheDeprecatedPersonString", new SingularNamingConvention(), "TheDeprecatedPerson", "TheDeprecatedPerson"},
                new object[] {"WithSingularNamingConventionAndNameParameterAsTheDeprecatedPeopleString", new SingularNamingConvention(), "TheDeprecatedPeople", "TheDeprecatedPerson"},
                new object[] {"WithSingularNamingConventionAndNameParameterAsTheDeprecatedStatusString", new SingularNamingConvention(), "TheDeprecatedStatus", "TheDeprecatedStatus"},
                new object[] {"WithSingularNamingConventionAndNameParameterAsTheDeprecatedStatusesString", new SingularNamingConvention(), "TheDeprecatedStatuses", "TheDeprecatedStatus"},

                new object[] {"WithStandardMemberNamingConventionAndNameParameterAsNullString", new StandardMemberNamingConvention(), null, null},
                new object[] {"WithStandardMemberNamingConventionAndNameParameterAsEmptyString", new StandardMemberNamingConvention(), String.Empty, String.Empty},
                new object[] {"WithStandardMemberNamingConventionAndNameParameterAsName", new StandardMemberNamingConvention(), "Name", "name"},
                new object[] {"WithStandardMemberNamingConventionAndNameParameterAsUserName", new StandardMemberNamingConvention(), "UserName", "user-name"},
                new object[] {"WithStandardMemberNamingConventionAndNameParameterAsPreviousUserName", new StandardMemberNamingConvention(), "PreviousUserName", "previous-user-name"},
                new object[] {"WithStandardMemberNamingConventionAndNameParameterAsPreviousDeprecatedUserName", new StandardMemberNamingConvention(), "PreviousDeprecatedUserName", "previous-deprecated-user-name"},

                new object[] {"WithUpperCaseNamingConventionAndNameParameterAsNullString", new UpperCaseNamingConvention(), null, null},
                new object[] {"WithUpperCaseNamingConventionAndNameParameterAsEmptyString", new UpperCaseNamingConvention(), String.Empty, String.Empty},
                new object[] {"WithUpperCaseNamingConventionAndNameParameterAsName", new UpperCaseNamingConvention(), "Name", "NAME"},
                new object[] {"WithUpperCaseNamingConventionAndNameParameterAsUserName", new UpperCaseNamingConvention(), "UserName", "USERNAME"},
                new object[] {"WithUpperCaseNamingConventionAndNameParameterAsPreviousUserName", new UpperCaseNamingConvention(), "PreviousUserName", "PREVIOUSUSERNAME"},
            };
        // ReSharper restore UnusedMember.Global
        #endregion
    }
}
