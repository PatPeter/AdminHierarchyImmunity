using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;

namespace AdminHierarchyImmunity
{
	class AdminQueryHandler : IEventHandlerAdminQuery
	{
		private AdminHierarchyImmunity plugin;

		public AdminQueryHandler(AdminHierarchyImmunity plugin)
		{
			this.plugin = plugin;
		}

		void IEventHandlerAdminQuery.OnAdminQuery(AdminQueryEvent ev)
		{
			// This event constantly spams this query to console
			// REQUEST_DATA PLAYER_LIST SILENT
			//if (ev.Query.IndexOf("REQUEST_DATA") == -1)
			//{
			//	this.plugin.Info(ev.Admin.Name + "/" + ev.Admin.SteamId + (ev.Handled ? " handled " : " did not handle ") + (ev.Successful ? "successful " : "unsuccessful ") + "query: " + ev.Query + " | Result: " + ev.Output);
			//}
		}
	}

	class BanHandler : IEventHandlerBan
	{
		private AdminHierarchyImmunity plugin;

		public BanHandler(AdminHierarchyImmunity plugin)
		{
			this.plugin = plugin;
		}

		public void OnBan(BanEvent ev)
		{
			string[] tree = this.plugin.GetConfigList("ahi_tree");

			int adminLevel = 0;
			int targetLevel = 0;
			for (int i = 0; i < tree.Length; i++)
			{
				string rank = tree[i];
				//this.plugin.Info("Is the admin's rank " + (ev.Admin.GetUserGroup() != null ? ev.Admin.GetUserGroup().Name : "RN:" + ev.Admin.GetRankName()) + " equal to " + rank + "?");
				if (ev.Admin.GetUserGroup() != null && ev.Admin.GetUserGroup().Name == rank)
				{
					adminLevel = tree.Length - i;
				}

				//this.plugin.Info("Is the player's rank " + (ev.Player.GetUserGroup() != null ? ev.Player.GetUserGroup().Name : "RN:" + ev.Player.GetRankName()) + " equal to " + rank + "?");
				if (ev.Player.GetUserGroup() != null && ev.Player.GetUserGroup().Name == rank)
				{
					targetLevel = tree.Length - i;
				}
			}

			//this.plugin.Info("Is " + adminLevel + " <= " + targetLevel + "?");
			if (adminLevel == 0)
			{
				this.plugin.Info("Allowing staff or global moderator " + ev.Admin.ToString() + " to ban " + ev.Player.ToString() + ".");
				ev.AllowBan = true;
			}
			// Do not allow moderators of the same rank to kick each other or themselves
			else if (adminLevel <= targetLevel)
			{
				this.plugin.Info("[STAFF VIOLATION] Admin " + ev.Admin.ToString() + " tried to ban the same or higher-ranking admin " + ev.Player.ToString());
				ev.Player.PersonalBroadcast(5, "You cannot ban " + ev.Player.Name + " because he/she is the same or higher rank than you.", false);
				ev.AllowBan = false;
			}
			else
			{
				ev.AllowBan = true;
			}
		}
	}

	/*class AuthHandler : IEventHandlerAuthCheck
	{
		private AdminHierarchyImmunity plugin;

		public AuthHandler(AdminHierarchyImmunity plugin)
		{
			this.plugin = plugin;
		}

		public void OnAuthCheck(AuthCheckEvent ev)
		{
			
		}
	}*/
}
