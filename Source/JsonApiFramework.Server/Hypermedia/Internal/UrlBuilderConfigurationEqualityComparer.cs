// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.
using System.Diagnostics.CodeAnalysis;

using JsonApiFramework.Http;

namespace JsonApiFramework.Server.Hypermedia.Internal;

internal class UrlBuilderConfigurationEqualityComparer : IEqualityComparer<IUrlBuilderConfiguration>
{
    private static EqualityComparer<string> StringEqualityComparer { get; } = EqualityComparer<string>.Default;

    public bool Equals(IUrlBuilderConfiguration? x, IUrlBuilderConfiguration? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (x is null) return false;
        if (y is null) return false;
        if (x.GetType() != y.GetType()) return false;

        return StringEqualityComparer.Equals(x.Scheme, y.Scheme)
            && StringEqualityComparer.Equals(x.Host, y.Host)
            && Nullable.Equals(x.Port, y.Port)
            && x.RootPathSegments.SafeToReadOnlyCollection().SequenceEqual(y.RootPathSegments.SafeToReadOnlyCollection());
    }

    public int GetHashCode([DisallowNull] IUrlBuilderConfiguration obj)
    {
        return obj.GetHashCode();
    }
}
