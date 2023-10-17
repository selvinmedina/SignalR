using System;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRChat.Client.Features.SignalR
{
    public class SignalRClient : IAsyncDisposable
    {
        private HubConnection _hubConnection;
        public SignalRClient()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:6000/chat")
                .Build(); 

            _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Console.WriteLine("ReceiveMessage");
                Console.WriteLine(user);
                Console.WriteLine(message);
            });
        }

        public async Task Start()
        {
            await _hubConnection.StartAsync();
        }

        public async ValueTask DisposeAsync()
        {
            Console.WriteLine("DisposeAsync SignalRClient");
            await _hubConnection.DisposeAsync();
        }

        public bool IsConnected => _hubConnection.State == HubConnectionState.Connected;

        public async Task Send()
        {
            await _hubConnection.SendAsync("SendMessage", "Selvin", "Hola mundo");
        }
    }
}

