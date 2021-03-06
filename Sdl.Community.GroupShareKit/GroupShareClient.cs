﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sdl.Community.GroupShareKit.Clients;
using Sdl.Community.GroupShareKit.Helpers;
using Sdl.Community.GroupShareKit.Http;

namespace Sdl.Community.GroupShareKit
{
    /// <summary>
    /// A Client for the GroupShare API v1. 
    /// </summary>
    public class GroupShareClient: IGroupShareClient
    {

        public const string ManagementApi = "ManagementRestApi";
        public const string ProjectApi = "ProjectServerRestApi";

        public static IEnumerable<string> AllScopes =
            new[]
            {
                ManagementApi,
                ProjectApi
            }; 

        /// <summary>
        /// Create a new instance of the GroupShare API v1 client pointing to the specified baseAddress.
        /// </summary>
        /// <param name="credentialStore">Provides credentials to the client when making requests</param>
        /// <param name="baseAddress">
        /// The address to point this client to.</param>
        public GroupShareClient(ICredentialStore credentialStore, Uri baseAddress)
            : this(new Connection(baseAddress, credentialStore))
        {
        }
        /// <summary>
        /// Create a new instance of the GroupShare API v1 client using the specified connection.
        /// </summary>
        /// <param name="connection">The underlying <seealso cref="IConnection"/> used to make requests</param>
        public GroupShareClient(IConnection connection)
        {
            Ensure.ArgumentNotNull(connection,"connection");

            Connection = connection;
            var apiConnection = new ApiConnection(connection);
            Project = new ProjectClient(apiConnection);
            User = new UserClient(apiConnection);
            Organization = new OrganizationClient(apiConnection);
            Authenticate = new AuthenticateClient(apiConnection);
            Role = new RoleClient(apiConnection);
            Permission = new PermissionClient(apiConnection);
        }

        /// <summary>
        /// Creates a new instance of the GroupShare API v1 client that has the authorization token configured
        /// </summary>
        /// <param name="user">Provides the user for which to obtain the token.</param>
        /// <param name="password">Provides the password for the specified user.</param>
        /// <param name="baseAddress">The address to point this client to.</param>
        /// <param name="scopes">The token scope(s).</param>
        /// <returns></returns>
        public async static Task<GroupShareClient> AuthenticateClient(string user, string password, Uri baseAddress, IEnumerable<string> scopes)
        {
            var credentials = new Credentials(user,password);

            var inMemoryCredentials = new InMemoryCredentialStore(credentials);

            var tokenGroupShareClient = new GroupShareClient(inMemoryCredentials, baseAddress);

            var authorization = await tokenGroupShareClient.Authenticate.Post(scopes);

            credentials = new Credentials(authorization.Token);

            inMemoryCredentials = new InMemoryCredentialStore(credentials);

            var groupShareClient = new GroupShareClient(inMemoryCredentials,baseAddress);

            return groupShareClient;
        }
        public IProjectClient Project { get; }

        public IUserClient User { get; }
        public IOrganizationClient Organization { get; set; }

        public IAuthenticateClient Authenticate { get; set; }

        public IRoleClient Role { get; set; }
        public IPermissionClient Permission { get; set; }

        /// <summary>
        /// Convenience property for getting and setting credentials.
        /// </summary>
        /// <remarks>
        /// You can use this property if you only have a single hard-coded credential. Otherwise, pass in an 
        /// <see cref="ICredentialStore"/> to the constructor. 
        /// Setting this property will change the <see cref="ICredentialStore"/> to use 
        /// the default <see cref="InMemoryCredentialStore"/> with just these credentials.
        /// </remarks>
        public Credentials Credentials
        {
            get { return Connection.Credentials; }
            set
            {
                Ensure.ArgumentNotNull(value, "value");
                Connection.Credentials = value;
            }
        }

        /// <summary>
        /// The base address of the GroupShare API.
        /// </summary>
        public Uri BaseAddress => Connection.BaseAddress;

        /// <summary>
        /// Provides a client connection to make rest requests to HTTP endpoints.
        /// </summary>
        public IConnection Connection { get; }
        
    }
}
