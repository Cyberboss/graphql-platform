using HotChocolate.Execution;
using NodaTime;

namespace HotChocolate.Types.NodaTime.Tests
{
    public class OffsetDateTypeIntegrationTests
    {
        public static class Schema
        {
            public class Query
            {
                public OffsetDate Hours
                    => new OffsetDate(
                        LocalDate.FromDateTime(new DateTime(2020, 12, 31, 18, 30, 13)),
                        Offset.FromHours(2)).WithCalendar(CalendarSystem.Gregorian);

                public OffsetDate HoursAndMinutes
                    => new OffsetDate(
                        LocalDate.FromDateTime(new DateTime(2020, 12, 31, 18, 30, 13)),
                        Offset.FromHoursAndMinutes(2, 35)).WithCalendar(CalendarSystem.Gregorian);
            }

            public class Mutation
            {
                public OffsetDate Test(OffsetDate arg) => arg;
            }
        }

        private readonly IRequestExecutor _testExecutor =
            SchemaBuilder.New()
                .AddQueryType<Schema.Query>()
                .AddMutationType<Schema.Mutation>()
                .AddNodaTime()
                .Create()
                .MakeExecutable();

        [Fact]
        public void QueryReturns()
        {
            var result = _testExecutor.Execute("query { test: hours }");
            Assert.Equal("2020-12-31+02", result.ExpectSingleResult().Data!["test"]);
        }

        [Fact]
        public void QueryReturnsWithMinutes()
        {
            var result = _testExecutor.Execute("query { test: hoursAndMinutes }");
            Assert.Equal("2020-12-31+02:35", result.ExpectSingleResult().Data!["test"]);
        }

        [Fact]
        public void ParsesVariable()
        {
            var result = _testExecutor
                .Execute(OperationRequestBuilder.New()
                    .SetDocument("mutation($arg: OffsetDate!) { test(arg: $arg) }")
                    .SetVariableValues(new Dictionary<string, object?> { {"arg", "2020-12-31+02" }, })
                    .Build());
            Assert.Equal("2020-12-31+02", result.ExpectSingleResult().Data!["test"]);
        }

        [Fact]
        public void ParsesVariableWithMinutes()
        {
            var result = _testExecutor
                .Execute(OperationRequestBuilder.New()
                    .SetDocument("mutation($arg: OffsetDate!) { test(arg: $arg) }")
                    .SetVariableValues(new Dictionary<string, object?> { {"arg", "2020-12-31+02:35" }, })
                    .Build());
            Assert.Equal("2020-12-31+02:35", result.ExpectSingleResult().Data!["test"]);
        }

        [Fact]
        public void DoesntParseAnIncorrectVariable()
        {
            var result = _testExecutor
                .Execute(OperationRequestBuilder.New()
                    .SetDocument("mutation($arg: OffsetDate!) { test(arg: $arg) }")
                    .SetVariableValues(new Dictionary<string, object?> { {"arg", "2020-12-31" }, })
                    .Build());
            Assert.Null(result.ExpectSingleResult().Data);
            Assert.Single(result.ExpectSingleResult().Errors!);
        }

        [Fact]
        public void ParsesLiteral()
        {
            var result = _testExecutor
                .Execute(OperationRequestBuilder.New()
                    .SetDocument("mutation { test(arg: \"2020-12-31+02\") }")
                    .Build());
            Assert.Equal("2020-12-31+02", result.ExpectSingleResult().Data!["test"]);
        }

        [Fact]
        public void ParsesLiteralWithMinutes()
        {
            var result = _testExecutor
                .Execute(OperationRequestBuilder.New()
                    .SetDocument("mutation { test(arg: \"2020-12-31+02:35\") }")
                    .Build());
            Assert.Equal("2020-12-31+02:35", result.ExpectSingleResult().Data!["test"]);
        }

        [Fact]
        public void DoesntParseIncorrectLiteral()
        {
            var result = _testExecutor
                .Execute(OperationRequestBuilder.New()
                    .SetDocument("mutation { test(arg: \"2020-12-31\") }")
                    .Build());
            Assert.Null(result.ExpectSingleResult().Data);
            Assert.Single(result.ExpectSingleResult().Errors!);
            Assert.Null(result.ExpectSingleResult().Errors![0].Code);
            Assert.Equal(
                "Unable to deserialize string to OffsetDate",
                result.ExpectSingleResult().Errors![0].Message);
        }

        [Fact]
        public void PatternEmptyThrowSchemaException()
        {
            static object Call() => new OffsetDateType([]);
            Assert.Throws<SchemaException>(Call);
        }
    }
}
