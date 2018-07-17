// Copyright (C) 2017 Dmitry Yakimenko (detunized@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

namespace RoboForm
{
    public class Vault
    {
        public readonly Account[] Accounts;

        // Use GenerateRandomDeviceId to generate a random device id only once per device,
        // store it and reuse later on subsequent logins.
        // Calling Vault.Open(username, password, Vault.GenerateRandomDeviceId(), ui) is
        // not a good idea. See bellow.
        public static Vault Open(string username, string password, string deviceId, Ui ui, Logger logger = null)
        {
            var clientInfo = new ClientInfo(username: username,
                                            password: password,
                                            deviceId: deviceId);
            return Open(clientInfo, ui, logger, new HttpClient());
        }

        // Generates a random device id that should be used with every new device.
        // Don't generate a new id at every login. This will prevent "remember this
        // device" feature with two-factor authentication from working properly. Using
        // a unique id every time also pollutes the server list of known devices. It
        // might start refusing new connections at some point. This is not proven but
        // easily possible as this happens with some other similar services.
        public static string GenerateRandomDeviceId()
        {
            return Crypto.RandomDeviceId();
        }

        //
        // Internal
        //

        internal static Vault Open(ClientInfo clientInfo, Ui ui, Logger logger, IHttpClient http)
        {
            return Client.OpenVault(clientInfo, ui, logger, http);
        }

        internal Vault(Account[] accounts)
        {
            Accounts = accounts;
        }
    }
}
