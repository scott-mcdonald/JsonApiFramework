// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;

using JsonApiFramework.TestData.ClrResources.ComplexTypes;

namespace JsonApiFramework.TestData.ClrResources
{
    public static class SampleDrawings
    {
        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region ServiceModel Resources
        public static readonly Drawing Drawing = new Drawing
            {
                Id = 1,
                Name = "Drawing A",
                Lines = new List<Line>
                    {
                        new Line
                            {
                                Point1 = new Point
                                    {
                                        X = 10,
                                        Y = 20,
                                        CustomData = new CustomData
                                            {
                                                Collection = new List<CustomProperty>
                                                    {
                                                        new CustomProperty
                                                            {
                                                                Name = "Notes",
                                                                Value = "This was my first point!"
                                                            },
                                                        new CustomProperty {Name = "Rating", Value = "3 stars"}
                                                    }
                                            }
                                    },
                                Point2 = new Point {X = 20, Y = 40}
                            },
                        new Line
                            {
                                Point1 = new Point {X = 20, Y = 10},
                                Point2 = new Point {X = 40, Y = 20}
                            },
                        new Line
                            {
                                Point1 = new Point {X = 100, Y = 200},
                                Point2 = new Point {X = 200, Y = 400},
                                CustomData = new CustomData
                                    {
                                        Collection = new List<CustomProperty>
                                            {
                                                new CustomProperty
                                                    {
                                                        Name = "Notes",
                                                        Value = "Move this line to match up with Drawing B"
                                                    },
                                                new CustomProperty {Name = "Email", Value = "smcdonald@drawings.com"}
                                            }
                                    }
                            },
                        new Line
                            {
                                Point1 = new Point {X = 200, Y = 100},
                                Point2 = new Point {X = 400, Y = 200}
                            },
                    },
                Polygons = new List<Polygon>
                    {
                        new Polygon
                            {
                                Points = new List<Point>
                                    {
                                        new Point {X = 24, Y = 42},
                                        new Point {X = 42, Y = 24},
                                        new Point {X = 86, Y = 68},
                                        new Point {X = 68, Y = 86},
                                    }
                            },
                        new Polygon
                            {
                                Points = new List<Point>
                                    {
                                        new Point {X = 10, Y = 20},
                                        new Point {X = 20, Y = 10},
                                        new Point {X = 100, Y = 200},
                                        new Point {X = 200, Y = 100},
                                    },
                                CustomData = new CustomData
                                    {
                                        Collection = new List<CustomProperty>
                                            {
                                                new CustomProperty {Name = "Notes", Value = "Move this polygon"},
                                                new CustomProperty {Name = "Email", Value = "smcdonald@drawings.com"}
                                            }
                                    }
                            },
                    },
                CustomData = new CustomData
                    {
                        Collection = new List<CustomProperty>
                            {
                                new CustomProperty {Name = "Rating", Value = "5 stars"},
                                new CustomProperty {Name = "Email", Value = "smcdonald@drawings.com"}
                            }
                    }
            };

        public static readonly Drawing Drawing1 = new Drawing
            {
                Id = 1,
                Name = "Drawing A",
                Lines = new List<Line>
                    {
                        new Line
                            {
                                Point1 = new Point
                                    {
                                        X = 10,
                                        Y = 20,
                                        CustomData = new CustomData
                                            {
                                                Collection = new List<CustomProperty>
                                                    {
                                                        new CustomProperty
                                                            {
                                                                Name = "Notes",
                                                                Value = "This was my first point!"
                                                            },
                                                        new CustomProperty {Name = "Rating", Value = "3 stars"}
                                                    }
                                            }
                                    },
                                Point2 = new Point {X = 20, Y = 40}
                            },
                        new Line
                            {
                                Point1 = new Point {X = 20, Y = 10},
                                Point2 = new Point {X = 40, Y = 20}
                            },
                        new Line
                            {
                                Point1 = new Point {X = 100, Y = 200},
                                Point2 = new Point {X = 200, Y = 400},
                                CustomData = new CustomData
                                    {
                                        Collection = new List<CustomProperty>
                                            {
                                                new CustomProperty
                                                    {
                                                        Name = "Notes",
                                                        Value = "Move this line to match up with Drawing B"
                                                    },
                                                new CustomProperty {Name = "Email", Value = "smcdonald@drawings.com"}
                                            }
                                    }
                            },
                        new Line
                            {
                                Point1 = new Point {X = 200, Y = 100},
                                Point2 = new Point {X = 400, Y = 200}
                            },
                    },
                Polygons = new List<Polygon>
                    {
                        new Polygon
                            {
                                Points = new List<Point>
                                    {
                                        new Point {X = 24, Y = 42},
                                        new Point {X = 42, Y = 24},
                                        new Point {X = 86, Y = 68},
                                        new Point {X = 68, Y = 86},
                                    }
                            },
                        new Polygon
                            {
                                Points = new List<Point>
                                    {
                                        new Point {X = 10, Y = 20},
                                        new Point {X = 20, Y = 10},
                                        new Point {X = 100, Y = 200},
                                        new Point {X = 200, Y = 100},
                                    },
                                CustomData = new CustomData
                                    {
                                        Collection = new List<CustomProperty>
                                            {
                                                new CustomProperty {Name = "Notes", Value = "Move this polygon"},
                                                new CustomProperty {Name = "Email", Value = "smcdonald@drawings.com"}
                                            }
                                    }
                            },
                    },
                CustomData = new CustomData
                    {
                        Collection = new List<CustomProperty>
                            {
                                new CustomProperty {Name = "Rating", Value = "5 stars"},
                                new CustomProperty {Name = "Email", Value = "smcdonald@drawings.com"}
                            }
                    }
            };

        public static readonly Drawing Drawing2 = new Drawing
            {
                Id = 2,
                Name = "Drawing B",
                Lines = new List<Line>
                    {
                        new Line
                            {
                                Point1 = new Point {X = -2, Y = -1},
                                Point2 = new Point {X = -4, Y = -2}
                            },
                        new Line
                            {
                                Point1 = new Point {X = -10, Y = -20},
                                Point2 = new Point {X = -20, Y = -40},
                                CustomData = new CustomData
                                    {
                                        Collection = new List<CustomProperty>
                                            {
                                                new CustomProperty
                                                    {
                                                        Name = "Notes",
                                                        Value = "Why are these negative?"
                                                    }
                                            }
                                    }
                            },
                        new Line
                            {
                                Point1 = new Point {X = -20, Y = -10},
                                Point2 = new Point {X = -40, Y = -20}
                            },
                    },
                Polygons = new List<Polygon>
                    {
                        new Polygon
                            {
                                Points = new List<Point>
                                    {
                                        new Point {X = -1, Y = -2},
                                        new Point {X = -2, Y = -1},
                                        new Point {X = -10, Y = -20},
                                        new Point {X = -20, Y = -10},
                                    },
                                CustomData = new CustomData
                                    {
                                        Collection = new List<CustomProperty>
                                            {
                                                new CustomProperty {Name = "Notes", Value = "Copy this polygon"},
                                                new CustomProperty {Name = "Email", Value = "emcdonald@drawings.com"}
                                            }
                                    }
                            },
                    },
                CustomData = new CustomData
                    {
                        Collection = new List<CustomProperty>
                            {
                                new CustomProperty {Name = "Rating", Value = "4.5 stars"},
                                new CustomProperty {Name = "Email", Value = "emcdonald@drawings.com"}
                            }
                    }
            };
        #endregion
    }
}