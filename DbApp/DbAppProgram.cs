﻿

using DataLayer;

var dataservice = new DataService();

//var categories = dataservice.GetCategories();

//PrintCategories(dataservice);

//dataservice.AddCategory("dsfsfksdlfk", "sfæsjdoi");
//dataservice.DeleteCategory(9999);

//PrintCategories(dataservice);

PrintProducts(dataservice);



static void PrintProducts(IDataService dataService)
{
    foreach (var e in dataService.GetProducts())
    {
        Console.WriteLine($"{e.Id}, {e.Name}, {e.Category.Name}");
    }
}

static void PrintCategories(IDataService dataService)
{
    foreach (var e in dataService.GetCategories())
    {
        Console.WriteLine($"{e.Id}, {e.Name}, {e.Description}");
    }
}