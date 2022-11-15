using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace PWAFeaturesRnd.Hubs
{
	public class NotificationHub : Hub
	{

		//private HubProxy HubProxy { get; set; }

		//public NotificationHub()
		//{

		//}

		//public async Task Index(string id)
		//{

		//}

		public async Task SendMessage(string message)
		{
			await Clients.All.SendAsync("ReceiveMessage", "anonymous", message);
		}

		//public Task ThrowException()
		//{
		//	throw new HubException("This error will be sent to the client!");
		//}

		//      public override async Task OnConnectedAsync()
		//      {
		//	string connectionid = this.Context.ConnectionId;

		//	SendMessage("GLAS00000001", "Test Message");

		//	//HubProxy.Invoke("LogoutAllOtherClients");

		//	//HubProxy = Connection.CreateHubProxy("MessageHub");
		//	//HubProxy.On<string, List<Guid>>("BroadcastControlUpdate", OnControlUpdate);
		//	//HubProxy.On<string, WorkflowPushNotification>("BroadcastWorkflowMessage", OnWorkflowUpdate);
		//	//HubProxy.On<string, BusinessTaskResponse>("BroadcastTaskResult", OnTaskResponse);
		//	//HubProxy.On<string, BusinessTaskResponse>("BroadcastReportResult", OnReportResponse);
		//	//HubProxy.On<string, bool>("DisconnectUser", DisconnectUser);
		//	//HubProxy.On<string, ClientUpdateData>("broadcastClientDataUpdate", OnClientDataUpdate);
		//	//HubProxy.On<List<string>>("SendConnectionIds", OnConnectionIdResponse);
		//	//HubProxy.On("Logout", Logout);

		//	await base.OnConnectedAsync();
		//      }

		//      public override async Task OnDisconnectedAsync(Exception exception)
		//      {


		//          await base.OnDisconnectedAsync(exception);
		//      }

		//      private async void OnTaskResponse(object response)
		//      {

		//      }

		//[HubMethodName("change_weather")]
		public void ChangeWeather(int temperature)
		{
			//Clients is ConnectionContext, it holds the information about all the connection.   
			//Others in 'Clients.Others' is holding the list of all connected user except the caller user   
			//(the user which has called this method).   
			//NotifyUser is a function on the clientside, you will understand it later.   
			//Clients.Others.NotifyUser(temperature);
		}	
	}
}
