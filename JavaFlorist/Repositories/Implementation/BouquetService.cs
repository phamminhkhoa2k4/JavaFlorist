﻿using JavaFlorist.Models.Domain;
using JavaFlorist.Models.DTO;
using JavaFlorist.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace JavaFlorist.Repositories.Implementation
{
    public class BouquetService : IBouquetService
    {
        private readonly DatabaseContext ctx;

        public BouquetService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Add(Bouquet_Info model)
        {
            try
            {

                ctx.Bouquet_Info.Add(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool Delete(int id)
        {

            try
            {
                var data = this.GetById(id);
                if (data == null)
                    return false;
                ctx.Bouquet_Info.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public List<Bouquet_Info> GetByCategory(string category)
        {
            var bouquetsCategory = ctx.Bouquet_Info.Where(b => b.category == category).ToList();

            return bouquetsCategory;
        }

        public Bouquet_Info GetById(int id)
        {
            return ctx.Bouquet_Info.Find(id);
        }

        public List<Bouquet_Info> GetRelatedBouquets(int bouquet_id)
        {
            var bouquet = this.GetById(bouquet_id); // Lấy thông tin sản phẩm theo ID

            // Lấy các sản phẩm khác cùng thể loại với sản phẩm hiện tại (bouquet)
            var relatedBouquets = this.GetByCategory(bouquet.category);

            // Loại bỏ sản phẩm hiện tại khỏi danh sách sản phẩm liên quan
            relatedBouquets = relatedBouquets.Where(b => b.bouquet_id != bouquet_id).Take(4).ToList();

            return relatedBouquets;
        }

        public List<Bouquet_Info> GetTopFiveDistinctCategoriesBySoldCount()
        {
            var topFiveCategories = ctx.Bouquet_Info
    .GroupBy(b => b.category)
    .Select(g => new
    {
        Category = g.Key,
        TotalSoldCount = g.Sum(b => b.sold)
    })
    .OrderByDescending(g => g.TotalSoldCount)
    .Take(5)
    .ToList();

            var topFiveCategoriesNames = topFiveCategories.Select(c => c.Category).ToList();

            var topFiveProducts = ctx.Bouquet_Info
                .Where(b => topFiveCategoriesNames.Contains(b.category))
                .GroupBy(b => b.category)
                .Select(group => group.OrderByDescending(b => b.sold).FirstOrDefault())
                .ToList();

            // Fetching additional products beyond the top categories
            var remainingProducts = ctx.Bouquet_Info
                .Where(b => !topFiveProducts.Any(tp => tp.bouquet_id == b.bouquet_id)) // Exclude products already selected
                .OrderByDescending(b => b.sold)
                .Take(8 - topFiveProducts.Count) // Select the remaining count to reach top 8
                .ToList();


            return topFiveProducts;


        }

        public BouquetListVm List(string term = "", bool paging = false, int currentPage = 0)
        {
            var data = new BouquetListVm();

            var list = ctx.Bouquet_Info.ToList();


            if (!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(a => a.name.ToLower().StartsWith(term)).ToList();
            }

            if (paging)
            {
                // here we will apply paging
                int pageSize = 20;
                int count = list.Count;
                int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = TotalPages;
            }


            data.BouquetList = list.AsQueryable();
            return data;
        }

        public BouquetListVm ListCategory(string category = "", bool paging = false, int currentPage = 0)
        {
            var data = new BouquetListVm();

            var list = ctx.Bouquet_Info.ToList();

            if (!string.IsNullOrEmpty(category))
            {
                category = category.ToLower();
                list = list.Where(a => a.category.ToLower() == category).ToList();
            }

            if (paging)
            {
                // Apply paging
                int pageSize = 20;
                int count = list.Count;
                int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = TotalPages;
            }

            data.BouquetList = list.AsQueryable();
            return data;
        }

        public List<Bouquet_Info> Search(string searchTerm)
        {
            var suggestions = ctx.Bouquet_Info
                .Where(b => b.name.Contains(searchTerm) || b.category.Contains(searchTerm)) // Tìm kiếm theo tên hoặc category
                .ToList();

            return suggestions;
        }

        public bool Update(Bouquet_Info model)
        {
            try
            {
                ctx.Bouquet_Info.Update(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
