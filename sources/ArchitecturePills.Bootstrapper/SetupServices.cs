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

using System.Reflection;
using Autofac;
using DustInTheWind.ArchitecturePills.Application.Initialize;
using DustInTheWind.ArchitecturePills.DataAccess;
using DustInTheWind.ArchitecturePills.Ports.DataAccess;
using DustInTheWind.ArchitecturePills.Presentation;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace DustInTheWind.ArchitecturePills.Bootstrapper;

internal static class SetupServices
{
    public static void Configure(ContainerBuilder containerBuilder)
    {
        Assembly applicationAssembly = typeof(InitializeRequest).Assembly;
        containerBuilder.RegisterMediatR(applicationAssembly);

        containerBuilder.RegisterType<InflationRepository>().As<IInflationRepository>();

        containerBuilder.RegisterType<MainWindow>().AsSelf();
        containerBuilder.RegisterType<MainViewModel>().AsSelf();
    }
}