using Smod2;
using Smod2.Attributes;
using Smod2.Config;
using Smod2.EventHandlers;
using Smod2.Events;

namespace AdminHierarchyImmunity
{
	[PluginDetails(
		author = "PatPeter",
		name = "AdminHierarchyImmunity",
		description = "Prevents lower-tier staff from performing certain admin commands on higher tier staff.",
		id = "patpeter.admin.hierarchy.immunity",
		configPrefix = "ahi",
		langFile = "admin_hierarchy_immunity",
		version = "1.0.0.5",
		SmodMajor = 3,
		SmodMinor = 4,
		SmodRevision = 0
		)]
	class AdminHierarchyImmunity : Plugin
	{
		public override void OnDisable()
		{
		}

		public override void OnEnable()
		{
			this.Info("Admin Hierarchy Immunity has loaded :)");
			this.Info("Config value: " + this.GetConfigString("ahi_tree"));
		}

		public override void Register()
		{
			// Register Events
			this.AddEventHandler(typeof(IEventHandlerAdminQuery), new AdminQueryHandler(this), Priority.Highest);
			this.AddEventHandler(typeof(IEventHandlerBan), new BanHandler(this), Priority.Highest);
			//this.AddEventHandler(typeof(IEventHandlerAuthCheck), new AuthCheckHandler(this), Priority.Highest);
			// Register Commands
			this.AddCommand("ahi_toggle", new ToggleCommand(this));
			// Register config settings
			this.AddConfig(new ConfigSetting("ahi_tree", new string[] { "owner", "admin", "moderator" }, true, "List of admin tiers ranked highest-to-lowest."));
		}
	}
}
