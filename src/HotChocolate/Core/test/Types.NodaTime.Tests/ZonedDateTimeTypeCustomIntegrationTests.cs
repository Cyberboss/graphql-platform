using HotChocolate.Execution;
using NodaTime;
using NodaTime.Text;

namespace HotChocolate.Types.NodaTime.Tests;

public class ZonedDateTimeTypeCustomIntegrationTests
{
    private readonly IRequestExecutor _testExecutor;

    public ZonedDateTimeTypeCustomIntegrationTests()
    {
        var pattern = ZonedDateTimePattern.CreateWithInvariantCulture(
            "uuuu'-'MM'-'dd'T'HH':'mm':'ss' 'z' '(o<g>)",
            DateTimeZoneProviders.Tzdb);

        _testExecutor = SchemaBuilder.New()
            .AddQueryType<ZonedDateTimeTypeIntegrationTests.Schema.Query>()
            .AddMutationType<ZonedDateTimeTypeIntegrationTests.Schema.Mutation>()
            .AddNodaTime(typeof(ZonedDateTimeType))
            .AddType(new ZonedDateTimeType(pattern))
            .Create()
            .MakeExecutable();
    }

    [Fact]
    public void QueryReturns()
    {
        var result = _testExecutor.Execute("query { test: rome }");
        Assert.Equal(
            "2020-12-31T18:30:13 Asia/Kathmandu (+05:45)",
            result.ExpectSingleResult().Data!["test"]);
    }

    [Fact]
    public void QueryReturnsUtc()
    {
        var result = _testExecutor.Execute("query { test: utc }");
        Assert.Equal("2020-12-31T18:30:13 UTC (+00)", result.ExpectSingleResult().Data!["test"]);
    }

    [Fact]
    public void ParsesVariable()
    {
        var result = _testExecutor
            .Execute(OperationRequestBuilder.New()
                .SetDocument("mutation($arg: ZonedDateTime!) { test(arg: $arg) }")
                .SetVariableValues(new Dictionary<string, object?> { {"arg", "2020-12-31T19:30:13 Asia/Kathmandu (+05:45)" }, })
                .Build());

        Assert.Equal(
            "2020-12-31T19:40:13 Asia/Kathmandu (+05:45)",
            result.ExpectSingleResult().Data!["test"]);
    }

    [Fact]
    public void ParsesVariableWithUTC()
    {
        var result = _testExecutor
            .Execute(OperationRequestBuilder.New()
                .SetDocument("mutation($arg: ZonedDateTime!) { test(arg: $arg) }")
                .SetVariableValues(new Dictionary<string, object?> { {"arg", "2020-12-31T19:30:13 UTC (+00)" }, })
                .Build());

        Assert.Equal("2020-12-31T19:40:13 UTC (+00)", result.ExpectSingleResult().Data!["test"]);
    }

    [Fact]
    public void DoesntParseAnIncorrectVariable()
    {
        var result = _testExecutor
            .Execute(OperationRequestBuilder.New()
                .SetDocument("mutation($arg: ZonedDateTime!) { test(arg: $arg) }")
                .SetVariableValues(new Dictionary<string, object?> { {"arg", "2020-12-31T19:30:13 (UTC)" }, })
                .Build());

        Assert.Null(result.ExpectSingleResult().Data);
        Assert.Single(result.ExpectSingleResult().Errors!);
    }

    [Fact]
    public void ParsesLiteral()
    {
        var result = _testExecutor
            .Execute(OperationRequestBuilder.New()
                .SetDocument(
                    @"mutation
                    {
                        test(arg: ""2020-12-31T19:30:13 Asia/Kathmandu (+05:45)"")
                    }")
                .Build());

        Assert.Equal(
            "2020-12-31T19:40:13 Asia/Kathmandu (+05:45)",
            result.ExpectSingleResult().Data!["test"]);
    }

    [Fact]
    public void ParsesLiteralWithUtc()
    {
        var result = _testExecutor
            .Execute(OperationRequestBuilder.New()
                .SetDocument("mutation { test(arg: \"2020-12-31T19:30:13 UTC (+00)\") }")
                .Build());

        Assert.Equal("2020-12-31T19:40:13 UTC (+00)", result.ExpectSingleResult().Data!["test"]);
    }

    [Fact]
    public void DoesntParseIncorrectLiteral()
    {
        var result = _testExecutor
            .Execute(OperationRequestBuilder.New()
                .SetDocument("mutation { test(arg: \"2020-12-31T19:30:13 (UTC)\") }")
                .Build());

        Assert.Null(result.ExpectSingleResult().Data);
        Assert.Single(result.ExpectSingleResult().Errors!);
        Assert.Null(result.ExpectSingleResult().Errors![0].Code);
        Assert.Equal(
            "Unable to deserialize string to ZonedDateTime",
            result.ExpectSingleResult().Errors![0].Message);
    }
}
