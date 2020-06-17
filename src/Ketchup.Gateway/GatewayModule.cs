﻿using System;
using System.Collections.Generic;
using Autofac;
using Ketchup.Core;
using Ketchup.Core.Modules;
using Ketchup.Core.Utilities;
using Ketchup.Gateway.Internal;
using Ketchup.Gateway.Internal.Implementation;
using Ketchup.Permission;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Ketchup.Gateway
{
    public class GatewayModule : KernelModule
    {
        public override void Initialize(KetchupPlatformContainer builder)
        {
            ServiceLocator.GetService<IGatewayProvider>()
                .InitGatewaySetting()
                .SettingKongService()
                .MapServiceClient(ClientMaps);
        }

        public override void MapGrpcService(IEndpointRouteBuilder endpointRoute)
        {
            endpointRoute.MapControllers();
        }

        protected override void RegisterModule(ContainerBuilderWrapper builder)
        {
            builder.ContainerBuilder.RegisterType<GatewayProvider>().As<IGatewayProvider>().SingleInstance();
        }

        private Dictionary<string, Type> ClientMaps()
        {
            return new Dictionary<string, Type>()
            {
                {"menus.PageSerachMenu", typeof(RpcMenu.RpcMenuClient)},
                {"menus.CreateOrEditMenu", typeof(RpcMenu.RpcMenuClient)},
                {"menus.GetMenusByRole", typeof(RpcMenu.RpcMenuClient)},
                {"menus.GetMenusSetRole", typeof(RpcMenu.RpcMenuClient)},
                {"menus.RemoveMenu", typeof(RpcMenu.RpcMenuClient)},

                {"operates.PageSerachOperate", typeof(RpcOperate.RpcOperateClient)},
                {"operates.CreateOrEditOperate", typeof(RpcOperate.RpcOperateClient)},
                {"operates.GetMenuOfOperate", typeof(RpcOperate.RpcOperateClient)},
                {"operates.RemoveOperate", typeof(RpcOperate.RpcOperateClient)},

                {"sysRoles.PageSerachRole", typeof(RpcRole.RpcRoleClient)},
                {"sysRoles.CreateOrEditRole", typeof(RpcRole.RpcRoleClient)},
                {"sysRoles.RemoveRole", typeof(RpcRole.RpcRoleClient)},
                {"sysRoles.SetRolePermission", typeof(RpcRole.RpcRoleClient)},

                {"sysUsers.PageSerachSysUser", typeof(RpcSysUser.RpcSysUserClient)},
                {"sysUsers.CreateOrEditSysUser", typeof(RpcSysUser.RpcSysUserClient)},
                {"sysUsers.RemoveSysUser", typeof(RpcSysUser.RpcSysUserClient)},
            };
        }
    }
}
