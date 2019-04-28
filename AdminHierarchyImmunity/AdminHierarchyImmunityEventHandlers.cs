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
			this.plugin.Info(ev.Admin.Name + "/" + ev.Admin.SteamId + (ev.Handled ? " handled " : " did not handle ") + (ev.Successful ? "successful " : "unsuccessful ") + "query: " + ev.Query + " | Result: " + ev.Output);
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
				if (ev.Admin.GetUserGroup() != null && ev.Admin.GetUserGroup().Name == rank)
				{
					adminLevel = i;
				}
				if (ev.Player.GetUserGroup() != null && ev.Player.GetUserGroup().Name == rank)
				{
					targetLevel = i;
				}
			}

			if (adminLevel == 0)
			{
				this.plugin.Info("Allowing staff or global moderator " + ev.Admin.ToString() + " to ban " + ev.Player.ToString() + ".");
			}
			else if (adminLevel <= targetLevel)
			{
				this.plugin.Info("Admin " + ev.Admin.ToString() + " tried to ban the same or higher-ranking admin " + ev.Player.ToString());
				ev.Player.PersonalBroadcast(5, "You cannot ban " + ev.Player.Name + " because he/she is the same or higher rank than you.", false);
				ev.AllowBan = false;
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
