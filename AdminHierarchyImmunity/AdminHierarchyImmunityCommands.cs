using Smod2.Commands;

namespace AdminHierarchyImmunity
{
	class ToggleCommand : ICommandHandler
	{
		private AdminHierarchyImmunity plugin;

		public ToggleCommand(AdminHierarchyImmunity plugin)
		{
			this.plugin = plugin;
		}

		public string GetCommandDescription()
		{
			return "Toggles Admin Hierarchy Immunity on or off.";
		}

		public string GetUsage()
		{
			return "";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			return new string[] { "Not implemented yet." };
		}
	}
}
