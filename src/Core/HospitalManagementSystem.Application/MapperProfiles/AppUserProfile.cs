﻿using AutoMapper;
using HospitalManagementSystem.Application.DTOs.Users;
using HospitalManagementSystem.Domain.Entities.Identity;

namespace HospitalManagementSystem.Application.MapperProfiles
{
    internal class AppUserProfile:Profile
    {
        public AppUserProfile()
        {
            CreateMap<RegisterDto, AppUser>();
        }
    }
}