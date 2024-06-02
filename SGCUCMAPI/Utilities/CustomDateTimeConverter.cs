﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SGCUCMAPI.Utilities
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string format = "dd/MM/yyyy";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), format, null);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(format));
        }
    }
}
