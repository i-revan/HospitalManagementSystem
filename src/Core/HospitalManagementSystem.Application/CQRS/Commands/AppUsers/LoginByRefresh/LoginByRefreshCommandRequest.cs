﻿namespace HospitalManagementSystem.Application.CQRS.Commands.AppUsers.LoginByRefresh;
public record LoginByRefreshCommandRequest(string RefreshToken):IRequest<LoginByRefreshCommandResponse>;