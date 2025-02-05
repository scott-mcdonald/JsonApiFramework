// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.Conventions;
using JsonApiFramework.Conventions.Internal;
using JsonApiFramework.XUnit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.Conventions;

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
    [MemberData(nameof(ApplyTestData))]
    public void TestNamingConventionApply(string testName, string namingConventionName, string originalOldName, string expectedNewName)
    {
        this.Output.WriteLine("Test Name: {0}", testName);
        this.Output.WriteLine(string.Empty);

        // Arrange
        this.Output.WriteLine("Original Old Name");
        this.Output.WriteLine(originalOldName ?? string.Empty);

        this.Output.WriteLine(string.Empty);

        this.Output.WriteLine("Expected New Name");
        this.Output.WriteLine(expectedNewName ?? string.Empty);

        // Act
        var namingConvention = NamingConventionFactoryDictionary[namingConventionName];
        var actualNewName = namingConvention.Apply(originalOldName);

        this.Output.WriteLine(string.Empty);

        this.Output.WriteLine("Actual New Name");
        this.Output.WriteLine(actualNewName ?? string.Empty);

        // Assert
        Assert.Equal(expectedNewName, actualNewName);
    }
    #endregion

    // PUBLIC FIELDS ////////////////////////////////////////////////////
    #region Test Data
    // ReSharper disable UnusedMember.Global
    public static readonly IEnumerable<object[]> ApplyTestData = new[]
        {
            new object[] {"WithLowerCaseNamingConventionAndNameParameterAsNullString", nameof(LowerCaseNamingConvention), null, null},
            new object[] {"WithLowerCaseNamingConventionAndNameParameterAsEmptyString", nameof(LowerCaseNamingConvention), string.Empty, string.Empty},
            new object[] {"WithLowerCaseNamingConventionAndNameParameterAsName", nameof(LowerCaseNamingConvention), "Name", "name"},
            new object[] {"WithLowerCaseNamingConventionAndNameParameterAsUserName", nameof(LowerCaseNamingConvention), "UserName", "username"},
            new object[] {"WithLowerCaseNamingConventionAndNameParameterAsPreviousUserName", nameof(LowerCaseNamingConvention), "PreviousUserName", "previoususername"},

            new object[] {"WithPluralNamingConventionAndNameParameterAsNullString", nameof(PluralNamingConvention), null, null},
            new object[] {"WithPluralNamingConventionAndNameParameterAsEmptyString", nameof(PluralNamingConvention), string.Empty, string.Empty},
            new object[] {"WithPluralNamingConventionAndNameParameterAsArticleString", nameof(PluralNamingConvention), "Article", "Articles"},
            new object[] {"WithPluralNamingConventionAndNameParameterAsArticlesString", nameof(PluralNamingConvention), "Articles", "Articles"},
            new object[] {"WithPluralNamingConventionAndNameParameterAsManString", nameof(PluralNamingConvention), "Man", "Men"},
            new object[] {"WithPluralNamingConventionAndNameParameterAsMenString", nameof(PluralNamingConvention), "Men", "Men"},
            new object[] {"WithPluralNamingConventionAndNameParameterAsPersonString", nameof(PluralNamingConvention), "Person", "People"},
            new object[] {"WithPluralNamingConventionAndNameParameterAsPeopleString", nameof(PluralNamingConvention), "People", "People"},
            new object[] {"WithPluralNamingConventionAndNameParameterAsStatusString", nameof(PluralNamingConvention), "Status", "Statuses"},
            new object[] {"WithPluralNamingConventionAndNameParameterAsStatusesString", nameof(PluralNamingConvention), "Statuses", "Statuses"},
            new object[] {"WithPluralNamingConventionAndNameParameterAsTheDeprecatedArticleString", nameof(PluralNamingConvention), "TheDeprecatedArticle", "TheDeprecatedArticles"},
            new object[] {"WithPluralNamingConventionAndNameParameterAsTheDeprecatedArticlesString", nameof(PluralNamingConvention), "TheDeprecatedArticles", "TheDeprecatedArticles"},
            new object[] {"WithPluralNamingConventionAndNameParameterAsTheDeprecatedManString", nameof(PluralNamingConvention), "TheDeprecatedMan", "TheDeprecatedMen"},
            new object[] {"WithPluralNamingConventionAndNameParameterAsTheDeprecatedMenString", nameof(PluralNamingConvention), "TheDeprecatedMen", "TheDeprecatedMen"},
            new object[] {"WithPluralNamingConventionAndNameParameterAsTheDeprecatedPersonString", nameof(PluralNamingConvention), "TheDeprecatedPerson", "TheDeprecatedPeople"},
            new object[] {"WithPluralNamingConventionAndNameParameterAsTheDeprecatedPeopleString", nameof(PluralNamingConvention), "TheDeprecatedPeople", "TheDeprecatedPeople"},
            new object[] {"WithPluralNamingConventionAndNameParameterAsTheDeprecatedStatusString", nameof(PluralNamingConvention), "TheDeprecatedStatus", "TheDeprecatedStatuses"},
            new object[] {"WithPluralNamingConventionAndNameParameterAsTheDeprecatedStatusesString", nameof(PluralNamingConvention), "TheDeprecatedStatuses", "TheDeprecatedStatuses"},

            new object[] {"WithSingularNamingConventionAndNameParameterAsNullString", nameof(SingularNamingConvention), null, null},
            new object[] {"WithSingularNamingConventionAndNameParameterAsEmptyString", nameof(SingularNamingConvention), string.Empty, string.Empty},
            new object[] {"WithSingularNamingConventionAndNameParameterAsArticleString", nameof(SingularNamingConvention), "Article", "Article"},
            new object[] {"WithSingularNamingConventionAndNameParameterAsArticlesString", nameof(SingularNamingConvention), "Articles", "Article"},
            new object[] {"WithSingularNamingConventionAndNameParameterAsManString", nameof(SingularNamingConvention), "Man", "Man"},
            new object[] {"WithSingularNamingConventionAndNameParameterAsMenString", nameof(SingularNamingConvention), "Men", "Man"},
            new object[] {"WithSingularNamingConventionAndNameParameterAsPersonString", nameof(SingularNamingConvention), "Person", "Person"},
            new object[] {"WithSingularNamingConventionAndNameParameterAsPeopleString", nameof(SingularNamingConvention), "People", "Person"},
            new object[] {"WithSingularNamingConventionAndNameParameterAsStatusString", nameof(SingularNamingConvention), "Status", "Status"},
            new object[] {"WithSingularNamingConventionAndNameParameterAsStatusesString", nameof(SingularNamingConvention), "Statuses", "Status"},
            new object[] {"WithSingularNamingConventionAndNameParameterAsTheDeprecatedArticleString", nameof(SingularNamingConvention), "TheDeprecatedArticle", "TheDeprecatedArticle"},
            new object[] {"WithSingularNamingConventionAndNameParameterAsTheDeprecatedArticlesString", nameof(SingularNamingConvention), "TheDeprecatedArticles", "TheDeprecatedArticle"},
            new object[] {"WithSingularNamingConventionAndNameParameterAsTheDeprecatedManString", nameof(SingularNamingConvention), "TheDeprecatedMan", "TheDeprecatedMan"},
            new object[] {"WithSingularNamingConventionAndNameParameterAsTheDeprecatedMenString", nameof(SingularNamingConvention), "TheDeprecatedMen", "TheDeprecatedMan"},
            new object[] {"WithSingularNamingConventionAndNameParameterAsTheDeprecatedPersonString", nameof(SingularNamingConvention), "TheDeprecatedPerson", "TheDeprecatedPerson"},
            new object[] {"WithSingularNamingConventionAndNameParameterAsTheDeprecatedPeopleString", nameof(SingularNamingConvention), "TheDeprecatedPeople", "TheDeprecatedPerson"},
            new object[] {"WithSingularNamingConventionAndNameParameterAsTheDeprecatedStatusString", nameof(SingularNamingConvention), "TheDeprecatedStatus", "TheDeprecatedStatus"},
            new object[] {"WithSingularNamingConventionAndNameParameterAsTheDeprecatedStatusesString", nameof(SingularNamingConvention), "TheDeprecatedStatuses", "TheDeprecatedStatus"},

            new object[] {"WithStandardMemberNamingConventionAndNameParameterAsNullString", nameof(StandardMemberNamingConvention), null, null},
            new object[] {"WithStandardMemberNamingConventionAndNameParameterAsEmptyString", nameof(StandardMemberNamingConvention), string.Empty, string.Empty},
            new object[] {"WithStandardMemberNamingConventionAndNameParameterAsName", nameof(StandardMemberNamingConvention), "Name", "name"},
            new object[] {"WithStandardMemberNamingConventionAndNameParameterAsUserName", nameof(StandardMemberNamingConvention), "UserName", "userName"},
            new object[] {"WithStandardMemberNamingConventionAndNameParameterAsPreviousUserName", nameof(StandardMemberNamingConvention), "PreviousUserName", "previousUserName"},
            new object[] {"WithStandardMemberNamingConventionAndNameParameterAsPreviousDeprecatedUserName", nameof(StandardMemberNamingConvention), "PreviousDeprecatedUserName", "previousDeprecatedUserName"},

            new object[] {"WithUpperCaseNamingConventionAndNameParameterAsNullString", nameof(UpperCaseNamingConvention), null, null},
            new object[] {"WithUpperCaseNamingConventionAndNameParameterAsEmptyString", nameof(UpperCaseNamingConvention), string.Empty, string.Empty},
            new object[] {"WithUpperCaseNamingConventionAndNameParameterAsName", nameof(UpperCaseNamingConvention), "Name", "NAME"},
            new object[] {"WithUpperCaseNamingConventionAndNameParameterAsUserName", nameof(UpperCaseNamingConvention), "UserName", "USERNAME"},
            new object[] {"WithUpperCaseNamingConventionAndNameParameterAsPreviousUserName", nameof(UpperCaseNamingConvention), "PreviousUserName", "PREVIOUSUSERNAME"},

            new object[] {"WithCamelCaseNamingConventionAndNameParameterAsNullString", nameof(CamelCaseNamingConvention), null, null},
            new object[] {"WithCamelCaseNamingConventionAndNameParameterAsEmptyString", nameof(CamelCaseNamingConvention), string.Empty, string.Empty},
            new object[] {"WithCamelCaseNamingConventionAndNameParameterAsName", nameof(CamelCaseNamingConvention), "Name", "name"},
            new object[] {"WithCamelCaseNamingConventionAndNameParameterAsUserName", nameof(CamelCaseNamingConvention), "UserName", "userName"},
            new object[] {"WithCamelCaseNamingConventionAndNameParameterAsPreviousUserName", nameof(CamelCaseNamingConvention), "PreviousUserName", "previousUserName"},
            new object[] {"WithCamelCaseNamingConventionAndNameParameterAsUserNameWithUnderscore", nameof(CamelCaseNamingConvention), "User_Name", "userName"},
            new object[] {"WithCamelCaseNamingConventionAndNameParameterAsPreviousUserNameWithUnderscores", nameof(CamelCaseNamingConvention), "Previous_User_Name", "previousUserName"},

            new object[] {"WithPascalCaseNamingConventionAndNameParameterAsNullString", nameof(PascalCaseNamingConvention), null, null},
            new object[] {"WithPascalCaseNamingConventionAndNameParameterAsEmptyString", nameof(PascalCaseNamingConvention), string.Empty, string.Empty},
            new object[] {"WithPascalCaseNamingConventionAndNameParameterAsName", nameof(PascalCaseNamingConvention), "Name", "Name"},
            new object[] {"WithPascalCaseNamingConventionAndNameParameterAsUserName", nameof(PascalCaseNamingConvention), "UserName", "UserName"},
            new object[] {"WithPascalCaseNamingConventionAndNameParameterAsPreviousUserName", nameof(PascalCaseNamingConvention), "PreviousUserName", "PreviousUserName"},
            new object[] {"WithPascalCaseNamingConventionAndNameParameterAsUserNameWithUnderscore", nameof(PascalCaseNamingConvention), "User_Name", "UserName"},
            new object[] {"WithPascalCaseNamingConventionAndNameParameterAsPreviousUserNameWithUnderscores", nameof(PascalCaseNamingConvention), "Previous_User_Name", "PreviousUserName"},
        };
    // ReSharper restore UnusedMember.Global

    private static readonly IReadOnlyDictionary<string, INamingConvention> NamingConventionFactoryDictionary = new Dictionary<string, INamingConvention>
    {
        { nameof(LowerCaseNamingConvention),        new LowerCaseNamingConvention() },
        { nameof(PluralNamingConvention),           new PluralNamingConvention() },
        { nameof(SingularNamingConvention),         new SingularNamingConvention() },
        { nameof(StandardMemberNamingConvention),   new StandardMemberNamingConvention() },
        { nameof(UpperCaseNamingConvention),        new UpperCaseNamingConvention() },
        { nameof(CamelCaseNamingConvention),        new CamelCaseNamingConvention() },
        { nameof(PascalCaseNamingConvention),       new PascalCaseNamingConvention() },
    };
    #endregion
}
