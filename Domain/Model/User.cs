﻿namespace Domain.Model;

public record User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}