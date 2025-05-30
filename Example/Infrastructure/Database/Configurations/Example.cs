﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Example.Infrastructure;

internal sealed class ExampleEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Example>
{
    public void Configure(EntityTypeBuilder<Domain.Example> builder)
    {
        builder
            .Property(example => example.Id)
            .ValueGeneratedOnAdd();
    }
}