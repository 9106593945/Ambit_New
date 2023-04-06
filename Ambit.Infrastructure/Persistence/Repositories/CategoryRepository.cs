using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Repositories;
using Ambit.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Ambit.Infrastructure.Persistence.Repositories
{
	public class CategoryRepository : BaseRepository, ICategoryRepository
	{
		public CategoryRepository(AppDbContext dbContext) : base(dbContext)
		{
		}
		public AppDbContext AppDbContext
		{
			get { return _dbContext as AppDbContext; }
		}

		public IEnumerable<CategoryEntityModel> GetAllCategories()
		{
			var categories = _dbContext.Category.ToList()
				.Where(i => i.isDeleted == false)
				.Select(x => new CategoryEntityModel()
				{
					Description = x.description,
					CategoryId = x.categoryid,
					Name = x.name,
					Active = x.Active,
					Image = x.image
				});

			if (categories == null)
				return null;

			return categories;
		}

		public CategoryEntityModel GetCategoryById(int Id)
		{
			var categories = _dbContext.Category.ToList()
				.Where(i => i.isDeleted == false && i.categoryid == Id)
				.Select(x => new CategoryEntityModel()
				{
					Description = x.description,
					CategoryId = x.categoryid,
					Name = x.name,
					Active = x.Active,
					Image = x.image
				});

			if (categories == null)
				return null;

			return categories.FirstOrDefault();
		}

		public bool AddNewCategory(CategoryEntityModel categoryEntityModel)
		{
			var category = _dbContext.Category.Add(new category
			{
				name = categoryEntityModel.Name,
				description = categoryEntityModel.Description,
				image = categoryEntityModel.Image
			});

			return true;
		}

		public bool UpdateCategory(CategoryEntityModel categoryEntityModel)
		{
			var category = _dbContext.Category.Find(categoryEntityModel.CategoryId);
			if (category != null)
			{
				category.name = categoryEntityModel.Name;
				category.description = categoryEntityModel.Description;
				category.image = categoryEntityModel.Image;
				return true;
			}
			return false;
		}

		public bool DeleteCategory(long id)
		{
			var category = _dbContext.Category.Find(id);
			if (category != null)
			{
				category.isDeleted = true;
				return true;
			}
			return false;
		}

		public bool ActiveInactiveCategory(long id, bool status)
		{
			var category = _dbContext.Category.Find(id);
			if (category != null)
			{
				category.Active = status;
				return true;
			}
			return false;
		}

		public IEnumerable<CategoryEntityModel> GetCategoriesByKey(string key)
		{
			var category = _dbContext.Category.Where(i => i.name.Contains(key) && i.isDeleted == false && i.Active == true)
				.Select(x => new CategoryEntityModel()
				{
					Description = x.description,
					CategoryId = x.categoryid,
					Name = x.name,
					Image = x.image
				});
			return category;
		}
	}
}
