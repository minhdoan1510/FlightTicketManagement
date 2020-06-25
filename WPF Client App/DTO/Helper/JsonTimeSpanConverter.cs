using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;

namespace System.Text.Json.Serialization
{
	/// <summary>
	/// <see cref="JsonConverterFactory"/> to convert <see cref="TimeSpan"/> to and from strings. Supports <see cref="Nullable{TimeSpan}"/>.
	/// </summary>
	/// <remarks>
	/// TimeSpans are transposed using the constant ("c") format specifier: [-][d.]hh:mm:ss[.fffffff].
	/// </remarks>
	public class JsonTimeSpanConverter : JsonConverterFactory
	{
		/// <inheritdoc/>
		public override bool CanConvert(Type typeToConvert)
		{
			// Don't perform a typeToConvert == null check for performance. Trust our callers will be nice.
#pragma warning disable CA1062 // Validate arguments of public methods
			return typeToConvert == typeof(TimeSpan);
				
#pragma warning restore CA1062 // Validate arguments of public methods
		}

		/// <inheritdoc/>
		public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			// Don't perform a typeToConvert == null check for performance. Trust our callers will be nice.
#pragma warning disable CA1062 // Validate arguments of public methods
			return  new JsonStandardTimeSpanConverter();
#pragma warning restore CA1062 // Validate arguments of public methods
		}

		internal class JsonStandardTimeSpanConverter : JsonConverter<TimeSpan>
		{
			/// <inheritdoc/>
			public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				return reader.TokenType != JsonTokenType.String
					? throw new JsonException()
					: TimeSpan.ParseExact(reader.GetString(), "c", CultureInfo.InvariantCulture);
			}

			/// <inheritdoc/>
			public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
				=> writer.WriteStringValue(value.ToString("c", CultureInfo.InvariantCulture));
		}


	}
}