namespace Ambit.AppCore.EntityModels
{
	public class Sidebar
	{
		public string Text { get; set; }
		public string Action { get; set; }
		public string Controller { get; set; }
		public Boolean Active { get; set; }
		public string Class { get; set; }
		public SubMenu SubMenu { get; set; }
	}

	public class SubMenu
	{
		public string Name { get; set; }
		public string Class { get; set; }
		public List<Sidebar> MenuItems { get; set; }
	}

	public class CommonAPIReponse<T>
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public T data { get; set; }
	}
}
