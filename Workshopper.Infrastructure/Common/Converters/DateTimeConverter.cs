﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Workshopper.Infrastructure.Common.Converters;

internal class DateTimeOffsetConverter : ValueConverter<DateTimeOffset, DateTimeOffset>
{
    public DateTimeOffsetConverter()
        : base(
            d => d.ToUniversalTime(),
            d => d.ToUniversalTime())
    {
    }
}