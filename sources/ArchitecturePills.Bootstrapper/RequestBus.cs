// Architecture Pills
// Copyright (C) 2022 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using DustInTheWind.ArchitecturePills.Infrastructure;
using MediatR;

namespace DustInTheWind.ArchitecturePills.Bootstrapper;

public class RequestBus : IRequestBus
{
    private readonly ILifetimeScope lifetimeScope;

    public RequestBus(ILifetimeScope lifetimeScope)
    {
        this.lifetimeScope = lifetimeScope ?? throw new ArgumentNullException(nameof(lifetimeScope));
    }

    public async Task<TResponse?> Send<TRequest, TResponse>([DisallowNull] TRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        await using ILifetimeScope localLifetimeScope = lifetimeScope.BeginLifetimeScope();

        IMediator mediator = localLifetimeScope.Resolve<IMediator>();

        if (request is IRequest<TResponse> mediatorRequest)
            return await mediator.Send(mediatorRequest, cancellationToken);

        object? responseObject = await mediator.Send(request, cancellationToken);

        if (responseObject is TResponse response)
            return response;

        string? responseTypeFullName = typeof(TResponse).FullName;
        throw new Exception($"The response from MediatR is different than the requested one: {responseTypeFullName}");
    }

    public async Task Send<TRequest>([DisallowNull] TRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        await using ILifetimeScope localLifetimeScope = lifetimeScope.BeginLifetimeScope();

        IMediator mediator = localLifetimeScope.Resolve<IMediator>();

        if (request is IRequest mediatorRequest)
            await mediator.Send(mediatorRequest, cancellationToken);
        else
            await mediator.Send(request, cancellationToken);
    }
}