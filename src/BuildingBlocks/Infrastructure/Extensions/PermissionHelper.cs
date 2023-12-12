using Shared.Common.Constants;

namespace Infrastructure.Extensions;

public static class PermissionHelper
{
    public static string GetPermission(FunctionCode functionCode, CommandCode commandCode)
    {
        return string.Join(".", functionCode, commandCode);
    }
}