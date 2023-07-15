using Microsoft.AspNetCore.SignalR;

namespace AspNetCore.MinimalApi.Ext.Sample.Hubs;

public class ChatHub : Hub
{
  //list of user connections
  private static readonly List<User> Users = new();

  //lock
  private static readonly object _lock = new();

  //disconnect, remove user from list
  public override async Task OnDisconnectedAsync(Exception exception) {
    lock (_lock) {
      var users = Users.Where(u => u.ConnectionId == Context.ConnectionId).ToList();

      foreach (var user in users)
        if (user != null)
          Users.Remove(user);
    }

    await Clients.All.SendAsync("UsersUpdated", Users);

    await base.OnDisconnectedAsync(exception);
  }

  //connect, add user to list
  public async Task OnConnect() {
    await Clients.Caller.SendAsync("UsersUpdated", Users);
  }

  public async Task AddUser(string userName) {
    var user = new User {
      ConnectionId = Context.ConnectionId
    };

    lock (_lock) {
      Users.Add(user);
    }

    await Clients.All.SendAsync("UsersUpdated", Users);
  }

  //send message to all users
  public async Task SendMessage(string message, string userName) {
    await Clients.All.SendAsync("ReceiveMessage", message, userName);
  }

  private class User
  {
    public string ConnectionId { get; set; }
    public string Name { get; set; }
  }
}
