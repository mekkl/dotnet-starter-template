﻿using Domain.Common;

namespace Domain.Model;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}