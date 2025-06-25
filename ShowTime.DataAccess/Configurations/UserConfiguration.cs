﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.Configurations
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);
            builder.HasIndex(u => u.Email)
                .IsUnique();
            builder.Property(u => u.Password)
                .IsRequired();
            builder.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
