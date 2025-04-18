using HotChocolate.Types.Mutable;

namespace HotChocolate.Fusion.Extensions;

internal static class MutableSchemaDefinitionExtensions
{
    public static bool IsRootOperationType(
        this MutableSchemaDefinition schema,
        MutableObjectTypeDefinition type)
    {
        return
            schema.QueryType == type
            || schema.MutationType == type
            || schema.SubscriptionType == type;
    }
}
