using System;
using System.Collections.Generic;
using System.Linq;

namespace BigHW.Category.CategoryServices
{
    public class CategoryServices
    {
        private List<Category> _categoryList = new List<Category>();

        public CategoryServices(Category category)
        {
            _categoryList.Add(category);
        }

        public void AddCategory(string type, string name)
        {
            if (!IsCategoryNameUnique(name))
            {
                throw new ArgumentException("Категория с таким именем уже существует.");
            }

            var category = new Category(name, type);
            _categoryList.Add(category);
            Console.WriteLine($"Успешно создана категория {category.Name}, {category.Type}.");
        }

        public void RemoveCategory(string categoryName)
        {
            var category = _categoryList.FirstOrDefault(c => c.Name == categoryName);

            if (category != null)
            {
                _categoryList.Remove(category);
                Console.WriteLine("Успешно удалена категория.");
            }
            else
            {
                throw new ArgumentException("Данной категории не существует");
            }
        }

        public void RenameCategory(string categoryName, string name)
        {
            var category = _categoryList.FirstOrDefault(c => c.Name == categoryName);

            if (category != null)
            {
                if (!IsCategoryNameUnique(name))
                {
                    throw new ArgumentException("Категория с таким именем уже существует.");
                }

                category.Name = name;
                Console.WriteLine($"Имя категории с ID {category.Id} изменено на {category.Name}.");
            }
            else
            {
                throw new ArgumentException($"Категория '{categoryName}' не найдена.");
            }
        }

        private bool IsCategoryNameUnique(string name)
        {
            return !_categoryList.Any(c => c.Name == name);
        }
    }
}
