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
		public string Message { get; set; }
		public int Status { get; set; }
		public string Error { get; set; }
		public T Data { get; set; }
		public int Id { get; set; }
		public bool IsValid
		{
			get
			{
				IEnumerable<T> enumerable = this.Data as IEnumerable<T>;
				if (enumerable != null && enumerable.Any())
				{
					return true;
				}

				return this.Data != null;
			}
		}
		public bool Success
		{
			get
			{
				return this.Status >= 200 && this.Status <= 299;
			}
		}
	}
}
