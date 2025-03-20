using System;
using Xunit;
using BigHW.Category;
using BigHW.Category.CategoryServices;

namespace BigHW.Tests
{
    public class CategoryServicesTests
    {
        [Fact]
        public void AddCategory_ShouldThrow_WhenNameNotUnique()
        {
            var initialCategory = new Category.Category("TestCategory", "Доход");
            var services = new CategoryServices(initialCategory);
            Assert.Throws<ArgumentException>(() => services.AddCategory("Расход", "TestCategory"));
        }
        
        [Fact]
        public void RemoveCategory_ShouldThrow_WhenCategoryNotFound()
        {
            var initialCategory = new Category.Category("TestCategory", "Доход");
            var services = new CategoryServices(initialCategory);
            Assert.Throws<ArgumentException>(() => services.RemoveCategory("NonExistentCategory"));
        }
        
        [Fact]
        public void RenameCategory_ShouldThrow_WhenCategoryNotFound()
        {
            var initialCategory = new Category.Category("TestCategory", "Доход");
            var services = new CategoryServices(initialCategory);
            Assert.Throws<ArgumentException>(() => services.RenameCategory("NonExistent", "NewName"));
        }
    }
}