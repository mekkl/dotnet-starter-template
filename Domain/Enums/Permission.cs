namespace Domain.Enums;

public enum Permission
{
    AccessMembers = 200,
    ReadMember = 201,
    
    AccessPermission = 300,
    ReadPermission = 301,
    WritePermission = 302,
    DeletePermission = 303,
    ListPermission = 304,
    
    AccessRole = 400,
    ReadRole = 401,
    WriteRole = 402,
    DeleteRole = 403,
    ListRole = 404,
}