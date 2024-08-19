﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonApiFramework.Json;

namespace JsonApiFramework.JsonApi;

/// <summary>
/// Represents a json:api compliant link.
/// </summary>
/// <see cref="http://jsonapi.org"/>
[JsonConverter(typeof(LinkConverter))]
public class Link : JsonObject
    , IGetMeta
    , ISetMeta
{
    // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
    #region Constructors
    public Link()
    { }

    public Link(string hRef)
    {
        Contract.Requires(string.IsNullOrWhiteSpace(hRef) == false);

        this.HRef = hRef;
    }

    public Link(string hRef, JsonElement meta)
    {
        Contract.Requires(string.IsNullOrWhiteSpace(hRef) == false);

        this.HRef = hRef;
        this.Meta = meta;
    }
    #endregion

    // PUBLIC PROPERTIES ////////////////////////////////////////////////
    #region JSON Properties
    public string HRef { get; set; }
    public Meta Meta { get; set; }
    #endregion

    #region Non-JSON Properties
    public IEnumerable<string> PathSegments
    {
        get { return this.Uri.GetPathSegments(); }
    }

    public Uri Uri
    {
        get
        {
            return !string.IsNullOrWhiteSpace(this.HRef) ? new Uri(this.HRef, UriKind.RelativeOrAbsolute) : null;
        }
        set
        {
            this.HRef = value != null ? value.ToString() : null;
        }
    }
    #endregion

    // PUBLIC METHODS ///////////////////////////////////////////////////
    #region Object Overrides
    public override string ToString()
    { return this.HRef ?? CoreStrings.NullText; }
    #endregion

    // PUBLIC OPERATORS /////////////////////////////////////////////////
    #region Conversion Operators
    public static implicit operator string(Link link)
    {
        return link != null
            ? link.HRef
            : null;
    }

    public static implicit operator Link(string href)
    {
        return new Link
            {
                HRef = href
            };
    }
    #endregion

    // PUBLIC FIELDS ////////////////////////////////////////////////////
    #region Fields
    public static readonly Link Empty = new Link();
    #endregion

    // PRIVATE METHODS //////////////////////////////////////////////////
    #region Methods
    private static IReadOnlyList<string> PathSegmentsFromUriOriginalString(Uri uri)
    {
        Contract.Requires(uri != null);

        var originalString = uri.OriginalString;
        if (string.IsNullOrWhiteSpace(originalString))
            return Enumerable.Empty<string>()
                             .ToList();

        var pathSegments = originalString.Split('/', '\\')
                                         .ToList();
        return pathSegments;
    }

    private static IReadOnlyList<string> PathSegmentsFromSegments(Uri uri)
    {
        var segments = uri.Segments;
        if (segments == null)
            return Enumerable.Empty<string>()
                             .ToList();

        var pathSegments = segments.Select(x => x.Trim('/', '\\'))
                                   .Where(x => !string.IsNullOrWhiteSpace(x))
                                   .ToList();
        return pathSegments;
    }
    #endregion
}