﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sdl.Community.GroupShareKit.Tests.Integration.Clients
{
    public class PermissionClientTest
    {
        [Fact]
        public async Task GetAll()
        {
            var groupShareClient = await Helper.GetAuthenticatedClient();
            var response = await groupShareClient.Permission.GetAll();

            Assert.True(response != null);
        }
    }
}
